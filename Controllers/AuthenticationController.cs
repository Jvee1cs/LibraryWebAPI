using Librarykuno.Interfaces;
using Librarykuno.Request;
using Librarykuno.Response;
using Microsoft.AspNetCore.Mvc;

namespace Librarykuno.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthenticationController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            AuthenticationResponse response = await _authService.RegisterAsync(request);
            if (response.IsSuccess)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            AuthenticationResponse response = await _authService.LoginAsync(request);
            return Ok(response);
        }
    }
}