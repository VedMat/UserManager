using UserManager.DTOs;
using UserManager.Models;

namespace UserManager.Services
{
    public interface IStartupProgramService
    {
        Task<ServiceResponse<StartupProgram>> CreateStartupProgramAsync(StartupProgramDto StartupProgramDto);
        Task<ServiceResponse<List<StartupProgram>>> GetAllStartupProgramsAsync();
        Task<ServiceResponse<StartupProgram>> GetStartupProgramByIdAsync(Guid id);
        Task<ServiceResponse<StartupProgram>> UpdateStartupProgramAsync(Guid id, StartupProgramDto StartupProgramDto);
        Task<ServiceResponse<string>> DeleteStartupProgramAsync(Guid id);
    }
}
