using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManager.Models;
using UserManager.Services;
using UserManager.DTOs;

namespace UserManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class StartupsController : ControllerBase
    {
        private readonly IStartupService _startupService;
        private readonly IMapper _mapper;

        public StartupsController(IStartupService startupService, IMapper mapper)
        {
            _startupService = startupService;
            _mapper = mapper;
        }

        // CREATE
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> CreateStartup([FromBody] StartupDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.ErrorResponse("Invalid data"));

            var result = await _startupService.CreateStartupAsync(model);
            if (!result.Success)
                return BadRequest(ApiResponse<string>.ErrorResponse(result.Message));

            return Ok(ApiResponse<string>.SuccessResponse("", result.Message));
        }

        // READ ALL
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<List<Startup>>))]
        public async Task<IActionResult> GetAllStartups()
        {
            var result = await _startupService.GetAllStartupsAsync();
            return Ok(ApiResponse<List<Startup>>.SuccessResponse(result.Data, "Startups retrieved successfully"));
        }

        // READ BY ID
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<Startup>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> GetStartupById(long id)
        {
            var result = await _startupService.GetStartupByIdAsync(id);
            if (!result.Success)
                return NotFound(ApiResponse<string>.ErrorResponse(result.Message));

            return Ok(ApiResponse<Startup>.SuccessResponse(result.Data, "Startup retrieved successfully"));
        }

        // UPDATE
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> UpdateStartup(long id, [FromBody] StartupDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.ErrorResponse("Invalid data"));

            var result = await _startupService.UpdateStartupAsync(id, model);
            if (!result.Success)
                return BadRequest(ApiResponse<string>.ErrorResponse(result.Message));

            return Ok(ApiResponse<string>.SuccessResponse("", result.Message));
        }

        // DELETE
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> DeleteStartup(long id)
        {
            var result = await _startupService.DeleteStartupAsync(id);
            if (!result.Success)
                return NotFound(ApiResponse<string>.ErrorResponse(result.Message));

            return Ok(ApiResponse<string>.SuccessResponse("", result.Message));
        }
    }
}
