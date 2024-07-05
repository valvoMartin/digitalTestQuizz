using digital.Shared.DTOs;
using digital.Shared.Entities.Countries;
using digital.Shared.Responses;

namespace digital.Backend.Repositories.Interfaces
{
    public interface ICitiesRepository
    {
        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<ActionResponse<IEnumerable<City>>> GetAsync(PaginationDTO pagination);


    
        Task<IEnumerable<City>> GetComboAsync(int stateId);
    
    }
}
