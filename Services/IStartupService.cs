using UserManager.Models;
using UserManager.DTOs;
using UserManager.Services;

namespace UserManager.Services
{
    public interface IStartupService
    {
        Task<ServiceResponse<Startup>> CreateStartupAsync(StartupDto startupDto);
        Task<ServiceResponse<List<Startup>>> GetAllStartupsAsync();
        Task<ServiceResponse<Startup>> GetStartupByIdAsync(Guid id);
        Task<ServiceResponse<Startup>> UpdateStartupAsync(Guid id, StartupDto startupDto);
        Task<ServiceResponse<string>> DeleteStartupAsync(Guid id);
    }
}
