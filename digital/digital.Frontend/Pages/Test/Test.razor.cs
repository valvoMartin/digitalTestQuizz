using CurrieTechnologies.Razor.SweetAlert2;
using digital.Frontend.Repositories;
using digital.Shared.DTOs;
using digital.Shared.Entities.Test;
using Microsoft.AspNetCore.Components;
using System.Net;

namespace digital.Frontend.Pages.Test
{
    public partial class Test
    {

        private Dictionary<int, int?> selectedAnswers = new Dictionary<int, int?>();

        private List<Question>? questions;

        private int currentQuestionIndex = 0;
        private int? selectedAnswerId;

        private User? user;
        //private Guid testSessionId;

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;


        protected override async Task OnInitializedAsync()
        {

            //var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            //userId = authState.User.Identity.Name;
            //questions = await QuestionService.GetQuestionsAsync();
            //await LoadSelectedAnswerAsync();

            //await LoadUserAsync();
            //await LoadQuestionsAsync();
            await LoadLastAnswerAsync();
        }






        private async Task LoadQuestionsAsync()
        {

            var responseHttp = await Repository.GetAsync<List<Question>>("api/questions/full");
            if (responseHttp.Error)
            {

                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);

            }
            questions = responseHttp.Response;

        }



        private async Task LoadLastAnswerAsync()
        {
            await LoadUserAsync();

            if (user != null)
            {
                var responseHttp = await Repository.GetAsync<LastQuestionDTO>($"api/AnswersUsers/lastQuestion/{user.Email}");
                if (responseHttp.Error)
                {
                    var message = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                }

                questions = responseHttp.Response?.Questions;
                selectedAnswers = responseHttp.Response!.SelectedAnswers!;
                if(selectedAnswers == null)
                {
                     //testSessionId = Guid.NewGuid();
                }

                //currentQuestionIndex = responseHttp.Response!.LastQuestionId;
                //await LoadSelectedAnswerAsync();
            }
        }

        private async Task LoadSelectedAnswerAsync()
        {
            if (user == null || questions == null || !questions.Any()) return;

            var questionId = questions[currentQuestionIndex].Id;
            var answerResponse = await Repository.GetAsync<AnswerUser>($"api/answersusers/get?email={user.Email}&questionId={questionId}");

            if (!answerResponse.Error)
            {
                var answerUser = answerResponse.Response;
                selectedAnswerId = answerUser?.AnswerId;
            }
            else
            {
                selectedAnswerId = null;
            }
        }



        private async Task LoadUserAsync()
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
            user = responseHttp.Response;
        }



        private async Task SelectAnswerAsync(int answerId)
        {
            if (currentQuestionIndex >= 0 && currentQuestionIndex < questions!.Count)
            {
                var questionId = questions[currentQuestionIndex].Id;
                selectedAnswers[questionId] = answerId;
                selectedAnswerId = answerId;
            }
        }

      

        private async Task SaveUserAnswerAsync(int questionId, int answerId, bool isLastQuestion)
        {
            if (selectedAnswers.TryGetValue(questionId, out var currentAnswerId) && currentAnswerId != answerId)
            {
                var request = new
                {
                    Email = user!.Email,
                    QuestionId = questions![currentQuestionIndex].Id,
                    AnswerId = answerId,
                    IsLast = isLastQuestion,
                    //testSessionId = testSessionId

                };

                var response = await Repository.PostAsync("api/answersusers/save", request);

                if (response.Error)
                {
                    var message = await response.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                }
            }
            
        }




        private async Task NextQuestion()
        {

            if (selectedAnswers.TryGetValue(questions[currentQuestionIndex].Id, out var answerId))
            {
                var isLastQuestion = currentQuestionIndex == questions!.Count - 1;
                await SaveUserAnswerAsync(questions[currentQuestionIndex].Id, (int)answerId!, isLastQuestion);
            }

            // Guarda la respuesta seleccionada
            if (selectedAnswerId.HasValue)
            {
                var isLastQuestion = currentQuestionIndex == questions!.Count - 1;
                await SaveUserAnswerAsync(questions[currentQuestionIndex].Id, (int)answerId!, isLastQuestion);
            }

            // Avanza a la siguiente pregunta
            if (currentQuestionIndex < questions!.Count - 1)
            {
                currentQuestionIndex++;
                //await LoadSelectedAnswerAsync(); // Carga la respuesta seleccionada si existe
                selectedAnswerId = selectedAnswers.TryGetValue(questions[currentQuestionIndex].Id, out var selectedAnswer) ? selectedAnswer : null;
            }
        }






        private async Task PreviousQuestion()
        {

            if (selectedAnswers.TryGetValue(questions[currentQuestionIndex].Id, out var answerId))
            {
                var isLastQuestion = currentQuestionIndex == questions!.Count - 1;
                await SaveUserAnswerAsync(questions[currentQuestionIndex].Id, (int)answerId!, isLastQuestion);
            }

            if (currentQuestionIndex > 0)
            {
                // Guarda la respuesta seleccionada en la base de datos
                //if (selectedAnswerId.HasValue)
                //{
                //    await SaveUserAnswerAsync(selectedAnswerId.Value, false);
                //}

                currentQuestionIndex--;
                selectedAnswerId = selectedAnswers.TryGetValue(questions[currentQuestionIndex].Id, out var selectedAnswer) ? selectedAnswer : null;

                //await LoadSelectedAnswerAsync(); // Carga la respuesta seleccionada si existe
            }
        }


    }
}