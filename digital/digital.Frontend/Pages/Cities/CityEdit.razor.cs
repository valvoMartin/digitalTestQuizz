//using Blazored.Modal;
//using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using digital.Frontend.Repositories;
using digital.Frontend.Shared;
using digital.Shared.Entities.Countries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System.Net;

namespace digital.Frontend.Pages.Cities
{
    [Authorize(Roles = "Admin")]
    public partial class CityEdit
    {
        private City? city;
        private FormWithName<City>? cityForm;

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        //[CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;

        [Parameter] public int CityId { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var responseHttp = await Repository.GetAsync<City>($"/api/cities/{CityId}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    Return();
                }
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            city = responseHttp.Response;
        }

        private async Task SaveAsync()
        {
            var response = await Repository.PutAsync($"/api/cities", city);
            if (response.Error)
            {
                var message = await response.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            //await BlazoredModal.CloseAsync(ModalResult.Ok());
            Return();

            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Cambios guardados con �xito.");
        }

        private void Return()
        {
            cityForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo($"/states/details/{city!.StateId}");
        }
    }
}
    