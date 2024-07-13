using digital.Backend.Data;
using digital.Backend.Helpers;
using digital.Backend.Repositories.Interfaces;
using digital.Shared.DTOs;
using digital.Shared.Entities.Test;
using digital.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace digital.Backend.Repositories.Implementations
{
    public class AnswersRepository : GenericRepository<Answer>, IAnswersRepository
    {
        private readonly DataContext _context;

        public AnswersRepository(DataContext context) : base(context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Answer>> GetAnswersByQuestionAsync(int questionId)
        {

            return await _context.Answers
                .Where(a => a.QuestionId == questionId)
                .OrderBy(a => a.Id)
                .ToListAsync();


        }

        public override async Task<ActionResponse<IEnumerable<Answer>>> GetAsync(PaginationDTO pagination)
        {
            //TODO: me da duda

            var queryable = _context.Answers
                .Where(x => x.QuestionId == pagination.Id)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Text.ToLower().Contains(pagination.Filter.ToLower()));
            }


            return new ActionResponse<IEnumerable<Answer>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.Id)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {


            var queryable = _context.Answers
                .Where(x => x.QuestionId == pagination.Id)
                .AsQueryable();


            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Text.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }

    }
}
