using digital.Shared.DTOs;
using digital.Shared.Entities.Companies;
using digital.Shared.Responses;

namespace digital.Backend.UnitsOfWork.Interfaces
{
    public interface ICategoriesUnitOfWork
    {
        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<ActionResponse<IEnumerable<Category>>> GetAsync(PaginationDTO pagination);





        Task<ActionResponse<Category>> GetAsync(int id);

        //Task<ActionResponse<IEnumerable<Category>>> GetAsync();

        Task<ActionResponse<Category>> AddFullAsync(Category company);

        Task<ActionResponse<Category>> UpdateFullAsync(Category company);

        Task<ActionResponse<Category>> DeleteAsync(int id);

    }
}
