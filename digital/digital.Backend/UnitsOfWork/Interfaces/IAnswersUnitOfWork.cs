using digital.Shared.DTOs;
using digital.Shared.Entities.Companies;
using digital.Shared.Entities.Countries;
using digital.Shared.Entities.Test;
using digital.Shared.Responses;

namespace digital.Backend.UnitsOfWork.Interfaces
{
    public interface IAnswersUnitOfWork
    {

        Task<ActionResponse<IEnumerable<Answer>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);



        Task<ActionResponse<Answer>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Answer>>> GetAsync();



        Task<IEnumerable<Answer>> GetAnswersByQuestionAsync(int questionId);

    }
}
