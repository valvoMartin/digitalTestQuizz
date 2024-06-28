//using Blazored.Modal;
//using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using digital.Frontend.Repositories;
using digital.Frontend.Services;
using digital.Shared.DTOs;
using Microsoft.AspNetCore.Components;

namespace digital.Frontend.Pages.Auth
{
    public partial class Login
    {
        private LoginDTO loginDTO = new();
        private bool wasClose;

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private ILoginService LoginService { get; set; } = null!;

        //[CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;
        //[CascadingParameter] public IModalService Modal { get; set; } = default!;

        private async Task LoginAsync()
        {
            //if (wasClose)
            //{
            //    NavigationManager.NavigateTo("/");
            //    return;
            //}

            var responseHttp = await Repository.PostAsync<LoginDTO, TokenDTO>("/api/accounts/Login", loginDTO);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            await LoginService.LoginAsync(responseHttp.Response!.Token);
            NavigationManager.NavigateTo("/");
        }

        //private async Task CloseModalAsync()
        //{
        //    wasClose = true;
        //    await BlazoredModal.CloseAsync(ModalResult.Ok());
        //}

    }
}
