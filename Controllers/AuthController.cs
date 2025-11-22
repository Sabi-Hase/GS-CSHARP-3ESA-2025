using Microsoft.AspNetCore.Mvc;
using Mooditor.Api.DTOs;
using Mooditor.Api.Services;

namespace Mooditor.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth) => _auth = auth;

        [HttpPost("register")]
        [ProducesResponseType(typeof(AuthResponseDto), 201)]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var result = await _auth.RegisterAsync(dto);
            // CreatedAtAction referencing a "GetUser" can be added later; for now return Created with payload
            return Created(string.Empty, result);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponseDto), 200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _auth.LoginAsync(dto);
            if (result == null)
                return Unauthorized(new { message = "Credenciais inválidas" });

            return Ok(result);
        }
    }
}
