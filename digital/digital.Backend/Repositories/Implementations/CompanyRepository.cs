using digital.Backend.Data;
using digital.Backend.Helpers;
using digital.Backend.Repositories.Interfaces;
using digital.Shared.DTOs;
using digital.Shared.Entities.Companies;
using digital.Shared.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace digital.Backend.Repositories.Implementations
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        private readonly DataContext _context;

        public CompanyRepository(DataContext context) : base(context)
        {
            _context = context;
        }


        public override async Task<ActionResponse<IEnumerable<Company>>> GetAsync(PaginationDTO pagination)
        {
            var queryable = _context.Companies
                .Where(x => x.DateDelete == null)
                .Include(Category => Category.Category!)
                .Include(u => u.City!)
                .ThenInclude(c => c.State!)
                .ThenInclude(s => s.Country)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(pagination.Filter))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
            }

            return new ActionResponse<IEnumerable<Company>>
            {
                WasSuccess = true,
                Result = await queryable
                    .OrderBy(x => x.Name)
                    .Paginate(pagination)
                    .ToListAsync()
            };
        }

        public override async Task<ActionResponse<Company>> DeleteAsync(int id)
        {
            var row = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id && x.DateDelete == null);  
            if (row == null)
            {
                return new ActionResponse<Company>
                {
                    WasSuccess = false,
                    Message = "Registro no encontrado"
                };
            }

            try
            {
                row.DateDelete = DateTime.UtcNow; // Ajusta la fecha de eliminación
                _context.Update(row);

                await _context.SaveChangesAsync();
                return new ActionResponse<Company>
                {
                    WasSuccess = true,
                };
            }
            catch
            {
                return new ActionResponse<Company>
                {
                    WasSuccess = false,
                    Message = "No se puede borrar, porque tiene registros relacionados"
                };
            }
        }


        
        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Companies
                .Where(x => x.DateDelete == null)
                .Include(Category => Category.Category!)
                .Include(u => u.City!)
                .ThenInclude(c => c.State!)
                .ThenInclude(s => s.Country)
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

        public override async Task<ActionResponse<Company>> GetAsync(int id)
        {
            var company = await _context.Companies
                .Include(x => x.Category)
                .Include(x => x.City)
                .ThenInclude(x => x!.State)
                .ThenInclude(x => x!.Country)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (company == null)
            {
                return new ActionResponse<Company>
                {
                    WasSuccess = false,
                    Message = "Compañia no existe"
                };
            }

            return new ActionResponse<Company>
            {
                WasSuccess = true,
                Result = company
            };
        }

        public async Task<ActionResponse<Company>> AddFullAsync(Company company)
        {
            try
            {
                var newCompany = new Company
                {
                    Cuit = company.Cuit,
                    Name = company.Name,
                    City = company.City,
                    CityId = company.CityId,
                    Email = company.Email,
                    WebPage = company.WebPage,
                    LegalForm = company.LegalForm,
                    Sector = company.Sector,
                    Size = company.Size,
                    OwnFacilities = company.OwnFacilities,
                    PorcAdministracion = company.PorcAdministracion,
                    PorcComercializacion = company.PorcComercializacion,
                    PorcProduccion = company.PorcProduccion,
                    PorcRRHH = company.PorcRRHH,
                    PorcLogistica = company.PorcLogistica,
                    PorcMantenimiento = company.PorcMantenimiento,
                    PorcProductoDestinadoAMercadoLocal = company.PorcProductoDestinadoAMercadoLocal,
                    PorcExportacion = company.PorcExportacion,
                    Terciariza = company.Terciariza,
                    Observaciones = company.Observaciones,
                    DateInsert = DateTime.UtcNow,
                    //DateUpdate = 
                    //DateDelete = 
                };


                _context.Add(newCompany);
                await _context.SaveChangesAsync();
                return new ActionResponse<Company>
                {
                    WasSuccess = true,
                    Result = newCompany
                };
            }
            catch (DbUpdateException)
            {
                return new ActionResponse<Company>
                {
                    //TODO: Si la empresa ya existe => le doy habilitación para hacer test con esa empresa pero no lo dejo modificar los campos q ya creo otra persona sobre esa empresa.
                    WasSuccess = false,
                    Message = "Ya existe una Empresa con el mismo nombre."
                };
            }
            catch (Exception exception)
            {
                return new ActionResponse<Company>
                {
                    WasSuccess = false,
                    Message = exception.Message
                };
            }
        }

        public async Task<ActionResponse<Company>> UpdateFullAsync(Company company)
        {
            try
            {
                var MyCompany = await _context.Companies
                    .Include(c => c.Category)
                    .Include(c => c.City)
                    .ThenInclude(s => s!.State)
                    .ThenInclude(y => y!.Country)
                    .FirstOrDefaultAsync(i => i.Id == company.Id);

                if (MyCompany == null)
                {
                    return new ActionResponse<Company>
                    {
                        WasSuccess = false,
                        Message = "Companñia no existe."
                    };
                }

                MyCompany.Cuit = company.Cuit;
                MyCompany.Name = company.Name;
                MyCompany.City = company.City;
                MyCompany.CityId = company.CityId;
                MyCompany.Email = company.Email;
                MyCompany.WebPage = company.WebPage;
                MyCompany.LegalForm = company.LegalForm;
                MyCompany.Sector = company.Sector;
                MyCompany.Category = company.Category;
                MyCompany.Size = company.Size;
                MyCompany.OwnFacilities = company.OwnFacilities;
                MyCompany.PorcAdministracion = company.PorcAdministracion;
                MyCompany.PorcComercializacion = company.PorcComercializacion;
                MyCompany.PorcProduccion = company.PorcProduccion;
                MyCompany.PorcRRHH = company.PorcRRHH;
                MyCompany.PorcLogistica = company.PorcLogistica;
                MyCompany.PorcMantenimiento = company.PorcMantenimiento;
                MyCompany.PorcProductoDestinadoAMercadoLocal = company.PorcProductoDestinadoAMercadoLocal;
                MyCompany.PorcExportacion = company.PorcExportacion;
                MyCompany.Terciariza = company.Terciariza;
                MyCompany.Observaciones = company.Observaciones;
                //DateInsert = company.DateInsert,
                MyCompany.DateUpdate = DateTime.UtcNow;
                    //DateDelete = 

                //_context.ProductCategories.RemoveRange(product.ProductCategories!);
                //product.ProductCategories = new List<ProductCategory>();

                //foreach (var productCategoryId in productDTO.ProductCategoryIds!)
                //{
                //    var category = await _context.Categories.FindAsync(productCategoryId);
                //    if (category != null)
                //    {
                //        _context.ProductCategories.Add(new ProductCategory { CategoryId = category.Id, ProductId = product.Id });
                //    }
                //}

                _context.Update(MyCompany);
                await _context.SaveChangesAsync();
                return new ActionResponse<Company>
                {
                    WasSuccess = true,
                    Result = MyCompany
                };
            }
            catch (DbUpdateException)
            {
                return new ActionResponse<Company>
                {
                    WasSuccess = false,
                    Message = "Ya existe una Empresa con el mismo nombre."
                };
            }
            catch (Exception exception)
            {
                return new ActionResponse<Company>
                {
                    WasSuccess = false,
                    Message = exception.Message
                };
            }
        }

        
    }
}
