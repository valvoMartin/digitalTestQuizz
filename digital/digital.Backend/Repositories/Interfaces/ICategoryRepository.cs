using digital.Shared.DTOs;
using digital.Shared.Entities.Companies;
using digital.Shared.Responses;

namespace digital.Backend.Repositories.Interfaces
{
    public interface ICategoryRepository
    {

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<ActionResponse<IEnumerable<Category>>> GetAsync(PaginationDTO pagination);





        //TODO: Agregar mas metodos a Categoria
        Task<ActionResponse<Category>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Category>>> GetAsync();

        Task<ActionResponse<Category>> AddFullAsync(Category category);

        Task<ActionResponse<Category>> UpdateFullAsync(Category category);

        Task<ActionResponse<Category>> DeleteAsync(int id);
    }
}
