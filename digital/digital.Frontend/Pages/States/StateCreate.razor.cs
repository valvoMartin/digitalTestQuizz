//using Blazored.Modal;
//using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using digital.Frontend.Repositories;
using digital.Frontend.Shared;
using digital.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace digital.Frontend.Pages.States
{
    [Authorize(Roles = "Admin")]
    public partial class StateCreate
    {

        private State state = new();
        private FormWithName<State>? stateForm;

        [Parameter] public int CountryId { get; set; }
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        //[CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;


        private async Task CreateAsync()
        {
            state.CountryId = CountryId;
            var responseHttp = await Repository.PostAsync("/api/states", state);
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
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro creado con �xito.");
        }

        private void Return()
        {
            stateForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo($"/countries/details/{CountryId}");
        }
    }
}
