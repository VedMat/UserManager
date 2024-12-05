using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserManager.Models;
using UserManager.Data;
using UserManager.DTOs;
using UserManager.Services;

namespace UserManager.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CompanyService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<Company>> CreateCompanyAsync(CompanyDto companyDto)
        {
            var company = _mapper.Map<Company>(companyDto);

            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();

            return ServiceResponse<Company>.SuccessResponse(company, "Company created successfully");
        }


        public async Task<ServiceResponse<List<Company>>> GetAllCompanysAsync()
        {
            var companys = await _context.Companies.Include(x=> x.Contacts).ToListAsync();
            return ServiceResponse<List<Company>>.SuccessResponse(companys, "Companys retrieved successfully");
        }

        public async Task<ServiceResponse<Company>> GetCompanyByIdAsync(Guid id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
                return ServiceResponse<Company>.ErrorResponse("Company not found");

            return ServiceResponse<Company>.SuccessResponse(company, "Company retrieved successfully");
        }

        public async Task<ServiceResponse<Company>> UpdateCompanyAsync(Guid id, CompanyDto companyDto)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
                return ServiceResponse<Company>.ErrorResponse("Company not found");

            _mapper.Map(companyDto, company);
            _context.Companies.Update(company);
            await _context.SaveChangesAsync();
            company = await _context.Companies.Include(x => x.Contacts).FirstAsync(x => x.Id ==id);

            return ServiceResponse<Company>.SuccessResponse(company, "Company updated successfully");
        }

        public async Task<ServiceResponse<string>> DeleteCompanyAsync(Guid id)
        {
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
                return ServiceResponse<string>.ErrorResponse("Company not found");

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return ServiceResponse<string>.SuccessResponse("Company deleted successfully");
        }
    }
}
