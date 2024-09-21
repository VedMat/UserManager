using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserManager.DTOs;
using UserManager.Helpers;
using UserManager.Services;

namespace UserManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService, IConfiguration configuration, IMapper mapper)
        {
            _userService = userService;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto model)
        {
            var user = _userService.Authenticate(model.Email, model.Password);
            if (user == null)
                return Unauthorized(new { message = "Invalid credentials" });

            var token = JwtHelper.GenerateJwtToken(user, _configuration);

            return Ok(new { token });
        }
    }
}
