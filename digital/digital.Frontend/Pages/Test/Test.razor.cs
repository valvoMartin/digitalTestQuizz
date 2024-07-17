using CurrieTechnologies.Razor.SweetAlert2;
using digital.Frontend.Repositories;
using digital.Shared.Entities;
using digital.Shared.Entities.Test;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net;

namespace digital.Frontend.Pages.Test
{
    public partial class Test
    {
        private List<Question>? questions;
        private int currentQuestionIndex = 0;
        //private string selectedAnswer;
        private int? selectedAnswerId;
        //private string? userId;

        private User? user;

        //[Parameter, SupplyParameterFromQuery] public string Page { get; set; } = string.Empty;
        //[Parameter, SupplyParameterFromQuery] public string Filter { get; set; } = string.Empty;
        //[Parameter, SupplyParameterFromQuery] public int RecordsNumber { get; set; } = 10;

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;


        protected override async Task OnInitializedAsync()
        {

            //var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            //userId = authState.User.Identity.Name;
            //questions = await QuestionService.GetQuestionsAsync();
            //await LoadSelectedAnswerAsync();
            await LoadUserAsync();

            await LoadQuestionsAsync();
        }



        private async Task LoadUserAsync()
        {
            var responseHttp = await Repository.GetAsync<User>($"/api/accounts");
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
            user = responseHttp.Response;
        }
        // private async Task LoadAsync(int page = 1)
        // {

        //     if (!string.IsNullOrWhiteSpace(Page))
        //     {
        //         page = Convert.ToInt32(Page);
        //     }

        //     var ok = await LoadQuestionsAsync();
        //     if (ok)
        //     {
        //         await LoadPagesAsync();
        //     }
        // }


        // private async Task LoadPagesAsync()
        // {
        //     ValidateRecordsNumber(RecordsNumber);
        //     var url = $"api/companies/totalPages?recordsnumber={RecordsNumber}";
        //     if (!string.IsNullOrEmpty(Filter))
        //     {
        //         url += $"&filter={Filter}";
        //     }

        //     var response = await Repository.GetAsync<int>(url);
        //     if (response.Error)
        //     {
        //         var message = await response.GetErrorMessageAsync();
        //         await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
        //         return;
        //     }
        //     totalPages = response.Response;
        // }

        // private void ValidateRecordsNumber(int recordsnumber)
        // {
        //     if (recordsnumber == 0)
        //     {
        //         RecordsNumber = 10;
        //     }
        // }


        private async Task LoadQuestionsAsync()
        {
            // ValidateRecordsNumber(RecordsNumber);
            // var url = $"api/cities?id={StateId}&page={page}&recordsnumber={RecordsNumber}";

            // //var url = $"api/cities?id={StateId}&page={page}";
            // if (!string.IsNullOrEmpty(Filter))
            // {
            //     url += $"&filter={Filter}";
            // }


            var responseHttp = await Repository.GetAsync<List<Question>>("api/questions/full");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);

            }
            questions = responseHttp.Response;

        }




        private void SelectAnswer(int answer)
        {
            selectedAnswerId = answer;
        }

        private void NextQuestion()
        {
            if (currentQuestionIndex < questions!.Count - 1)
            {
                currentQuestionIndex++;
                selectedAnswerId = null;

                //await LoadSelectedAnswerAsync();
            }
        }

        private void PreviousQuestion()
        {
            if (currentQuestionIndex > 0)
            {
                currentQuestionIndex--;
                selectedAnswerId = null;

                //await LoadSelectedAnswerAsync();
            }
        }

        //private async Task LoadSelectedAnswerAsync()
        //{
        //    if (questions != null && questions.Count > 0)
        //    {
        //        selectedAnswerId = await Repository.GetUserAnswerAsync(userId, questions[currentQuestionIndex].Id);
        //    }
        //}
    }
}