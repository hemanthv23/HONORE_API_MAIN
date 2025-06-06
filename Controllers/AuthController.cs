using HONORE_API_MAIN.Models.DTOs;
using HONORE_API_MAIN.Services;
using Microsoft.AspNetCore.Mvc;

namespace HONORE_API_MAIN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthResponse
                {
                    Success = false,
                    Message = "Invalid input data"
                });
            }

            var response = await _authService.LoginAsync(request);

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpPost("register/individual")]
        public async Task<ActionResult<AuthResponse>> RegisterIndividual([FromBody] IndividualRegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthResponse
                {
                    Success = false,
                    Message = "Invalid input data"
                });
            }

            var response = await _authService.RegisterIndividualAsync(request);

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpPost("register/business")]
        public async Task<ActionResult<AuthResponse>> RegisterBusiness([FromBody] BusinessRegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new AuthResponse
                {
                    Success = false,
                    Message = "Invalid input data"
                });
            }

            var response = await _authService.RegisterBusinessAsync(request);

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        [HttpGet("all-users")]
        public async Task<ActionResult<IEnumerable<UserInfo>>> GetAllUsers()
        {
            var users = await _authService.GetAllUsersAsync();
            return Ok(users);
        }
    }
}
