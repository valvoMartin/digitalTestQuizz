using digital.Backend.Data;
using digital.Backend.Repositories.Interfaces;
using digital.Backend.UnitsOfWork.Interfaces;
using digital.Shared.Entities.Companies;
using Microsoft.EntityFrameworkCore;

namespace digital.Backend.Repositories.Implementations
{
    public class RubrosRepository : GenericRepository<Rubro>, IRubrosRepository
    {
       private readonly DataContext _context;

        public RubrosRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sector>> GetSectorsByRubroAsync(int rubroId)
        {
            return await _context.Sectors
                .Where(s => s.RubroId == rubroId)
                .OrderBy(s => s.Name)
                .ToListAsync();
        }
    }
}
