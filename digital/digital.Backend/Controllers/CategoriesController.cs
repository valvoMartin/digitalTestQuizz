using digital.Backend.UnitsOfWork.Interfaces;
using digital.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace digital.Backend.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriesController : GenericController<Category>
    {

        private readonly ICategoriesUnitOfWork _categoriesUnitOfWork;

        public CategoriesController(IGenericUnitOfWork<Category> unitOfWork, ICategoriesUnitOfWork categoryUnitOfWork) : base(unitOfWork)
        {
            _categoriesUnitOfWork = categoryUnitOfWork;
        }



    }
}
