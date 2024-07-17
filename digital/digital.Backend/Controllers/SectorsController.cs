using digital.Backend.UnitsOfWork.Interfaces;
using digital.Shared.Entities.Companies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace digital.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SectorsController : GenericController<Sector>
    {
        public SectorsController(IGenericUnitOfWork<Sector> genericUnitOfWork): base(genericUnitOfWork)
        {
            
        }
    }
}
