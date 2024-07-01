using digital.Shared.DTOs;
using digital.Shared.Entities;
using digital.Shared.Responses;
using Microsoft.AspNetCore.Identity;

namespace digital.Backend.UnitsOfWork.Interfaces
{
    public interface ICompanyUnitOfWork
    {
        Task<ActionResponse<IEnumerable<Company>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);



        Task<User> GetCompanyAsync(string cuit);

        Task<IdentityResult> AddCompanyAsync(Company user);

        


        Task<IdentityResult> UpdateCompanyAsync(Company user);
    }
}
