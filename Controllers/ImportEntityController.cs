using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManager.DTOs;
using UserManager.Services;


namespace UserManager.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ImportEntityController : ControllerBase
    {
        private readonly IImportEntityService _importEntityService;


        public ImportEntityController(IImportEntityService importEntityService)
        {
            _importEntityService = importEntityService;
        }

        [HttpPost("upload")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> UploadCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse("Invalid data"));
            }

            // Logica per processare il file
            await _importEntityService.ImportDataAsync(file);

            return Ok(ApiResponse<string>.SuccessResponse("File uploaded successfully"));
        }
    }
}
