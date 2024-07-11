using CurrieTechnologies.Razor.SweetAlert2;
using digital.Frontend.Repositories;
using digital.Shared.DTOs;
using digital.Shared.Entities.Companies;
using digital.Shared.Entities.Countries;
using digital.Shared.Enums;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;
using System.Reflection;
using static System.Net.WebRequestMethods;

namespace digital.Frontend.Pages.Companies
{
    public partial class CompanyCreate
    {
        //private List<string> ItemNames = new List<string> { "RRHH", "Administración", "Ventas", "Producción" };



        private Company company = new();
        private List<Country>? countries = new();
        private List<State>? states = new();
        private List<City>? cities = new();

        private List<Category>? categories = new();

        private List<Rubro>? rubros = new();
        private List<Sector>? sectors = new();

        private bool loading;

        private int step = 1;


        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;


        protected override async Task OnInitializedAsync()
        {
            //TODO: Traer los sectores de la empresa

            await LoadCountriesAsync();
            await LoadRubrosAsync();
        }


        private async void HandleValidSubmit()
        {
            if (step < 3)
            {
                step++;
            }
            else
            {
                await CreateCompanyAsync();
            }
        }

        private void PrevStep()
        {
            if (step > 1)
            {
                step--;
            }
        }


        private async Task CreateCompanyAsync()
        {
            //TODO: Ver pq no carga la empresa => error de campos vacios

            company.PorcAdministracion = float.Parse(Administracion);
            company.PorcComercializacion = float.Parse(Comercializacion);
            company.PorcProduccion = float.Parse(Produccion);
            company.PorcRRHH = float.Parse(RRHH);
            company.PorcLogistica = float.Parse(Logistica);
            company.PorcMantenimiento = float.Parse(Mantenimiento);

            company.PorcProductoDestinadoAMercadoLocal = float.Parse(MercadoLocal);
            company.PorcProductoDestinadoAMercadoExterior = float.Parse(MercadoExterior);

            var responseHttp = await Repository.PostAsync("api/companies/full", company);
            
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                if (message.Contains("Debes") || message.Contains("El campo"))
                {
                    message = "Aun quedan campos obligatorios por completar";
                }
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



        
        private async Task LoadRubrosAsync()
        {
            //rubros = await Http.GetFromJsonAsync<List<Rubro>>("api/rubros");

            var responseHttp = await Repository.GetAsync<List<Rubro>>("api/rubros");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                // TODO: Manejar error cuando los campos aun estan Vacio. 
                return;
            }

            rubros = responseHttp.Response;
        }

        private async Task LoadCountriesAsync()
        {
            var responseHttp = await Repository.GetAsync<List<Country>>("/api/countries/combo");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                // TODO: Manejar error cuando los campos aun estan Vacio. 
                return;
            }

            countries = responseHttp.Response;
        }

        

        private async Task CountryChangedAsync(ChangeEventArgs e)
        {
            var selectedCountry = Convert.ToInt32(e.Value!);
            states = null;
            cities = null;
            categories = null;
            company.CityId = 0;

            await LoadStatesAsync(selectedCountry);
            await LoadCategoriesAsync(selectedCountry);
        }

        private async Task StateChangedAsync(ChangeEventArgs e)
        {
            var selectedState = Convert.ToInt32(e.Value!);
            cities = null;
            company.CityId = 0;
            await LoadCitiesAsync(selectedState);
        }


        private async Task RubroChangedAsync(ChangeEventArgs e)
        {
            var selectedRubro = Convert.ToInt32(e.Value!);
            sectors = null;
            company.SectorId = 0;
            await LoadSectorsAsync(selectedRubro);

        }

        private async Task LoadSectorsAsync(int selectedRubro)
        {
            var responseHttp = await Repository.GetAsync<List<Sector>>($"api/rubros/{selectedRubro}/sectors");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            sectors = responseHttp.Response;

        }

        private async Task LoadCategoriesAsync(int countryId)
        {
            var responseHttp = await Repository.GetAsync<List<Category>>($"/api/countries/categories/{countryId}");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            categories = responseHttp.Response;
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












        //Indica que el porcentaje de los cuanto asigna a cada sector de la empresa
        const string individual = "16.6";

        private string Administracion = individual;
        private string Comercializacion = individual;
        private string Produccion = individual;
        private string RRHH = individual;
        private string Logistica = individual;
        private string Mantenimiento = individual;

        private string MercadoLocal = "50";
        private string MercadoExterior = "50";


        //private float Total1 => float.Parse(Administracion) + float.Parse(Comercializacion) + float.Parse(Produccion) + float.Parse(RRHH);
        //private float Total2 => float.Parse(MercadoLocal) + float.Parse(MercadoExterior);

        private void OnMercadoLocalChange(string value)
        {
            if (float.TryParse(value, out float newValue))
            {
                MercadoLocal = newValue.ToString("0.#");
                MercadoExterior = (100 - newValue).ToString("0.#");
            }
        }

        private void OnMercadoExteriorChange(string value)
        {
            if (float.TryParse(value, out float newValue))
            {
                MercadoExterior = newValue.ToString("0.#");
                MercadoLocal = (100 - newValue).ToString("0.#");
            }
        }

        private void OnSliderChange(int index, string value)
        {
            if (float.TryParse(value, out float newValue))
            {
                AdjustValues(index, newValue);
            }
        }

        private void AdjustValues(int index, float newValue)
        {
            float[] values = { float.Parse(Administracion), float.Parse(Comercializacion), float.Parse(Produccion), float.Parse(RRHH), float.Parse(Logistica), float.Parse(Mantenimiento)};
            float oldValue = values[index];
            values[index] = newValue;

            float diff = newValue - oldValue;
            float totalOthers = values.Where((v, i) => i != index).Sum();

            if (totalOthers == 0)
            {
                ResetValues(Administracion, individual);
                ResetValues(Comercializacion, individual);
                ResetValues(Produccion, individual);
                ResetValues(RRHH, individual);
                ResetValues(Logistica, individual);
                ResetValues(Mantenimiento, individual);
                return;
            }

            for (int i = 0; i < values.Length; i++)
            {
                if (i != index)
                {
                    values[i] -= (values[i] / totalOthers) * diff;
                    if (values[i] < 0) values[i] = 0;
                }
            }

            float correction = 100 - values.Sum();
            values[index] += correction;

            if (values.Any(v => float.IsNaN(v)))
            {
                ResetValues(Administracion, individual);
                ResetValues(Comercializacion, individual);
                ResetValues(Produccion, individual);
                ResetValues(RRHH, individual);
                ResetValues(Logistica, individual);
                ResetValues(Mantenimiento, individual);
                return;
            }

            Administracion = values[0].ToString("0.#");
            Comercializacion = values[1].ToString("0.#");
            Produccion = values[2].ToString("0.#");
            RRHH = values[3].ToString("0.#");
            Logistica = values[4].ToString("0.#");
            Mantenimiento = values[5].ToString("0.#");
        }


        private void EnsureTotalIs100()
        {
            float[] values = { float.Parse(Administracion), float.Parse(Comercializacion), float.Parse(Produccion), float.Parse(RRHH), float.Parse(Logistica), float.Parse(Mantenimiento) };

            float total = values.Sum();

            if (total != 100f)
            {
                float difference = 100f - total;

                // Ajustar el último valor para que el total sea 100%

                values[3] += difference;
                if (values[3] < 0) values[3] = 0;

                Administracion = values[0].ToString("0.0");
                Comercializacion = values[1].ToString("0.0");
                Produccion = values[2].ToString("0.0");
                RRHH = values[3].ToString("0.0");
                Logistica = values[4].ToString("0.0");
                Mantenimiento = values[5].ToString("0.0");
            }
        }


        private void ResetValues(string value, string porcent)
        {
            value = porcent;
        }






    }
}