using UserManager.Models;
using UserManager.DTOs;
using UserManager.Services;

namespace UserManager.Services
{
    public interface ICompanyService
    {
        Task<ServiceResponse<Company>> CreateCompanyAsync(CompanyDto companyDto);
        Task<ServiceResponse<List<Company>>> GetAllCompanysAsync();
        Task<ServiceResponse<Company>> GetCompanyByIdAsync(Guid id);
        Task<ServiceResponse<Company>> UpdateCompanyAsync(Guid id, CompanyDto companyDto);
        Task<ServiceResponse<string>> DeleteCompanyAsync(Guid id);
    }
}
