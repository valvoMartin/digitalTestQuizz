using digital.Shared.DTOs;
using digital.Shared.Entities.Test;
using digital.Shared.Responses;

namespace digital.Backend.Repositories.Interfaces
{
    public interface IQuestionsRepository
    {
        Task<ActionResponse<IEnumerable<Question>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);



        Task<ActionResponse<Question>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Question>>> GetAsync();
    }
}
