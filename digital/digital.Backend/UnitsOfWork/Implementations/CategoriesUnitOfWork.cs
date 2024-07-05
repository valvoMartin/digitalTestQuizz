using digital.Backend.Repositories.Interfaces;
using digital.Backend.UnitsOfWork.Interfaces;
using digital.Shared.DTOs;
using digital.Shared.Entities.Companies;
using digital.Shared.Responses;

namespace digital.Backend.UnitsOfWork.Implementations
{
    public class CategoriesUnitOfWork : GenericUnitOfWork<Category>, ICategoriesUnitOfWork
    {

        private readonly ICategoryRepository _categoryRepository;

        public CategoriesUnitOfWork(IGenericRepository<Category> repository, ICategoryRepository categoryRepository) : base(repository)
        {
            _categoryRepository = categoryRepository;
        }
            


        public override async  Task<ActionResponse<int>> GetTotalPagesAsync(PaginationDTO pagination) => await _categoryRepository.GetTotalPagesAsync(pagination);

        public override async Task<ActionResponse<IEnumerable<Category>>> GetAsync(PaginationDTO pagination) => await _categoryRepository.GetAsync(pagination);
     
        public override async Task<ActionResponse<Category>> GetAsync(int id) => await _categoryRepository.GetAsync(id);

        //public async Task<ActionResponse<IEnumerable<Category>>> GetAsync() => await _categoryRepository.GetAsync();
      
        public async Task<ActionResponse<Category>> AddFullAsync(Category category) => await _categoryRepository.AddFullAsync(category);

        public async Task<ActionResponse<Category>> UpdateFullAsync(Category category) => await _categoryRepository.UpdateFullAsync(category);
       
        public async Task<ActionResponse<Category>> DeleteAsync(int id) => await _categoryRepository.DeleteAsync(id);

        

      

       

       
    }
}
