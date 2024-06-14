using digital.Backend.Helpers;
using digital.Backend.UnitsOfWork.Interfaces;
using digital.Shared.DTOs;
using digital.Shared.Entities;
using digital.Shared.Responses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace digital.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {

        private readonly IUsersUnitOfWork _usersUnitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMailHelper _mailHelper;

        public AccountsController(IUsersUnitOfWork usersUnitOfWork, IConfiguration configuration, IMailHelper mailHelper)
        {
            _usersUnitOfWork = usersUnitOfWork;
            _configuration = configuration;
            _mailHelper = mailHelper;
        }


        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO model)
        {
            User user = model;
            var result = await _usersUnitOfWork.AddUserAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _usersUnitOfWork.AddUserToRoleAsync(user, user.UserType.ToString());
                var response = await SendConfirmationEmailAsync(user);
                if (response.WasSuccess)
                {
                    return NoContent();
                }

                return BadRequest(response.Message);
            }

            return BadRequest(result.Errors.FirstOrDefault());

            
        }


        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDTO model)
        {
            var result = await _usersUnitOfWork.LoginAsync(model);
            if (result.Succeeded)
            {
                var user = await _usersUnitOfWork.GetUserAsync(model.Email);
                return Ok(BuildToken(user));
            }

            if (result.IsLockedOut)
            {
                return BadRequest("Ha superado el máximo número de intentos, su cuenta está bloqueada, intente de nuevo en 5 minutos.");
            }

            if (result.IsNotAllowed)
            {
                return BadRequest("El usuario no ha sido habilitado, debes de seguir las instrucciones del correo enviado para poder habilitar el usuario.");
            }

            return BadRequest("Email o contraseña incorrectos.");

        }


        private TokenDTO BuildToken(User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Email!),
                new(ClaimTypes.Role, user.UserType.ToString()),
                new("Document", user.Document),
                new("FirstName", user.FirstName),
                new("LastName", user.LastName),
                new("CityId", user.CityId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwtKey"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(30);
            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            return new TokenDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }



        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PutAsync(User user)
        {
            try
            {
                var currentUser = await _usersUnitOfWork.GetUserAsync(User.Identity!.Name!);
                if (currentUser == null)
                {
                    return NotFound();
                }

               
                currentUser.Document = user.Document;
                currentUser.FirstName = user.FirstName;
                currentUser.LastName = user.LastName;
                currentUser.PhoneNumber = user.PhoneNumber;
                currentUser.CityId = user.CityId;

                var result = await _usersUnitOfWork.UpdateUserAsync(currentUser);
                if (result.Succeeded)
                {
                    return NoContent();
                }

                return BadRequest(result.Errors.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _usersUnitOfWork.GetUserAsync(User.Identity!.Name!));
        }


        [HttpPost("changePassword")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _usersUnitOfWork.GetUserAsync(User.Identity!.Name!);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _usersUnitOfWork.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.FirstOrDefault()!.Description);
            }

            return NoContent();
        }



        private async Task<ActionResponse<string>> SendConfirmationEmailAsync(User user)
        {
            var myToken = await _usersUnitOfWork.GenerateEmailConfirmationTokenAsync(user);
            var tokenLink = Url.Action("ConfirmEmail", "accounts", new
            {
                userid = user.Id,
                token = myToken
            }, HttpContext.Request.Scheme, _configuration["Url Frontend"]); // TODO: Cuando hago clck me lleva a la url que puse en el appsettings del back en el campo => Url Frontend

            return _mailHelper.SendMail(user.FullName, user.Email!,
                $"DigitalBee - Confirmación de cuenta",
                $"<h1>DigitalBee Test - Confirmación de cuenta</h1>" +
                $"<p>Para habilitar el usuario, por favor hacer clic 'Confirmar Email':</p>" +
                $"<b><a href ={tokenLink}>Confirmar Email</a></b>");
        }


        // TODO: Aca me quede el viernes antes de viajar
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmailAsync(string userId, string token)
        {
            token = token.Replace(" ", "+");
            var user = await _usersUnitOfWork.GetUserAsync(new Guid(userId));
            if (user == null)
            {
                return NotFound();
            }

            var result = await _usersUnitOfWork.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.FirstOrDefault());
            }

            return NoContent();
        }


    }
}
