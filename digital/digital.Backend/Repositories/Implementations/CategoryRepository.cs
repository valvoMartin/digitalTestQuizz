using digital.Backend.Data;
using digital.Backend.Helpers;
using digital.Backend.Repositories.Interfaces;
using digital.Shared.DTOs;
using digital.Shared.Entities.Companies;
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
                .Include(s => s.Sector)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<Category>>
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
            var queryable = _context.Categories
                .Include(x => x.Country)
                .Include(s => s.Sector)
                .AsQueryable();

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
                .Include(s => s.Sector)
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
                    //Country = category.Country,
                    CountryId = category.CountryId,
                    //EmployesLimit = category.EmployesLimit,
                    RevenueLimit = category.RevenueLimit,
                    SectorId = category.SectorId,
                    //TODO: Agregar 
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
                    .Include(s => s.Sector)
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
                //objet.Country = category.Country;
                objet.CountryId = category.CountryId;
                objet.RevenueLimit = category.RevenueLimit;
                objet.SectorId = category.SectorId;
                

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

        //Task<ActionResponse<IEnumerable<Category>>> GetAsync(PaginationDTO pagination)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<ActionResponse<Category>> ICategoryRepository.GetAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ActionResponse<Category>> AddFullAsync(Category category)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ActionResponse<Category>> UpdateFullAsync(Category category)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<ActionResponse<Category>> ICategoryRepository.DeleteAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}











        //public override async Task<ActionResponse<Company>> DeleteAsync(int id)
        //{
        //    var row = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id && x.DateDelete == null);
        //    if (row == null)
        //    {
        //        return new ActionResponse<Company>
        //        {
        //            WasSuccess = false,
        //            Message = "Registro no encontrado"
        //        };
        //    }

        //    try
        //    {
        //        row.DateDelete = DateTime.UtcNow; // Ajusta la fecha de eliminación
        //        _context.Update(row);

        //        await _context.SaveChangesAsync();
        //        return new ActionResponse<Company>
        //        {
        //            WasSuccess = true,
        //        };
        //    }
        //    catch
        //    {
        //        return new ActionResponse<Company>
        //        {
        //            WasSuccess = false,
        //            Message = "No se puede borrar, porque tiene registros relacionados"
        //        };
        //    }
        //}






    }
}
