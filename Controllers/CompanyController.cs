using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManager.Models;
using UserManager.Services;
using UserManager.DTOs;
using System.Collections.Generic;

namespace UserManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CompanysController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanysController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }

        // CREATE
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<CompanyDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.ErrorResponse("Invalid data"));

            var result = await _companyService.CreateCompanyAsync(model);
            if (!result.Success)
                return BadRequest(ApiResponse<string>.ErrorResponse(result.Message));

            return Ok(ApiResponse<CompanyDto>.SuccessResponse(_mapper.Map<CompanyDto>(result.Data), result.Message));
        }

        // READ ALL
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<List<Company>>))]
        public async Task<IActionResult> GetAllCompanys()
        {
            var result = await _companyService.GetAllCompanysAsync();
            return Ok(ApiResponse<List<CompanyDto>>.SuccessResponse(_mapper.Map<List<CompanyDto>>(result.Data), "Companys retrieved successfully"));
        }

        // READ BY ID
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<CompanyDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> GetCompanyById(Guid id)
        {
            var result = await _companyService.GetCompanyByIdAsync(id);
            if (!result.Success)
                return NotFound(ApiResponse<string>.ErrorResponse(result.Message));

            return Ok(ApiResponse<CompanyDto>.SuccessResponse(_mapper.Map<CompanyDto>(result.Data), "Company retrieved successfully"));
        }

        // UPDATE
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<CompanyDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] CompanyDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.ErrorResponse("Invalid data"));

            var result = await _companyService.UpdateCompanyAsync(id, model);
            if (!result.Success)
                return BadRequest(ApiResponse<string>.ErrorResponse(result.Message));

            return Ok(ApiResponse<CompanyDto>.SuccessResponse(_mapper.Map<CompanyDto>(result.Data), result.Message));
        }

        // DELETE
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            var result = await _companyService.DeleteCompanyAsync(id);
            if (!result.Success)
                return NotFound(ApiResponse<string>.ErrorResponse(result.Message));

            return Ok(ApiResponse<string>.SuccessResponse("", result.Message));
        }
    }
}
