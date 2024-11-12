using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserManager.Models;
using UserManager.Data;
using UserManager.DTOs;
using UserManager.Services;

namespace UserManager.Services
{
    public class StartupService : IStartupService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public StartupService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<string>> CreateStartupAsync(StartupDto startupDto)
        {
            var startup = _mapper.Map<Startup>(startupDto);
            await _context.Startups.AddAsync(startup);
            await _context.SaveChangesAsync();

            return ServiceResponse<string>.SuccessResponse("Startup created successfully");
        }

        public async Task<ServiceResponse<List<Startup>>> GetAllStartupsAsync()
        {
            var startups = await _context.Startups.ToListAsync();
            return ServiceResponse<List<Startup>>.SuccessResponse(startups, "Startups retrieved successfully");
        }

        public async Task<ServiceResponse<Startup>> GetStartupByIdAsync(long id)
        {
            var startup = await _context.Startups.FindAsync(id);
            if (startup == null)
                return ServiceResponse<Startup>.ErrorResponse("Startup not found");

            return ServiceResponse<Startup>.SuccessResponse(startup, "Startup retrieved successfully");
        }

        public async Task<ServiceResponse<string>> UpdateStartupAsync(long id, StartupDto startupDto)
        {
            var startup = await _context.Startups.FindAsync(id);
            if (startup == null)
                return ServiceResponse<string>.ErrorResponse("Startup not found");

            _mapper.Map(startupDto, startup);
            _context.Startups.Update(startup);
            await _context.SaveChangesAsync();

            return ServiceResponse<string>.SuccessResponse("Startup updated successfully");
        }

        public async Task<ServiceResponse<string>> DeleteStartupAsync(long id)
        {
            var startup = await _context.Startups.FindAsync(id);
            if (startup == null)
                return ServiceResponse<string>.ErrorResponse("Startup not found");

            _context.Startups.Remove(startup);
            await _context.SaveChangesAsync();

            return ServiceResponse<string>.SuccessResponse("Startup deleted successfully");
        }
    }
}
