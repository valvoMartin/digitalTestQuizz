using digital.Shared.DTOs;
using digital.Shared.Entities;
using digital.Shared.Responses;

namespace digital.Backend.UnitsOfWork.Interfaces
{
    public interface IStatesUnitOfWork
    {
        Task<ActionResponse<State>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<State>>> GetAsync();



        Task<ActionResponse<IEnumerable<State>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);


        Task<IEnumerable<State>> GetComboAsync(int countryId);



    }
}
