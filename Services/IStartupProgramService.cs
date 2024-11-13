using UserManager.DTOs;
using UserManager.Models;

namespace UserManager.Services
{
    public interface IStartupProgramService
    {
        Task<ServiceResponse<string>> CreateStartupProgramAsync(StartupProgramDto StartupProgramDto);
        Task<ServiceResponse<List<StartupProgram>>> GetAllStartupProgramsAsync();
        Task<ServiceResponse<StartupProgram>> GetStartupProgramByIdAsync(Guid id);
        Task<ServiceResponse<string>> UpdateStartupProgramAsync(Guid id, StartupProgramDto StartupProgramDto);
        Task<ServiceResponse<string>> DeleteStartupProgramAsync(Guid id);
    }
}
