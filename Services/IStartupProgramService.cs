using UserManager.DTOs;
using UserManager.Models;

namespace UserManager.Services
{
    public interface IStartupProgramService
    {
        Task<ServiceResponse<string>> CreateStartupProgramAsync(StartupProgramDto StartupProgramDto);
        Task<ServiceResponse<List<StartupProgram>>> GetAllStartupProgramsAsync();
        Task<ServiceResponse<StartupProgram>> GetStartupProgramByIdAsync(long id);
        Task<ServiceResponse<string>> UpdateStartupProgramAsync(long id, StartupProgramDto StartupProgramDto);
        Task<ServiceResponse<string>> DeleteStartupProgramAsync(long id);
    }
}
