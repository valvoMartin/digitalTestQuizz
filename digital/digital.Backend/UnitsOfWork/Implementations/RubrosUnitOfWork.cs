using digital.Backend.Repositories.Interfaces;
using digital.Backend.UnitsOfWork.Interfaces;
using digital.Shared.Entities.Companies;
using digital.Shared.Entities.Countries;

namespace digital.Backend.UnitsOfWork.Implementations
{
    public class RubrosUnitOfWork : GenericUnitOfWork<Rubro>, IRubrosUnitOfWork
    {
        private readonly IRubrosRepository _rubrosRepository;

        public RubrosUnitOfWork(IGenericRepository<Rubro> repository, IRubrosRepository rubrosRepository) : base(repository)
        {
            _rubrosRepository = rubrosRepository;
        }

        public async Task<IEnumerable<Sector>> GetSectorsByRubroAsync(int rubroId) => await _rubrosRepository.GetSectorsByRubroAsync(rubroId);
    }
}
