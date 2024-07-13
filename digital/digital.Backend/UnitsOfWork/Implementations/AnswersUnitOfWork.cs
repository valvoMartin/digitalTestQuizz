using digital.Backend.Repositories.Interfaces;
using digital.Backend.UnitsOfWork.Interfaces;
using digital.Shared.DTOs;
using digital.Shared.Entities.Companies;
using digital.Shared.Entities.Countries;
using digital.Shared.Entities.Test;
using digital.Shared.Responses;

namespace digital.Backend.UnitsOfWork.Implementations
{
    public class AnswersUnitOfWork : GenericUnitOfWork<Answer>, IAnswersUnitOfWork
    {
        private readonly IAnswersRepository _answersRepository;

        public AnswersUnitOfWork(IGenericRepository<Answer> repository, IAnswersRepository answersRepository) : base(repository)
        {
            _answersRepository = answersRepository;
        }



        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _answersRepository.GetTotalPagesAsync(pagination);

        public override async Task<ActionResponse<IEnumerable<Answer>>> GetAsync(PaginationDTO pagination) => await _answersRepository.GetAsync(pagination);




        public override async Task<ActionResponse<Answer>> GetAsync(int id) => await _answersRepository.GetAsync(id);
        public override async Task<ActionResponse<IEnumerable<Answer>>> GetAsync() => await _answersRepository.GetAsync();




        public async Task<IEnumerable<Answer>> GetAnswersByQuestionAsync(int questionId) => await _answersRepository.GetAnswersByQuestionAsync(questionId);

    }
}
