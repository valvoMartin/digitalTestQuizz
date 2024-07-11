using digital.Shared.Entities.Companies;

namespace digital.Backend.UnitsOfWork.Interfaces
{
    public interface IRubrosUnitOfWork
    {
        Task<IEnumerable<Sector>> GetSectorsByRubroAsync(int rubroId);
    }
}
