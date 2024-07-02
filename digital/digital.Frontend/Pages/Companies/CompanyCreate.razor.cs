using CurrieTechnologies.Razor.SweetAlert2;
using digital.Frontend.Repositories;
using digital.Shared.DTOs;
using digital.Shared.Entities;
using digital.Shared.Enums;
using Microsoft.AspNetCore.Components;

namespace digital.Frontend.Pages.Companies
{
    public partial class CompanyCreate
    {
        private Company company = new();
        private List<Country>? countries;
        private List<State>? states;
        private List<City>? cities;
        private bool loading;

        private int step = 1;


        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;


        protected override async Task OnInitializedAsync()
        {
            //TODO: Traer los sectores de la empresa

            await LoadCountriesAsync();
        }


        private void HandleValidSubmit()
        {
            if (step < 2)
            {
                step++;
            }
            else
            {
                CreteCompanyAsync();
            }
        }

        private void PrevStep()
        {
            if (step > 1)
            {
                step--;
            }
        }


        private async Task CreteCompanyAsync()
        {
            

            var responseHttp = await Repository.PostAsync<Company>("/api/companies/full", company);
            
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            Return();
        }


        private void Return()
        {
            //productForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo($"/home");
        }






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

        private async Task CountryChangedAsync(ChangeEventArgs e)
        {
            var selectedCountry = Convert.ToInt32(e.Value!);
            states = null;
            cities = null;
            company.CityId = 0;
            await LoadStatesAsync(selectedCountry);
        }

        private async Task StateChangedAsync(ChangeEventArgs e)
        {
            var selectedState = Convert.ToInt32(e.Value!);
            cities = null;
            company.CityId = 0;
            await LoadCitiesAsync(selectedState);
        }

    }
}