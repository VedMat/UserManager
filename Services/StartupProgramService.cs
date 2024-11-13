using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserManager.Data;
using UserManager.DTOs;
using UserManager.Models;

namespace UserManager.Services
{
    public class StartupProgramService : IStartupProgramService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public StartupProgramService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<string>> CreateStartupProgramAsync(StartupProgramDto StartupProgramDto)
        {
            var StartupProgram = _mapper.Map<StartupProgram>(StartupProgramDto);
            await _context.StartupPrograms.AddAsync(StartupProgram);
            await _context.SaveChangesAsync();

            return ServiceResponse<string>.SuccessResponse("StartupProgram created successfully");
        }

        public async Task<ServiceResponse<List<StartupProgram>>> GetAllStartupProgramsAsync()
        {
            var StartupPrograms = await _context.StartupPrograms.ToListAsync();
            return ServiceResponse<List<StartupProgram>>.SuccessResponse(StartupPrograms, "StartupPrograms retrieved successfully");
        }

        public async Task<ServiceResponse<StartupProgram>> GetStartupProgramByIdAsync(Guid id)
        {
            var StartupProgram = await _context.StartupPrograms.FindAsync(id);
            if (StartupProgram == null)
                return ServiceResponse<StartupProgram>.ErrorResponse("StartupProgram not found");

            return ServiceResponse<StartupProgram>.SuccessResponse(StartupProgram, "StartupProgram retrieved successfully");
        }

        public async Task<ServiceResponse<string>> UpdateStartupProgramAsync(Guid id, StartupProgramDto StartupProgramDto)
        {
            var StartupProgram = await _context.StartupPrograms.FindAsync(id);
            if (StartupProgram == null)
                return ServiceResponse<string>.ErrorResponse("StartupProgram not found");

            _mapper.Map(StartupProgramDto, StartupProgram);
            _context.StartupPrograms.Update(StartupProgram);
            await _context.SaveChangesAsync();

            return ServiceResponse<string>.SuccessResponse("StartupProgram updated successfully");
        }

        public async Task<ServiceResponse<string>> DeleteStartupProgramAsync(Guid id)
        {
            var StartupProgram = await _context.StartupPrograms.FindAsync(id);
            if (StartupProgram == null)
                return ServiceResponse<string>.ErrorResponse("StartupProgram not found");

            _context.StartupPrograms.Remove(StartupProgram);
            await _context.SaveChangesAsync();

            return ServiceResponse<string>.SuccessResponse("StartupProgram deleted successfully");
        }
    }
}
