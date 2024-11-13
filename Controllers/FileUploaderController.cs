using AutoMapper;
using Azure;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManager.Data;
using UserManager.DTOs;
using UserManager.Models;
using UserManager.Services;

namespace UserManager.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class FileUploadController : ControllerBase
    {
        private readonly BlobStorageService _blobStorageService;
        private readonly IStartupService _startupService;
        private readonly IMapper _mapper;

        public FileUploadController(IStartupService startupService, IMapper mapper, BlobStorageService blobStorageService)
        {
            _mapper = mapper;
            _startupService = startupService;
            _blobStorageService = blobStorageService;
        }

        [HttpGet("downloadUrl/{Id}")]
        public async Task<IActionResult> GetDownloadUrl(string Id)
        {
            var startup = await _startupService.GetStartupByIdAsync(Guid.Parse(Id));
            if (startup.Data == null || startup.Data.PitchDeck == null)
            {
                return NotFound("File not found.");
            }

            var sasUri = await _blobStorageService.DownloadFileAsync(startup.Data.PitchDeck);
            return Ok(ApiResponse<string>.SuccessResponse(sasUri, "Startup retrieved successfully"));
        }


        [HttpPost("upload/{Id}")]
        public async Task<IActionResult> UploadFile(IFormFile file, string Id)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Nessun file selezionato per l'upload.");
            }

            string? fileUrl;
            using (var stream = file.OpenReadStream())
            {
                fileUrl = await _blobStorageService.UploadFileAsync(stream, file.FileName);
            }

            try
            {

                var startup = await _startupService.GetStartupByIdAsync(Guid.Parse(Id));
                var startupDto = _mapper.Map<StartupDto>(startup.Data);
                startupDto.PitchDeck = file.FileName;
                await _startupService.UpdateStartupAsync(Guid.Parse(Id), startupDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Ok(ApiResponse<string>.SuccessResponse("", "File upload successfully"));
        }
    }
}
