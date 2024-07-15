﻿using digital.Shared.DTOs;
using digital.Shared.Entities.Companies;
using digital.Shared.Responses;
using Microsoft.AspNetCore.Identity;

namespace digital.Backend.UnitsOfWork.Interfaces
{
    public interface ICompaniesUnitOfWork
    {
        Task<ActionResponse<IEnumerable<Company>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);




        Task<ActionResponse<Company>> GetAsync(int id);

        Task<ActionResponse<Company>> AddFullAsync(Company company);

        Task<ActionResponse<Company>> UpdateFullAsync(Company company);

        Task<ActionResponse<Company>> DeleteAsync(int id);

    }
}