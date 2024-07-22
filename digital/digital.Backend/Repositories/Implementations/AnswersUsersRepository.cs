using digital.Backend.Data;
using digital.Backend.Repositories.Interfaces;
using digital.Shared.DTOs;
using digital.Shared.Entities.Test;
using digital.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace digital.Backend.Repositories.Implementations
{
    public class AnswersUsersRepository : GenericRepository<AnswerUser>, IAnswersUsersRepository
    {

        private readonly DataContext _context;

        public AnswersUsersRepository(DataContext context) : base(context)
        {
            _context = context;
        }


        //public async Task<ActionResponse<Question?>> GetLastQuestionByUserId(int userId)
        //{
        //    var lastAnswerUser = await _context.AnswersUsers
        //       .Where(au => au.UserId == userId)
        //       .OrderByDescending(au => au.Id)
        //       .FirstOrDefaultAsync();



        //    if (lastAnswerUser == null)
        //    {
        //        return new ActionResponse<Question?>
        //        {
        //            WasSuccess = true,
        //            Result = null,
        //            Message = "El usuario no tiene respuestas asociadas aùn."
        //        };
        //    }

        //    var lastQuestion = await _context.Questions
        //        .Include(q => q.Answers)
        //        //.Include(q => q.AnswerUsers)
        //        .FirstOrDefaultAsync(q => q.Id == lastAnswerUser.QuestionId);


        //    return new ActionResponse<Question?>
        //    {
        //        WasSuccess = true,
        //        Result = lastQuestion
        //    };
        //}



        public async Task<ActionResponse<AnswerUser>> SaveUserAnswerAsync(string email, int questionId, int answerId, bool isLast)
        {
            try
            {
                var answerUser = new AnswerUser
                {
                    Email = email,
                    QuestionId = questionId,
                    AnswerId = answerId,
                };
                if (isLast) 
                {
                    answerUser.DateFinished = DateTime.UtcNow;
                }

                _context.AnswersUsers.Add(answerUser);
                await _context.SaveChangesAsync();

                return new ActionResponse<AnswerUser>
                {
                    WasSuccess = true,
                    Result = answerUser
                };
            }
            catch (Exception exception)
            {
                return new ActionResponse<AnswerUser>
                {
                    WasSuccess = false,
                    Message = exception.Message
                };
            }
        }



        //public async Task<ActionResponse<LastQuestionDTO>> GetLastQuestionAndAllQuestionsAsync(string email)
        //{
        //    try
        //    {
        //        var lastAnswerUser = await _context.AnswersUsers
        //       .Include(au => au.User)
        //       .Where(au => au.User!.Email!.ToLower() == email)
        //       .OrderByDescending(au => au.Id)
        //       .FirstOrDefaultAsync();

        //        var lastQuestionId = lastAnswerUser?.QuestionId ?? 0;

        //        var allQuestions = await _context.Questions
        //            .Include(q => q.Answers)
        //            .ToListAsync();


        //        return new ActionResponse<LastQuestionDTO>
        //        {
        //            WasSuccess = true,
        //            Result = new LastQuestionDTO
        //            {
        //                LastQuestionId = lastQuestionId,
        //                Questions = allQuestions
        //            }
        //        };

        //    }
        //    catch (Exception exception)
        //    {

        //        throw new Exception($"Error retrieving last answer user: {exception.Message}", exception);
        //    }



        //}


        public async Task<ActionResponse<LastQuestionDTO>> GetLastQuestionAndAllQuestionsAsync(string email)
        {
            try
            {

                var allQuestions = await _context.Questions
                    .Include(q => q.Answers)
                    .ToListAsync();

                // Obtener todas las respuestas del usuario
                var userAnswers = await _context.AnswersUsers
                    .Where(au => au.User!.Email!.ToLower() == email.ToLower())
                    .ToListAsync();

                // Crear un diccionario con las respuestas seleccionadas por el usuario
                var selectedAnswers = userAnswers.ToDictionary(ua => ua.QuestionId, ua => (int?)ua.AnswerId);

               


                return new ActionResponse<LastQuestionDTO>
                {
                    WasSuccess = true,
                    Result = new LastQuestionDTO
                    {
                        Questions = allQuestions,
                        SelectedAnswers = selectedAnswers
                    }
                };

            }
            catch (Exception exception)
            {

                return new ActionResponse<LastQuestionDTO>
                {
                    WasSuccess = false,
                    Message = $"Error retrieving last answer user: {exception.Message}"
                };
            }
        }

        //public async Task<ActionResponse<int>> GetCurrentTestNumberAsync(string email)
        //{
        //    try
        //    {
        //        // Obtén el número de intento más alto registrado para el usuario
        //        var latestAttempt = await _context.AnswersUsers
        //            .Where(au => au.Email == email)
        //            .GroupBy(au => au.TestNumber)
        //            .OrderByDescending(g => g.Key)
        //            .Select(g => g.Key)
        //            .FirstOrDefaultAsync();

        //        // Si no hay intentos registrados, devuelve 0; de lo contrario, devuelve el número de intento más alto + 1
        //        //return latestAttempt.GetValueOrDefault() + 1;
        //        return;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejo de excepciones
        //        throw new Exception($"Error retrieving current test attempt number: {ex.Message}", ex);
        //    }

        //}

        public async Task<ActionResponse<bool>> IsUserTestActiveAsync(string email)
        {
            try
            {
                var activeTest = await _context.AnswersUsers
                    .Where(t => t.Email == email && t.DateFinished == null) // Cambia el criterio según tus necesidades
                    .FirstOrDefaultAsync();

                var isActive = activeTest != null;

                return new ActionResponse<bool>
                {
                    WasSuccess = true,
                    Result = isActive
                };
            }
            catch (Exception ex)
            {
                return new ActionResponse<bool>
                {
                    WasSuccess = false,
                    Message = $"Error checking active test: {ex.Message}"
                };
                // Manejo de errores
                //throw new Exception($"Error checking active test: {ex.Message}", ex);
            }
        }

        public async Task<ActionResponse<int>> GetCurrentTestNumberAsync(string email)
        {
            return new ActionResponse<int> {WasSuccess = true, Result=1 };
        }
    }
}  
        


