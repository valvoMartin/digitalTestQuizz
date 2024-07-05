//using Blazored.Modal;
//using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using digital.Frontend.Repositories;
using digital.Frontend.Shared;
using digital.Shared.Entities.Countries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace digital.Frontend.Pages.Cities
{
    [Authorize(Roles = "Admin")]
    public partial class CityCreate
    {
        private City city = new();
        private FormWithName<City>? cityForm;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        //[CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;

        [Parameter] public int StateId { get; set; }

        private async Task CreateAsync()
        {
            city.StateId = StateId;
            var responseHttp = await Repository.PostAsync("/api/cities", city);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
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
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro creado con éxito.");
        }

        private void Return()
        {
            cityForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo($"/states/details/{StateId}");
        }
    }
}
