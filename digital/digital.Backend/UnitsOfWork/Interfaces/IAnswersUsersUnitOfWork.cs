using digital.Shared.DTOs;
using digital.Shared.Entities.Test;
using digital.Shared.Responses;

namespace digital.Backend.UnitsOfWork.Interfaces
{
    public interface IAnswersUsersUnitOfWork
    {
        Task<ActionResponse<IEnumerable<AnswerUser>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);




        Task<ActionResponse<Question>> GetLastQuestionByUserId(int userId);

    }
}
