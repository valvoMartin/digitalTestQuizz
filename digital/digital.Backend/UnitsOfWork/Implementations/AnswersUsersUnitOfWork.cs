using digital.Backend.Repositories.Interfaces;
using digital.Backend.UnitsOfWork.Interfaces;
using digital.Shared.DTOs;
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





        //public async Task<ActionResponse<Question>> GetLastQuestionByUserId(int userId) => await _answersUsersRepository.GetLastQuestionByUserId(userId);

        public async Task<ActionResponse<AnswerUser>> SaveUserAnswerAsync(string email, int questionId, int answerId, bool isLast) => await _answersUsersRepository.SaveUserAnswerAsync(email, questionId, answerId, isLast);

        public async Task<ActionResponse<LastQuestionDTO>> GetLastQuestionAndAllQuestionsAsync(string email) => await _answersUsersRepository.GetLastQuestionAndAllQuestionsAsync(email);

        public async Task<ActionResponse<int>> GetCurrentTestNumberAsync(string email) => await _answersUsersRepository.GetCurrentTestNumberAsync(email);

        public async Task<ActionResponse<bool>> IsUserTestActiveAsync(string email) => await _answersUsersRepository.IsUserTestActiveAsync(email);

    }
}
