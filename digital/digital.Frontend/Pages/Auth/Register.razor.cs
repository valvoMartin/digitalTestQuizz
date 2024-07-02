using CurrieTechnologies.Razor.SweetAlert2;
using digital.Frontend.Repositories;
using digital.Frontend.Services;
using digital.Shared.DTOs;
using digital.Shared.Entities;
using digital.Shared.Enums;
using Microsoft.AspNetCore.Components;
using Orders.Frontend.Repositories;

namespace digital.Frontend.Pages.Auth
{
    public partial class Register
    {

        // private SweetAlertTheme SweetAlert = SweetAlertTheme.Dark;
        private UserDTO userDTO = new();
        private List<Country>? countries;
        private List<State>? states;
        private List<City>? cities;
        private bool loading;


        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;


        [Inject] private IRepository Repository { get; set; } = null!;
        //[Inject] private ILoginService LoginService { get; set; } = null!;


        protected override async Task OnInitializedAsync()
        {
            //await LoadCountriesAsync();
        }

        private async Task CreteUserAsync()
        {
            userDTO.UserName = userDTO.Email;
            userDTO.UserType = UserType.User;

           

            loading = true;
            var responseHttp = await Repository.PostAsync<UserDTO>("/api/accounts/CreateUser", userDTO);
            loading = false;
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            await SweetAlertService.FireAsync("Confirmación", "Su cuenta ha sido creada con éxito. <br>Se te ha <b>enviado un correo electrónico</b> con las instrucciones para activar tu usuario.", SweetAlertIcon.Info);

            NavigationManager.NavigateTo("/");
        }


        //private async Task CountryChangedAsync(ChangeEventArgs e)
        //{
        //    var selectedCountry = Convert.ToInt32(e.Value!);
        //    states = null;
        //    cities = null;
        //    userDTO.CityId = 0;
        //    await LoadStatesAsync(selectedCountry);
        //}

        //private async Task StateChangedAsync(ChangeEventArgs e)
        //{
        //    var selectedState = Convert.ToInt32(e.Value!);
        //    cities = null;
        //    userDTO.CityId = 0;
        //    await LoadCitiesAsync(selectedState);
        //}

        //private async Task LoadCitiesAsync(int stateId)
        //{
        //    var responseHttp = await Repository.GetAsync<List<City>>($"/api/cities/combo/{stateId}");
        //    if (responseHttp.Error)
        //    {
        //        var message = await responseHttp.GetErrorMessageAsync();
        //        await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
        //        return;
        //    }

        //    cities = responseHttp.Response;
        //}
        //private async Task LoadCountriesAsync()
        //{
        //    var responseHttp = await Repository.GetAsync<List<Country>>("/api/countries/combo");
        //    if (responseHttp.Error)
        //    {
        //        var message = await responseHttp.GetErrorMessageAsync();
        //        await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
        //        return;
        //    }

        //    countries = responseHttp.Response;
        //}

        //private async Task LoadStatesAsync(int countryId)
        //{
        //    var responseHttp = await Repository.GetAsync<List<State>>($"/api/states/combo/{countryId}");
        //    if (responseHttp.Error)
        //    {
        //        var message = await responseHttp.GetErrorMessageAsync();
        //        await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
        //        return;
        //    }

        //    states = responseHttp.Response;
        //}

        

    }
}
