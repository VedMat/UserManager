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
    public class ResourcesController : ControllerBase
    {
        private readonly IResourceService _resourceService;
        private readonly IMapper _mapper;

        public ResourcesController(IResourceService resourceService, IMapper mapper)
        {
            _resourceService = resourceService;
            _mapper = mapper;
        }

        /// <summary>
        /// Crea una nuova risorsa.
        /// </summary>
        /// <param name="model">Dettagli della risorsa da creare.</param>
        /// <returns>Conferma della creazione della risorsa.</returns>
        [HttpPost]
        [Authorize(Roles = "Client")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> CreateResource([FromBody] ResourceDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse("Dati del modello non validi"));
            }

            Guid ownerId;
            try
            {
                ownerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            }
            catch
            {
                return Unauthorized(ApiResponse<string>.ErrorResponse("Token utente non valido"));
            }

            var result = await _resourceService.CreateResourceAsync(model, ownerId);
            if (!result.Success)
                return BadRequest(ApiResponse<string>.ErrorResponse(result.Message));

            return Ok(ApiResponse<string>.SuccessResponse("", result.Message));
        }

        /// <summary>
        /// Recupera tutte le risorse disponibili per l'utente corrente.
        /// </summary>
        /// <returns>Lista delle risorse.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<List<Resource>>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse<string>))]
        public IActionResult GetResources()
        {
            Guid userId;
            try
            {
                userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            }
            catch
            {
                return Unauthorized(ApiResponse<List<Resource>>.ErrorResponse("Token utente non valido"));
            }

            var role = User.FindFirstValue(ClaimTypes.Role);
            var resources = _resourceService.GetResources(userId, role);
            if (!resources.Success)
                return BadRequest(ApiResponse<List<Resource>>.ErrorResponse(resources.Message));

            return Ok(ApiResponse<List<Resource>>.SuccessResponse(resources.Data, "Risorse recuperate con successo"));
        }

        /// <summary>
        /// Aggiorna una risorsa esistente.
        /// </summary>
        /// <param name="id">ID della risorsa da aggiornare.</param>
        /// <param name="model">Nuovi dettagli della risorsa.</param>
        /// <returns>Conferma dell'aggiornamento.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> UpdateResource(Guid id, [FromBody] ResourceDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse("Dati del modello non validi"));
            }

            Guid userId;
            try
            {
                userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            }
            catch
            {
                return Unauthorized(ApiResponse<string>.ErrorResponse("Token utente non valido"));
            }

            var result = await _resourceService.UpdateResourceAsync(id, model, userId);
            if (!result.Success)
                return BadRequest(ApiResponse<string>.ErrorResponse(result.Message));

            return Ok(ApiResponse<string>.SuccessResponse("", result.Message));
        }

        /// <summary>
        /// Elimina una risorsa esistente.
        /// </summary>
        /// <param name="id">ID della risorsa da eliminare.</param>
        /// <returns>Conferma dell'eliminazione.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> DeleteResource(Guid id)
        {
            Guid userId;
            try
            {
                userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            }
            catch
            {
                return Unauthorized(ApiResponse<string>.ErrorResponse("User token is not valid"));
            }

            var result = await _resourceService.DeleteResourceAsync(id, userId);
            if (!result.Success)
                return BadRequest(ApiResponse<string>.ErrorResponse(result.Message));

            return Ok(ApiResponse<string>.SuccessResponse("", result.Message));
        }
    }
}
