using digital.Backend.UnitsOfWork.Implementations;
using digital.Backend.UnitsOfWork.Interfaces;
using digital.Shared.Entities.Companies;
using digital.Shared.Entities.Test;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace digital.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AnswersUsersController : GenericController<AnswerUser>
    {
        private readonly IAnswersUsersUnitOfWork _answersUsersUnitOfWork;

        public AnswersUsersController(IGenericUnitOfWork<AnswerUser> unitOfWork, IAnswersUsersUnitOfWork answersUsersUnitOfWork) : base(unitOfWork)
        {
            _answersUsersUnitOfWork = answersUsersUnitOfWork;
        }




        [HttpGet("LastAnswered/{userId:int}")]
        public async Task<IActionResult> GetLastAnsweredQuestionByUserId(int userId)
        {
            var response = await _answersUsersUnitOfWork.GetLastQuestionByUserId(userId);
            if (response.WasSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest(response.Message);
        }
    }
}
