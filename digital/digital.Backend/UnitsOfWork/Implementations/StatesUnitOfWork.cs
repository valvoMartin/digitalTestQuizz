using digital.Backend.Repositories.Interfaces;
using digital.Backend.UnitsOfWork.Interfaces;
using digital.Shared.DTOs;
using digital.Shared.Entities;
using digital.Shared.Responses;

namespace digital.Backend.UnitsOfWork.Implementations
{
    public class StatesUnitOfWork : GenericUnitOfWork<State>, IStatesUnitOfWork
    {
        private readonly IStatesRepository _statesRepository;

        public StatesUnitOfWork(IGenericRepository<State> repository, IStatesRepository statesRepository) : base(repository)
        {
            _statesRepository = statesRepository;
        }

        public override async Task<ActionResponse<IEnumerable<State>>> GetAsync() => await _statesRepository.GetAsync();

        public override async Task<ActionResponse<State>> GetAsync(int id) => await _statesRepository.GetAsync(id);



        public override async Task<ActionResponse<IEnumerable<State>>> GetAsync(PaginationDTO pagination) => await _statesRepository.GetAsync(pagination);

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _statesRepository.GetTotalPagesAsync(pagination);


        public async Task<IEnumerable<State>> GetComboAsync(int countryId) => await _statesRepository.GetComboAsync(countryId);


    }
}



