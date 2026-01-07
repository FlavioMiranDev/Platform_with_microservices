using Microsoft.AspNetCore.Mvc;
using MiraNexus.Auth.Data.DTOs;
using MiraNexus.Auth.Services;

namespace MiraNexus.Auth.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authservice;

        public AuthController(AuthService authService)
        {
            _authservice = authService;
        }


        [HttpGet]
        public async Task<IActionResult> Refresh([FromHeader] string Token)
        {
            var user = await _authservice.Refresh(Token);

            return user is null ? NotFound() : Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserLoginResponseDTO>> Login([FromBody] UserLoginRequestDTO login)
        {
            var response = await _authservice.Authenticate(login);

            if (response is null) return BadRequest();

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequestDTO newUser)
        {
            var response = await _authservice.Register(newUser);

            if (response is null) return BadRequest();

            return Created();
        }

        [HttpPost("active")]
        public async Task<IActionResult> Active([FromBody] UserActiveRequestDTO active)
        {
            var response = await _authservice.Active(active);

            return response is not null ? Ok(new
            {
                Message = "Usuario ativado com sucesso"
            }) : BadRequest();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] UserLogoutRequestDTO logou)
        {
            await _authservice.Logout(logou.Token, logou.Id);

            return NoContent();
        }
    }
}
