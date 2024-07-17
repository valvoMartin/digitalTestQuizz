using digital.Backend.Repositories.Interfaces;
using digital.Backend.UnitsOfWork.Interfaces;
using digital.Shared.Entities.Test;
using digital.Shared.Responses;

namespace digital.Backend.UnitsOfWork.Implementations
{
    public class AnswersUsersUnitOfWork : GenericUnitOfWork<AnswerUser>, IAnswersUsersUnitOfWork
    {
        private readonly IAnswersUsersRepository _answersUsersRepository;

        public AnswersUsersUnitOfWork(IGenericRepository<AnswerUser> repository, IAnswersUsersRepository answersUserRepository) : base(repository)
        {
            _answersUsersRepository = answersUserRepository;
        }





        public async Task<ActionResponse<Question>> GetLastQuestionByUserId(int userId) => await _answersUsersRepository.GetLastQuestionByUserId(userId);
       
    }
}
