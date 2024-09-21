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
    public class ResourcesController : ControllerBase
    {
        private readonly IResourceService _resourceService;

        public ResourcesController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        [HttpPost]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> CreateResource([FromBody] ResourceDto model)
        {
            Guid ownerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _resourceService.CreateResourceAsync(model, ownerId);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(new { message = result.Message });
        }

        [HttpGet]
        public IActionResult GetResources()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var role = User.FindFirstValue(ClaimTypes.Role);
            var resources = _resourceService.GetResources(userId, role);
            return Ok(resources);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResource(Guid id, [FromBody] ResourceDto model)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _resourceService.UpdateResourceAsync(id, model, userId);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(new { message = result.Message });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResource(Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Enum.TryParse(User.FindFirstValue(ClaimTypes.Role), out UserRole role);

            var result = await _resourceService.DeleteResourceAsync(id, userId);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(new { message = result.Message });
        }
    }
}
