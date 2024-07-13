using CurrieTechnologies.Razor.SweetAlert2;
using digital.Frontend.Repositories;
using digital.Frontend.Shared;
using digital.Shared.Entities.Companies;
using digital.Shared.Entities.Countries;
using digital.Shared.Entities.Test;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace digital.Frontend.Pages.Questions
{
    [Authorize(Roles = "Admin")]
    public partial class QuestionEdit
    {
        private Question? question;
        private QuestionForm? questionForm;
        [Parameter] public int QuestionId { get; set; }


      
        private bool loading = true;
     
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;




        protected override async Task OnInitializedAsync()
        {

            await LoadQuestionAsync();
            //await LoadSubAreasAsync();

           
        }
        private async Task LoadQuestionAsync()
        {
            loading = true; 
            var responseHttp = await Repository.GetAsync<Question>($"api/questions/{QuestionId}");

            if (responseHttp.Error)
            {
                loading = false;
                if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/questions");
                }
                else
                {
                    var messageError = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", messageError, SweetAlertIcon.Error);
                }
                return;
            }
            
            loading = false;
            question = responseHttp.Response;
            
        }


        private async Task SaveAsync()
        {
            var responseHttp = await Repository.PutAsync($"/api/questions", question);
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
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Cambios guardados con éxito.");
        }

        private void Return()
        {
            //questionForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo("countries");
        }
      
    }
}