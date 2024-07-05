using digital.Backend.Data;
using digital.Backend.Helpers;
using digital.Backend.Repositories.Interfaces;
using digital.Shared.DTOs;
using digital.Shared.Entities;
using digital.Shared.Responses;
using Microsoft.EntityFrameworkCore;

namespace digital.Backend.Repositories.Implementations
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context) : base(context)
        {
            _context = context;
        }





        public override async Task<ActionResponse<IEnumerable<Category>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Categories
                .Include(x => x.Country)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<Category>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.Name)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Categories.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            double count = await queryable.CountAsync();
            int totalPages = (int)Math.Ceiling(count / pagination.RecordsNumber);
            return new ActionResponse<int>
            {
                WasSuccess = true,
                Result = totalPages
            };
        }

        public override async Task<ActionResponse<Category>> GetAsync(int id)
        {
            var product = await _context.Categories
                .Include(x => x.Country)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
            {
                return new ActionResponse<Category>
                {
                    WasSuccess = false,
                    Message = "Producto no existe"
                };
            }

            return new ActionResponse<Category>
            {
                WasSuccess = true,
                Result = product
            };
        }



        public async Task<ActionResponse<Category>> AddFullAsync(Category category)
        {
            try
            {
                var newObjet = new Category
                {
                    Name = category.Name,
                    Country = category.Country,
                    CountryId = category.CountryId,
                    EmployesLimit = category.EmployesLimit,
                    RevenueLimit = category.RevenueLimit,

                    //TODO: Estara bien agregar la coleccion de companias?
                    //Companies = category.Companies
                };

                

                _context.Add(newObjet);
                await _context.SaveChangesAsync();
                return new ActionResponse<Category>
                {
                    WasSuccess = true,
                    Result = newObjet
                };
            }
            catch (DbUpdateException)
            {
                return new ActionResponse<Category>
                {
                    WasSuccess = false,
                    Message = "Ya existe esta Categoria con el mismo nombre."
                };
            }
            catch (Exception exception)
            {
                return new ActionResponse<Category>
                {
                    WasSuccess = false,
                    Message = exception.Message
                };
            }

        }

        public async Task<ActionResponse<Category>> UpdateFullAsync(Category category)
        {
            try
            {
                var objet = await _context.Categories
                    .Include(x => x.Country)
                    .FirstOrDefaultAsync(x => x.Id == category.Id);

                if (objet == null)
                {
                    return new ActionResponse<Category>
                    {
                        WasSuccess = false,
                        Message = "Categoria no existe"
                    };
                }

                objet.Name = category.Name;
                objet.Country = category.Country;
                objet.CountryId = category.CountryId;
                objet.EmployesLimit = category.EmployesLimit;
                objet.RevenueLimit = category.RevenueLimit;

                

                _context.Update(objet);
                await _context.SaveChangesAsync();
                return new ActionResponse<Category>
                {
                    WasSuccess = true,
                    Result = objet
                };
            }
            catch (DbUpdateException)
            {
                return new ActionResponse<Category>
                {
                    WasSuccess = false,
                    Message = "Ya existe una Categoria con el mismo nombre."
                };
            }
            catch (Exception exception)
            {
                return new ActionResponse<Category>
                {
                    WasSuccess = false,
                    Message = exception.Message
                };
            }
        }

    }
}
