//using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using digital.Frontend.Repositories;
using digital.Shared.DTOs;
using Microsoft.AspNetCore.Components;

namespace digital.Frontend.Pages.Auth
{
    public partial class ResetPassword
    {
        private ResetPasswordDTO resetPasswordDTO = new();
        private bool loading;

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Parameter, SupplyParameterFromQuery] public string Token { get; set; } = string.Empty;

        //[CascadingParameter] IModalService Modal { get; set; } = default!;

        private async Task ChangePasswordAsync()
        {
            resetPasswordDTO.Token = Token;
            loading = true;
            var responseHttp = await Repository.PostAsync("/api/accounts/ResetPassword", resetPasswordDTO);
            loading = false;
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                loading = false;
                return;
            }

            await SweetAlertService.FireAsync("Confirmaci�n", "Contrase�a cambiada con �xito, ahora puede ingresar con su nueva contrase�a.", SweetAlertIcon.Success);

            //Modal.Show<Login>();

            NavigationManager.NavigateTo("/Login");
        }
    }
}
