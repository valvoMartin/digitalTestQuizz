using digital.Backend.Repositories.Interfaces;
using digital.Backend.UnitsOfWork.Interfaces;
using digital.Shared.DTOs;
using digital.Shared.Entities.Test;
using digital.Shared.Responses;

namespace digital.Backend.UnitsOfWork.Implementations
{
    public class QuestionsUnitOfWork : GenericUnitOfWork<Question>, IQuestionsUnitOfWork
    {
        private readonly IQuestionsRepository _questionsRepository;

        public QuestionsUnitOfWork(IGenericRepository<Question> repository, IQuestionsRepository questionsRepository) : base(repository)
        {
            _questionsRepository = questionsRepository;
        }



        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _questionsRepository.GetTotalPagesAsync(pagination);

        public override async Task<ActionResponse<IEnumerable<Question>>> GetAsync(PaginationDTO pagination) => await _questionsRepository.GetAsync(pagination);


        public override async Task<ActionResponse<Question>> GetAsync(int id) => await _questionsRepository.GetAsync(id);
        public override async Task<ActionResponse<IEnumerable<Question>>> GetAsync() => await _questionsRepository.GetAsync();



    }
}
