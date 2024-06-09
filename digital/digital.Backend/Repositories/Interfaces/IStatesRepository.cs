using digital.Shared.Entities;
using digital.Shared.Responses;

namespace digital.Backend.Repositories.Interfaces
{
    public interface IStatesRepository
    {
        Task<ActionResponse<State>> GetAsync(int id);

        Task<ActionResponse<IEnumerable<State>>> GetAsync();

    }
}
