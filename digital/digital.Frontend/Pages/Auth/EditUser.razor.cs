
//using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using digital.Frontend.Repositories;
using digital.Frontend.Services;
using digital.Shared.DTOs;
using digital.Shared.Entities.Countries;
using digital.Shared.Entities.Test;
using Microsoft.AspNetCore.Components;
using System.Net;

namespace digital.Frontend.Pages.Auth
{
    public partial class EditUser
    {

        private User? user;
        private List<Country>? countries;
        private List<State>? states;
        private List<City>? cities;

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private ILoginService LoginService { get; set; } = null!;

        //[CascadingParameter] IModalService Modal { get; set; } = default!;

        

        protected override async Task OnInitializedAsync()
        {
            await LoadUserAsync();
            // await LoadCountriesAsync();
            //await LoadStatesAsync(user!.City!.State!.Country!.Id);
            //await LoadCitiesAsync(user!.City!.State!.Id);

            //if (!string.IsNullOrEmpty(user!.Photo))
            //{
            //    imageUrl = user.Photo;
            //    user.Photo = null;
            //}
        }

        //private void ShowModal()
        //{
        //    Modal.Show<ChangePassword>();
        //}

        private async Task LoadUserAsync()
        {
            var responseHttp = await Repository.GetAsync<User>($"/api/accounts");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/");
                    return;
                }
                var messageError = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", messageError, SweetAlertIcon.Error);
                return;
            }
            user = responseHttp.Response;
        }

        

        //private async Task CountryChangedAsync(ChangeEventArgs e)
        //{
        //    var selectedCountry = Convert.ToInt32(e.Value!);
        //    states = null;
        //    cities = null;
        //    user!.CityId = 0;
        //    await LoadStatesAsync(selectedCountry);
        //}


        //private async Task StateChangedAsync(ChangeEventArgs e)
        //{
        //    var selectedState = Convert.ToInt32(e.Value!);
        //    cities = null;
        //    user!.CityId = 0;
        //    await LoadCitiesAsync(selectedState);
        //}


        private async Task LoadCountriesAsync()
        {
            var responseHttp = await Repository.GetAsync<List<Country>>("/api/countries/combo");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            countries = responseHttp.Response;
        }


        private async Task LoadStatesAsync(int countryId)
        {
            var responseHttp = await Repository.GetAsync<List<State>>($"/api/states/combo/{countryId}");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            states = responseHttp.Response;
        }


        private async Task LoadCitiesAsync(int stateId)
        {
            var responseHttp = await Repository.GetAsync<List<City>>($"/api/cities/combo/{stateId}");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            cities = responseHttp.Response;
        }


        private async Task SaveUserAsync()
        {
            var responseHttp = await Repository.PutAsync<User, TokenDTO>("/api/accounts", user!);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            await LoginService.LoginAsync(responseHttp.Response!.Token);
            NavigationManager.NavigateTo("/");
        }
    }
}
