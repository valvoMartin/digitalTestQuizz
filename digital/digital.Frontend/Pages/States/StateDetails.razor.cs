//using Blazored.Modal;
//using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using digital.Frontend.Pages.Cities;
using digital.Frontend.Repositories;
using digital.Shared.Entities.Countries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Net;

namespace digital.Frontend.Pages.States
{
    [Authorize(Roles = "Admin")]
    public partial class StateDetails
    {
        private State? state;
        private List<City>? cities;
        private int currentPage = 1;
        private int totalPages;

        [Parameter] public int StateId { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;

        [Parameter, SupplyParameterFromQuery] public string Page { get; set; } = string.Empty;
        [Parameter, SupplyParameterFromQuery] public string Filter { get; set; } = string.Empty;
        [Parameter, SupplyParameterFromQuery] public int RecordsNumber { get; set; } = 10;

        //[CascadingParameter] IModalService Modal { get; set; } = default!;


        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
        }

       

        private async Task DeleteAsync(City city)
        {
            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmaci�n",
                Text = $"�Realmente deseas eliminar la ciudad? {city.Name}",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
                CancelButtonText = "No",
                ConfirmButtonText = "Si"
            });

            var confirm = string.IsNullOrEmpty(result.Value);
            if (confirm)
            {
                return;
            }

            var responseHttp = await Repository.DeleteAsync<City>($"/api/cities/{city.Id}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode != HttpStatusCode.NotFound)
                {
                    var message = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                    return;
                }
            }

            await LoadAsync();
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro borrado con �xito.");
        }

        private async Task SelectedPageAsync(int page)
        {
            currentPage = page;
            await LoadAsync(page);
        }

        private async Task SelectedRecordsNumberAsync(int recordsnumber)
        {
            RecordsNumber = recordsnumber;
            int page = 1;
            await LoadAsync(page);
            await SelectedPageAsync(page);
        }

        private async Task LoadAsync(int page = 1)
        {

            if (!string.IsNullOrWhiteSpace(Page))
            {
                page = Convert.ToInt32(Page);
            }

            var ok = await LoadStateAsync();
            if (ok)
            {
                ok = await LoadCitiesAsync(page);
                if (ok)
                {
                    await LoadPagesAsync();
                }
            }
        }

        private async Task LoadPagesAsync()
        {

            ValidateRecordsNumber(RecordsNumber);
            var url = $"api/cities/totalPages?id={StateId}&recordsnumber={RecordsNumber}";


            //var url = $"api/cities/totalPages?id={StateId}";
            if (!string.IsNullOrEmpty(Filter))
            {
                url += $"&filter={Filter}";
            }


            var responseHttp = await Repository.GetAsync<int>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            totalPages = responseHttp.Response;
        }

        private async Task<bool> LoadCitiesAsync(int page)
        {
            ValidateRecordsNumber(RecordsNumber);
            var url = $"api/cities?id={StateId}&page={page}&recordsnumber={RecordsNumber}";

            //var url = $"api/cities?id={StateId}&page={page}";
            if (!string.IsNullOrEmpty(Filter))
            {
                url += $"&filter={Filter}";
            }


            var responseHttp = await Repository.GetAsync<List<City>>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return false;
            }
            cities = responseHttp.Response;
            return true;
        }

        private async Task<bool> LoadStateAsync()
        {
            var responseHttp = await Repository.GetAsync<State>($"api/states/{StateId}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/countries");
                    return false;
                }

                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return false;
            }
            state = responseHttp.Response;
            return true;
        }


        //private async Task ShowModalAsync(int id = 0, bool isEdit = false)
        //{
        //    IModalReference modalReference;

        //    if (isEdit)
        //    {
        //        modalReference = Modal.Show<CityEdit>(string.Empty, new ModalParameters().Add("CityId", id));
        //    }
        //    else
        //    {
        //        modalReference = Modal.Show<CityCreate>(string.Empty, new ModalParameters().Add("StateId", StateId));
        //    }

        //    var result = await modalReference.Result;
        //    if (result.Confirmed)
        //    {
        //        await LoadAsync();
        //    }
        //}



        private void ValidateRecordsNumber(int recordsnumber)
        {
            if (recordsnumber == 0)
            {
                RecordsNumber = 10;
            }
        }



        private async Task CleanFilterAsync()
        {
            Filter = string.Empty;
            await ApplyFilterAsync();
        }

        private async Task ApplyFilterAsync()
        {
            int page = 1;
            await LoadAsync(page);
            await SelectedPageAsync(page);
        }


        private async Task FilterCallBack(string filter)
        {
            Filter = filter;
            await ApplyFilterAsync();
            StateHasChanged();
        }

    }
}
