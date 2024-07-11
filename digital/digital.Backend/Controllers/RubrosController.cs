using digital.Backend.Data;
using digital.Backend.UnitsOfWork.Interfaces;
using digital.Shared.Entities.Companies;
using digital.Shared.Entities.Countries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace digital.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RubrosController : GenericController<Rubro>
    {
        //private readonly IRubroUnitOfWork _rubroUnitOfWork;

        //public RubrosController(IRubroUnitOfWork<Rubro> unit, IRubroUnitOfWork rubroUnitOfWork) : base(unit)
        //{
        //    _rubroUnitOfWork = rubroUnitOfWork;
        //}


        //[HttpGet("full")]
        //public override async Task<IActionResult> GetAsync()
        //{
        //    var response = await _countriesUnitOfWork.GetAsync();
        //    if (response.WasSuccess)
        //    {
        //        return Ok(response.Result);
        //    }
        //    return BadRequest();
        //}
        private readonly IRubrosUnitOfWork _rubrosUnitOfWork;

        public RubrosController(IGenericUnitOfWork<Rubro> unitOfWork, IRubrosUnitOfWork rubroUnitOfWork) : base(unitOfWork)
        {
            _rubrosUnitOfWork = rubroUnitOfWork;
        }
        //private readonly DataContext _context;

        //public RubrosController(DataContext context)
        //{
        //    _context = context;
        //}

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Rubro>>> GetRubros()
        //{
        //    return await _rubrosUnitOfWork.Rubros.ToListAsync();
        //}

        [HttpGet("{rubroId}/sectors")]
        public async Task<ActionResult<IEnumerable<Sector>>> GetSectorsByRubro(int rubroId)
        {
            var sectors = await _rubrosUnitOfWork.GetSectorsByRubroAsync(rubroId);
            return Ok(sectors);
        }
    }
}
