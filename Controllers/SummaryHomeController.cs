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
    [Authorize]
    [Produces("application/json")]
    public class SummaryHomeController : ControllerBase
    {
        private readonly ISummaryHomeService _summaryHomeService;
        private readonly IMapper _mapper;

        public SummaryHomeController(ISummaryHomeService summaryHomeService, IMapper mapper)
        {
            _summaryHomeService = summaryHomeService;
            _mapper = mapper;
        }

        // READ ALL
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<List<SummaryHomeDto>>))]
        public async Task<IActionResult> GetAllSummaryHome()
        {
            var result = await _summaryHomeService.GetSummaryHome();
            return Ok(ApiResponse<Dictionary<string, double>>.SuccessResponse(result.Data, "SummaryHome retrieved successfully"));
        }
    }
}
