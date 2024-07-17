using digital.Backend.Data;
using digital.Backend.Repositories.Interfaces;
using digital.Shared.Entities.Test;
using digital.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace digital.Backend.Repositories.Implementations
{
    public class AnswersUsersRepository : GenericRepository<AnswerUser>, IAnswersUsersRepository
    {

        private readonly DataContext _context;

        public AnswersUsersRepository(DataContext context) : base(context)
        {
            _context = context;
        }


        
        public async Task<ActionResponse<Question?>> GetLastQuestionByUserId(int userId)
        {
            var lastAnswerUser = await _context.AnswersUsers
               .Where(au => au.UserId == userId)
               .OrderByDescending(au => au.DateFinished)
               .FirstOrDefaultAsync();

            

            if (lastAnswerUser == null)
            {
                return new ActionResponse<Question?>
                {
                    WasSuccess = true,
                    Result = null,
                    Message = "No answers found for the given user."
                };
            }

            var lastQuestion = await _context.Questions
                .Include(q => q.Answers)
                .Include(q => q.AnswerUsers)
                .FirstOrDefaultAsync(q => q.Id == lastAnswerUser.QuestionId);


            return new ActionResponse<Question?>
            {
                WasSuccess = true,
                Result = lastQuestion
            };
        }
    }
}
