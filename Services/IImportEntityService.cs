using UserManager.Models;
using UserManager.DTOs;
using UserManager.Services;

namespace UserManager.Services
{
    public interface IImportEntityService
    {
        Task<ServiceResponse<string>> ImportDataAsync(IFormFile file);
    }
}
