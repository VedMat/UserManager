using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using UserManager.DTOs;
using UserManager.Models;
using UserManager.Services;

namespace UserManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Crea un nuovo manager.
        /// </summary>
        /// <param name="model">Dettagli del manager da creare.</param>
        /// <returns>Conferma della creazione del manager.</returns>
        [HttpPost("managers")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<User>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> CreateManager([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)    
            {
                return BadRequest(ApiResponse<string>.ErrorResponse("Model data invalid"));
            }

            var result = await _userService.CreateUserAsync(model, UserRole.Manager);
            if (!result.Success)
                return BadRequest(ApiResponse<string>.ErrorResponse(result.Message));

            return Ok(ApiResponse<User>.SuccessResponse(result.Data, result.Message));
        }

        /// <summary>
        /// Crea un nuovo client.
        /// </summary>
        /// <param name="model">Dettagli del client da creare.</param>
        /// <returns>Conferma della creazione del client.</returns>
        [HttpPost("clients")]
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<User>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> CreateClient([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse("Model data invalid"));
            }

            Guid managerId;
            try
            {
                managerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            }
            catch
            {
                return Unauthorized(ApiResponse<string>.ErrorResponse("User token invalid"));
            }

            var result = await _userService.CreateClientAsync(model, managerId);
            if (!result.Success)
                return BadRequest(ApiResponse<string>.ErrorResponse(result.Message));

            return Ok(ApiResponse<User>.SuccessResponse(result.Data, result.Message));
        }

        /// <summary>
        /// Recupera il profilo dell'utente corrente.
        /// </summary>
        /// <returns>Dettagli del profilo utente.</returns>
        [HttpGet("profile")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<User>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse<string>))]
        public IActionResult GetProfile()
        {
            Guid userId;
            try
            {
                userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            }
            catch
            {
                return Unauthorized(ApiResponse<User>.ErrorResponse("User token invalid"));
            }

            var user = _userService.GetById(userId);
            if (user == null)
                return NotFound(ApiResponse<User>.ErrorResponse("Utente non trovato"));

            return Ok(ApiResponse<User>.SuccessResponse(user));
        }

        /// <summary>
        /// Aggiorna il profilo dell'utente corrente.
        /// </summary>
        /// <param name="model">Nuovi dettagli del profilo.</param>
        /// <returns>Conferma dell'aggiornamento del profilo.</returns>
        [HttpPut("profile")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<User>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> UpdateProfile([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse("Model data invalid"));
            }

            Guid userId;
            try
            {
                userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            }
            catch
            {
                return Unauthorized(ApiResponse<User>.ErrorResponse("User token invalid"));
            }

            var result = await _userService.UpdateUserAsync(userId, model);
            if (!result.Success)
                return BadRequest(ApiResponse<string>.ErrorResponse(result.Message));

            return Ok(ApiResponse<User>.SuccessResponse(result.Data, result.Message));
        }

        /// <summary>
        /// Elimina il profilo dell'utente corrente.
        /// </summary>
        /// <returns>Conferma dell'eliminazione del profilo.</returns>
        [HttpDelete("profile")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> DeleteProfile()
        {
            // Retrieve the user's ID and role from JWT claims
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized(ApiResponse<string>.ErrorResponse("User ID not found in token"));

            if (!Guid.TryParse(userIdClaim, out Guid userId))
                return Unauthorized(ApiResponse<string>.ErrorResponse("Invalid User ID"));

            // Proceed to delete the user
            var result = await _userService.DeleteUserAsync(userId);
            if (!result.Success)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse(result.Message));
            }

            return Ok(ApiResponse<string>.SuccessResponse("", result.Message));
        }
    }
}
