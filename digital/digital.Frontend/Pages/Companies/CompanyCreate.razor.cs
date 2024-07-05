using CurrieTechnologies.Razor.SweetAlert2;
using digital.Frontend.Repositories;
using digital.Shared.DTOs;
using digital.Shared.Entities;
using digital.Shared.Enums;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;
using System.Reflection;

namespace digital.Frontend.Pages.Companies
{
    public partial class CompanyCreate
    {
        //private List<string> ItemNames = new List<string> { "RRHH", "Administración", "Ventas", "Producción" };



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


        private async void HandleValidSubmit()
        {
            if (step < 3)
            {
                step++;
            }
            else
            {
                await CreteCompanyAsync();
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









        private RubroCompanyEnum selectedRubro;
        private List<SectorCompanyEnum> filteredSectors = new List<SectorCompanyEnum>();

        private void OnRubroChanged(ChangeEventArgs e)
        {
            selectedRubro = (RubroCompanyEnum)Enum.Parse(typeof(RubroCompanyEnum), e.Value.ToString());
            FilterSectorsByRubro(selectedRubro);
        }

        private void FilterSectorsByRubro(RubroCompanyEnum rubro)
        {
            string rubroKey = GetRubroKey(rubro);
            filteredSectors = Enum.GetValues(typeof(SectorCompanyEnum))
                                  .Cast<SectorCompanyEnum>()
                                  .Where(s => GetEnumDescription(s).StartsWith(rubroKey, StringComparison.OrdinalIgnoreCase))
                                  .ToList();
        }

        private string GetRubroKey(RubroCompanyEnum rubro)
        {
            switch (rubro)
            {
                case RubroCompanyEnum.Agro:
                    return "Agropecuario";
                case RubroCompanyEnum.IndsutriaMineria:
                    return "Industria y Mineria";
                case RubroCompanyEnum.Servicios:
                    return "Servicios";
                case RubroCompanyEnum.Construccion:
                    return "Construccion";
                case RubroCompanyEnum.Comercio:
                    return "Comercio";
                default:
                    return string.Empty;
            }
        }
        private string GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = (DescriptionAttribute)field.GetCustomAttribute(typeof(DescriptionAttribute));
            return attribute != null ? attribute.Description : value.ToString();
        }

        //private string sectorValidationMessage = "Seleccione un Rubro primero";

        //private void ValidateSector()
        //{
        //    if (company.Rubro == 0)
        //    {
        //        sectorValidationMessage = "Seleccione un Rubro primero";
        //    }
        //    else
        //    {
        //        sectorValidationMessage = "Seleccione un Sector";
        //    }
        //}












        //Indica que el porcentaje de los cuanto asigna a cada sector de la empresa

        private string Administracion = "16.6";
        private string Comercializacion = "16.6";
        private string Produccion = "16.6";
        private string RRHH = "16.6";
        private string Logistica = "16.6";
        private string Mantenimiento = "16.6";

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
                ResetValues(Administracion, "16.6");
                ResetValues(Comercializacion, "16.6");
                ResetValues(Produccion, "16.6");
                ResetValues(RRHH, "16.6");
                ResetValues(Logistica, "16.6");
                ResetValues(Mantenimiento, "16.6");
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
                ResetValues(Administracion, "16.6");
                ResetValues(Comercializacion, "16.6");
                ResetValues(Produccion, "16.6");
                ResetValues(RRHH, "16.6");
                ResetValues(Logistica, "16.6");
                ResetValues(Mantenimiento, "16.6");
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