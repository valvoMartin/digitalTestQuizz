using digital.Backend.Data;
using digital.Backend.Helpers;
using digital.Backend.Repositories.Interfaces;
using digital.Shared.DTOs;
using digital.Shared.Entities.Companies;
using digital.Shared.Entities.Test;
using digital.Shared.Responses;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace digital.Backend.Repositories.Implementations
{
    public class QuestionsRepository : GenericRepository<Question>, IQuestionsRepository
    {
        private readonly DataContext _context;

        public QuestionsRepository(DataContext context) : base(context)
        {
            _context = context;
        }


        public async override Task<ActionResponse<IEnumerable<Question>>> GetAsync(PaginationDTO pagination)
        {
            //TODO: Include(el area por Id y el TipoPregunta por id)
            var queryable = _context.Questions
                .Include(x => x.Answers)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Text.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<Question>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.Id)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public async override Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            //TODO: Include(el area por Id y el TipoPregunta por id)
            var queryable = _context.Questions
                .Include(x => x.Answers)
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


        public override async Task<ActionResponse<IEnumerable<Question>>> GetAsync()
        {
            //TODO: Include(el area por Id y el TipoPregunta por id)
          



            var queryable = _context.Questions
                .Include(x => x.Answers)
                //.Include(x => x.AnswerUsers)
                .AsQueryable();


            return new ActionResponse<IEnumerable<Question>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.Id)
                    .ToListAsync()
            };
        }

        

    }
}
