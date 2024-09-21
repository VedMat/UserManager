using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserManager.DTOs;
using UserManager.Models;
using UserManager.Services;

namespace UserManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpPost("managers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateManager([FromBody] RegisterDto model)
        {
            var result = await _userService.CreateUserAsync(model, UserRole.Manager);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(new { message = result.Message });
        }

        [Authorize]
        [HttpPost("clients")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> CreateClient([FromBody] RegisterDto model)
        {
            var managerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _userService.CreateClientAsync(model, managerId);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(new { message = result.Message });
        }

        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = _userService.GetById(userId);
            return Ok(user);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] RegisterDto model)
        {

            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _userService.UpdateUserAsync(userId, model);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(new { message = result.Message });
        }

        [HttpDelete("profile")]
        public async Task<IActionResult> DeleteProfile()
        {
            // Retrieve the user's ID and role from JWT claims
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("User ID not found in token.");

            if (!Guid.TryParse(userIdClaim, out Guid userId))
                return Unauthorized("Invalid User ID.");

            // Check if the user is an admin
            if (userRole == "Admin")
                return BadRequest("Admins cannot delete their own accounts.");

            // Proceed to delete the user
            var result = await _userService.DeleteUserAsync(userId);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(new { message = result.Message });
        }
    }
}
