using digital.Shared.DTOs;
using digital.Shared.Entities.Test;
using digital.Shared.Responses;

namespace digital.Backend.UnitsOfWork.Interfaces
{
    public interface IAnswersUsersUnitOfWork
    {
        Task<ActionResponse<IEnumerable<AnswerUser>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);



        Task<ActionResponse<AnswerUser>> SaveUserAnswerAsync(string email, int questionId, int answerId, bool isLast);
        //Task<ActionResponse<Question>> GetLastQuestionByUserId(int userId);

        Task<ActionResponse<LastQuestionDTO>> GetLastQuestionAndAllQuestionsAsync(string email);

        Task<ActionResponse<int>> GetCurrentTestNumberAsync(string email);

        Task<ActionResponse<bool>> IsUserTestActiveAsync(string email);

    }
}
