using digital.Shared.DTOs;
using digital.Shared.Entities;
using digital.Shared.Responses;
using Microsoft.AspNetCore.Identity;

namespace digital.Backend.UnitsOfWork.Interfaces
{
    public interface ICompanyUnitOfWork
    {
        Task<ActionResponse<Company>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<Company>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);

        Task<ActionResponse<Company>> AddFullAsync(Company company);

        Task<ActionResponse<Company>> UpdateFullAsync(Company company);

        Task<ActionResponse<Company>> DeleteAsync(int id);

    }
}
