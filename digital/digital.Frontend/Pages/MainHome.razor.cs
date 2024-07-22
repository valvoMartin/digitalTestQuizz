using CurrieTechnologies.Razor.SweetAlert2;
using digital.Frontend.Repositories;
using digital.Shared.Entities.Test;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Orders.Frontend.Repositories;
using System.Net;

namespace digital.Frontend.Pages
{
    public partial class MainHome
    {
        public User? user;
        private bool isLoading = true; 


        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;


        private async Task LoadUserAsync()
        {
            try
            {
                var responseHttp = await Repository.GetAsync<User>($"api/accounts");
                if (responseHttp.Error)
                {
                    if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                    {
                        NavigationManager.NavigateTo("/");
                        return;
                    }
                    var messageError = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", messageError, SweetAlertIcon.Error);
                    return;
                }
                if (responseHttp.Response != null)
                {
                    user = responseHttp.Response;
                }
                else
                {
                    // Manejo en caso de que la respuesta sea nula
                    await SweetAlertService.FireAsync("Error", "Usuario no encontrado", SweetAlertIcon.Error);
                    NavigationManager.NavigateTo("/");
                }
                //user = responseHttp.Response;
            }
            catch (Exception ex)
            {
                await SweetAlertService.FireAsync("Error", ex.Message, SweetAlertIcon.Error);
            }
            finally
            {
                isLoading = false;
            }
        }
           
        

        protected async override Task OnInitializedAsync()
        {
            await LoadUserAsync();
        }

    }
}
