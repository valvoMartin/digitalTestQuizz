using digital.Backend.Repositories.Interfaces;
using digital.Backend.UnitsOfWork.Interfaces;
using digital.Shared.Entities;

namespace digital.Backend.UnitsOfWork.Implementations
{
    public class CompanyUnitOfWork : GenericUnitOfWork<City>, ICitiesUnitOfWork
    {
        public CompanyUnitOfWork(IGenericRepository<City> repository) : base(repository)
        {
        }

        public Task<IEnumerable<City>> GetComboAsync(int stateId)
        {
            throw new NotImplementedException();
        }
    }

}
