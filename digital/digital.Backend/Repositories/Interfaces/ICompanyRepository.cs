using digital.Backend.Repositories.Implementations;
using digital.Shared.DTOs;
using digital.Shared.Entities;
using digital.Shared.Responses;

namespace digital.Backend.Repositories.Interfaces
{
    public interface ICompanyRepository
    {
        Task<ActionResponse<Company>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Company>>> GetAsync(PaginationDTO pagination);



        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);



        //TODO: Agregar mas metodos a COMPAÑIAS

        Task<ActionResponse<Company>> AddFullAsync(Company company);

        Task<ActionResponse<Company>> UpdateFullAsync(Company product);

    }
}
