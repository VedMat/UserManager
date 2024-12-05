using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserManager.Data;
using UserManager.DTOs;
using UserManager.Models;

namespace UserManager.Services
{
    public class SummaryHomeService : ISummaryHomeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SummaryHomeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<Dictionary<string, double>>> GetSummaryHome()
        {
            var summaryHome = new Dictionary<string, double>();

            List<StartupProgram> program = await _context.StartupPrograms.ToListAsync();

            int numStartups = await _context.Startups.CountAsync();
            int numPrograms = program.Count;
            double avgBudget =  program.Sum(x => x.Budget) / numPrograms;
            int another = 10000;

            summaryHome.Add("Total Startups", numStartups);
            summaryHome.Add("Total Programs", numPrograms);
            summaryHome.Add("Average Budget", avgBudget);
            summaryHome.Add("Other", another);
            return ServiceResponse< Dictionary<string, double>>.SuccessResponse(summaryHome, "Summary home retrieved successfully");
        }
    }
}
