using UserManager.DTOs;
using UserManager.Models;

namespace UserManager.Services
{
    public interface IResourceService
    {
        Task<(bool Success, string Message)> CreateResourceAsync(ResourceDto model, Guid ownerId);
        IEnumerable<ResourceDto> GetResources(Guid userId, string role);
        Task<(bool Success, string Message)> UpdateResourceAsync(Guid resourceId, ResourceDto model, Guid userId);
        Task<(bool Success, string Message)> DeleteResourceAsync(Guid resourceId, Guid userId);
    }
}
