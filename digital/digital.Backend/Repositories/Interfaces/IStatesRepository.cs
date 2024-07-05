﻿using digital.Shared.DTOs;
using digital.Shared.Entities.Countries;
using digital.Shared.Responses;

namespace digital.Backend.Repositories.Interfaces
{
    public interface IStatesRepository
    {
        Task<ActionResponse<IEnumerable<State>>> GetAsync(PaginationDTO pagination);

        Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination);




        Task<ActionResponse<State>> GetAsync(int id);
        
        Task<ActionResponse<IEnumerable<State>>> GetAsync();

        Task<IEnumerable<State>> GetComboAsync(int countryId);

    }
}
