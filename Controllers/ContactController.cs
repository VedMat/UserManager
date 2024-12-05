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
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public ContactController(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        // CREATE
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ContactDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> CreateContact([FromBody] ContactDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.ErrorResponse("Invalid data"));

            var result = await _contactService.CreateContactAsync(model);
            if (!result.Success)
                return BadRequest(ApiResponse<string>.ErrorResponse(result.Message));

            return Ok(ApiResponse<ContactDto>.SuccessResponse(_mapper.Map<ContactDto>(result.Data), result.Message));
        }

        // READ ALL
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<List<Contact>>))]
        public async Task<IActionResult> GetAllContacts()
        {
            var result = await _contactService.GetAllContactsAsync();
            return Ok(ApiResponse<List<ContactDto>>.SuccessResponse(_mapper.Map<List<ContactDto>>(result.Data), "Contacts retrieved successfully"));
        }

        // READ BY STARTUP
        [HttpGet("startup/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<List<Contact>>))]
        public async Task<IActionResult> GetContactByStartupId(Guid id)
        {
            var result = await _contactService.GetContactByStartupIdAsync(id);
            return Ok(ApiResponse<List<ContactDto>>.SuccessResponse(_mapper.Map<List<ContactDto>>(result.Data), "Contacts retrieved successfully"));
        }
        
        // READ BY COMPANY
        [HttpGet("company/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<List<Contact>>))]
        public async Task<IActionResult> GetContactByCompanyId(Guid id)
        {
            var result = await _contactService.GetContactByCompanyIdAsync(id);
            return Ok(ApiResponse<List<ContactDto>>.SuccessResponse(_mapper.Map<List<ContactDto>>(result.Data), "Contacts retrieved successfully"));
        }

        // READ BY ID
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ContactDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> GetContactById(Guid id)
        {
            var result = await _contactService.GetContactByIdAsync(id);
            if (!result.Success)
                return NotFound(ApiResponse<string>.ErrorResponse(result.Message));

            return Ok(ApiResponse<ContactDto>.SuccessResponse(_mapper.Map<ContactDto>(result.Data), "Contact retrieved successfully"));
        }

        // UPDATE
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<ContactDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> UpdateContact(Guid id, [FromBody] ContactDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<string>.ErrorResponse("Invalid data"));

            var result = await _contactService.UpdateContactAsync(id, model);
            if (!result.Success)
                return BadRequest(ApiResponse<string>.ErrorResponse(result.Message));

            return Ok(ApiResponse<ContactDto>.SuccessResponse(_mapper.Map<ContactDto>(result.Data), result.Message));
        }

        // DELETE
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            var result = await _contactService.DeleteContactAsync(id);
            if (!result.Success)
                return NotFound(ApiResponse<string>.ErrorResponse(result.Message));

            return Ok(ApiResponse<string>.SuccessResponse("", result.Message));
        }
    }
}
