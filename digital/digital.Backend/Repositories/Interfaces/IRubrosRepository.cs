using digital.Shared.Entities.Companies;

namespace digital.Backend.Repositories.Interfaces
{
    public interface IRubrosRepository
    {
        Task<IEnumerable<Sector>> GetSectorsByRubroAsync(int rubroId);
    }
}
