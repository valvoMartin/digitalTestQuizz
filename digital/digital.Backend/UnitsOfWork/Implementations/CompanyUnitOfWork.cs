﻿using digital.Backend.Repositories.Implementations;
using digital.Backend.Repositories.Interfaces;
using digital.Backend.UnitsOfWork.Interfaces;
using digital.Shared.DTOs;
using digital.Shared.Entities;
using digital.Shared.Responses;
using Microsoft.AspNetCore.Identity;

namespace digital.Backend.UnitsOfWork.Implementations
{
    public class CompanyUnitOfWork : GenericUnitOfWork<Company>, ICompanyUnitOfWork
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyUnitOfWork(IGenericRepository<Company> repository, ICompanyRepository companyRepository) : base(repository)
        {
            _companyRepository = companyRepository;
        }



        public override async Task<ActionResponse<IEnumerable<Company>>> GetAsync(PaginationDTO pagination) => await _companyRepository.GetAsync(pagination);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _companyRepository.GetTotalPagesAsync(pagination);

        public override async Task<ActionResponse<Company>> GetAsync(int id) => await _companyRepository.GetAsync(id);

        public async Task<ActionResponse<Company>> AddFullAsync(Company company) => await _companyRepository.AddFullAsync(company);

        public async Task<ActionResponse<Company>> UpdateFullAsync(Company company) => await _companyRepository.UpdateFullAsync(company);

        public async Task<ActionResponse<Company>> DeleteAsync(int id) => await _companyRepository.DeleteAsync(id);
    }


}


