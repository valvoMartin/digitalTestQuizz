using CurrieTechnologies.Razor.SweetAlert2;
using digital.Frontend.Repositories;
using digital.Shared.Entities.Companies;
using digital.Shared.Entities.Test;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace digital.Frontend.Pages.Questions
{
    [Authorize(Roles = "Admin")]
    public partial class QuestionCreate
    {


        private Question question = new();
        private QuestionForm? questionForm;
     
        private bool loading = true;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;


        protected override async Task OnInitializedAsync()
        {
            // TODO: CARGO LAS SUBAREAS
            //var httpActionResponse = await Repository.GetAsync<List<SubAreas>>("/api/categories/combo");
            loading = false;

            //if (httpActionResponse.Error)
            //{
            //    var message = await httpActionResponse.GetErrorMessageAsync();
            //    await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
            //    return;
            //}

            //nonSelectedCategories = httpActionResponse.Response!;
        }


        private async Task CreateAsync()
        {
            var httpActionResponse = await Repository.PostAsync("/api/questions", question);
            if (httpActionResponse.Error)
            {
                var message = await httpActionResponse.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            Return();
        }

        private void Return()
        {
            questionForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo($"/questions");
        }
    }


}