using Azure;
using digital.Backend.UnitsOfWork.Implementations;
using digital.Backend.UnitsOfWork.Interfaces;
using digital.Shared.DTOs;
using digital.Shared.Entities.Companies;
using digital.Shared.Entities.Test;
using digital.Shared.Responses;
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



        [HttpPost("save")]
        public async Task<IActionResult> SaveUserAnswer([FromBody] SaveUserAnswerRequestDTO request)
        {
            var response = await _answersUsersUnitOfWork.SaveUserAnswerAsync(request.Email, request.QuestionId, request.AnswerId, request.IsLast);

            if (!response.WasSuccess)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
            
        }


        [HttpGet("lastQuestion/{email}")]
        public async Task<ActionResult<LastQuestionDTO>> GetLastQuestionAndAllQuestions(string email)
        {
            var response = await _answersUsersUnitOfWork.GetLastQuestionAndAllQuestionsAsync(email);
            if (!response.WasSuccess)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Result);
        }
    }
}
