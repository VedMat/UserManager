using UserManager.Models;
using UserManager.DTOs;
using UserManager.Services;

namespace UserManager.Services
{
    public interface IStartupService
    {
        Task<ServiceResponse<string>> CreateStartupAsync(StartupDto startupDto);
        Task<ServiceResponse<List<Startup>>> GetAllStartupsAsync();
        Task<ServiceResponse<Startup>> GetStartupByIdAsync(Guid id);
        Task<ServiceResponse<string>> UpdateStartupAsync(Guid id, StartupDto startupDto);
        Task<ServiceResponse<string>> DeleteStartupAsync(Guid id);
    }
}
