using digital.Shared.DTOs;
using digital.Shared.Entities.Test;
using digital.Shared.Responses;

namespace digital.Backend.Repositories.Interfaces
{
    public interface IAnswersRepository
    {

        Task<ActionResponse<IEnumerable<Answer>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);



        Task<ActionResponse<Answer>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Answer>>> GetAsync();



        Task<IEnumerable<Answer>> GetAnswersByQuestionAsync(int questionId);
    }
}
