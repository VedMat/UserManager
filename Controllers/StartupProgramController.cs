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
    //[Authorize]
    [Produces("application/json")]
    public class StartupProgramsController : ControllerBase
    {
        private readonly IStartupProgramService _startupProgramService;
        private readonly IMapper _mapper;

        public StartupProgramsController(IStartupProgramService startupProgramService, IMapper mapper)
        {
            _startupProgramService = startupProgramService;
            _mapper = mapper;
        }

        // CREATE
        [HttpPost]
        //[Authorize(Roles = "Admin,User")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> CreateStartupProgram([FromBody] StartupProgramDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.ErrorResponse("Invalid data"));

            var result = await _startupProgramService.CreateStartupProgramAsync(model);
            if (!result.Success)
                return BadRequest(ApiResponse<string>.ErrorResponse(result.Message));

            return Ok(ApiResponse<string>.SuccessResponse("", result.Message));
        }

        // READ ALL
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<List<StartupProgram>>))]
        public async Task<IActionResult> GetAllStartupPrograms()
        {
            var result = await _startupProgramService.GetAllStartupProgramsAsync();
            return Ok(ApiResponse<List<StartupProgram>>.SuccessResponse(result.Data, "Startup Programs retrieved successfully"));
        }

        // READ BY ID
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<StartupProgram>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> GetStartupProgramById(Guid id)
        {
            var result = await _startupProgramService.GetStartupProgramByIdAsync(id);
            if (!result.Success)
                return NotFound(ApiResponse<string>.ErrorResponse(result.Message));

            return Ok(ApiResponse<StartupProgram>.SuccessResponse(result.Data, "Startup Program retrieved successfully"));
        }

        // UPDATE
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> UpdateStartupProgram(Guid id, [FromBody] StartupProgramDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.ErrorResponse("Invalid data"));

            var result = await _startupProgramService.UpdateStartupProgramAsync(id, model);
            if (!result.Success)
                return BadRequest(ApiResponse<string>.ErrorResponse(result.Message));

            return Ok(ApiResponse<string>.SuccessResponse("", result.Message));
        }

        // DELETE
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> DeleteStartupProgram(Guid id)
        {
            var result = await _startupProgramService.DeleteStartupProgramAsync(id);
            if (!result.Success)
                return NotFound(ApiResponse<string>.ErrorResponse(result.Message));

            return Ok(ApiResponse<string>.SuccessResponse("", result.Message));
        }
    }
}
