// LocationController.cs
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManager.DTOs;
using UserManager.Models;
using UserManager.Services;

namespace UserManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;

        public LocationController(ILocationService locationService, IMapper mapper)
        {
            _locationService = locationService;
            _mapper = mapper;
        }

        // CREATE
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> CreateLocation([FromBody] LocationDto locationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.ErrorResponse("Invalid data"));

            var result = await _locationService.CreateLocationAsync(locationDto);
            if (!result.Success)
                return BadRequest(ApiResponse<string>.ErrorResponse(result.Message));

            return Ok(ApiResponse<string>.SuccessResponse("", result.Message));
        }

        // READ ALL
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<List<LocationDto>>))]
        public async Task<IActionResult> GetAllLocations()
        {
            var result = await _locationService.GetAllLocationsAsync();
            return Ok(ApiResponse<List<LocationDto>>.SuccessResponse(result.Data, "Locations retrieved successfully"));
        }

        // READ BY ID
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<LocationDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> GetLocationById(int id)
        {
            var result = await _locationService.GetLocationByIdAsync(id);
            if (!result.Success)
                return NotFound(ApiResponse<string>.ErrorResponse(result.Message));

            return Ok(ApiResponse<LocationDto>.SuccessResponse(result.Data, "Location retrieved successfully"));
        }

        // UPDATE
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> UpdateLocation(int id, [FromBody] LocationDto locationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.ErrorResponse("Invalid data"));

            var result = await _locationService.UpdateLocationAsync(id, locationDto);
            if (!result.Success)
                return BadRequest(ApiResponse<string>.ErrorResponse(result.Message));

            return Ok(ApiResponse<string>.SuccessResponse("", result.Message));
        }

        // DELETE
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var result = await _locationService.DeleteLocationAsync(id);
            if (!result.Success)
                return NotFound(ApiResponse<string>.ErrorResponse(result.Message));

            return Ok(ApiResponse<string>.SuccessResponse("", result.Message));
        }
    }
}
