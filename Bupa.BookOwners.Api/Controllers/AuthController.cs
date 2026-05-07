using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Bupa.BookOwners.Api.Enums;
using Bupa.BookOwners.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Bupa.BookOwners.Api.Controllers
{
    [ApiController]
    [Route("api/Auth")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;

        public AuthController(ILogger<AuthController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// Returns a guest token to be used for authentication
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet(nameof(GetGuestToken))]
        public IActionResult GetGuestToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtKey = _configuration["Jwt:Key"];

            if (jwtKey != null)
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity([new Claim("id", "guest_user")]),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                        SecurityAlgorithms.HmacSha256Signature
                    ),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok(new { token = tokenHandler.WriteToken(token) });
            }
            else
            {
                _logger.LogError("Jwt Key configration is missing.");
                return StatusCode(500);
            }
        }
    }
}
