using digital.Backend.Data;
using digital.Backend.Helpers;
using digital.Backend.Repositories.Interfaces;
using digital.Shared.DTOs;
using digital.Shared.Entities;
using digital.Shared.Responses;
using Microsoft.EntityFrameworkCore;

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

        public override async Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination)
        {
            var queryable = _context.Companies.AsQueryable();

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
            var product = await _context.Companies
                .Include(x => x.Sector)
                .Include(x => x.City)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
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
                Result = product
            };
        }

        public async Task<ActionResponse<Company>> AddFullAsync(Company company)
        {
            try
            {
                var newCompany = new Company
                {
                    Cuit = newCompany.Cuit,
                    Name = newCompany.Name,
                    City = newCompany.City
                     = newCompany.Description,
                    Price = newCompany.Price,
                    Stock = newCompany.Stock,
                    //ProductCategories = new List<ProductCategory>(),
                };

               

                foreach (var productCategoryId in productDTO.ProductCategoryIds!)
                {
                    var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == productCategoryId);
                    if (category != null)
                    {
                        newProduct.ProductCategories.Add(new ProductCategory { Category = category });
                    }
                }

                _context.Add(newProduct);
                await _context.SaveChangesAsync();
                return new ActionResponse<Product>
                {
                    WasSuccess = true,
                    Result = newProduct
                };
            }
            catch (DbUpdateException)
            {
                return new ActionResponse<Product>
                {
                    WasSuccess = false,
                    Message = "Ya existe un producto con el mismo nombre."
                };
            }
            catch (Exception exception)
            {
                return new ActionResponse<Product>
                {
                    WasSuccess = false,
                    Message = exception.Message
                };
            }
        }

        public async Task<ActionResponse<Company>> UpdateFullAsync(Company productDTO)
        {
            try
            {
                var product = await _context.Products
                    .Include(x => x.ProductCategories!)
                    .ThenInclude(x => x.Category)
                    .FirstOrDefaultAsync(x => x.Id == productDTO.Id);
                if (product == null)
                {
                    return new ActionResponse<Product>
                    {
                        WasSuccess = false,
                        Message = "Producto no existe"
                    };
                }

                product.Name = productDTO.Name;
                product.Description = productDTO.Description;
                product.Price = productDTO.Price;
                product.Stock = productDTO.Stock;

                _context.ProductCategories.RemoveRange(product.ProductCategories!);
                product.ProductCategories = new List<ProductCategory>();

                foreach (var productCategoryId in productDTO.ProductCategoryIds!)
                {
                    var category = await _context.Categories.FindAsync(productCategoryId);
                    if (category != null)
                    {
                        _context.ProductCategories.Add(new ProductCategory { CategoryId = category.Id, ProductId = product.Id });
                    }
                }

                _context.Update(product);
                await _context.SaveChangesAsync();
                return new ActionResponse<Product>
                {
                    WasSuccess = true,
                    Result = product
                };
            }
            catch (DbUpdateException)
            {
                return new ActionResponse<Product>
                {
                    WasSuccess = false,
                    Message = "Ya existe un producto con el mismo nombre."
                };
            }
            catch (Exception exception)
            {
                return new ActionResponse<Product>
                {
                    WasSuccess = false,
                    Message = exception.Message
                };
            }
        }

        
    }
}
