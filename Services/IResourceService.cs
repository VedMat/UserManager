using UserManager.DTOs;
using UserManager.Models;

namespace UserManager.Services
{
    public interface IResourceService
    {
        Task<ServiceResponse<Resource>> CreateResourceAsync(ResourceDto model, Guid ownerId);
        ServiceResponse<List<Resource>> GetResources(Guid userId, string role);
        Task<ServiceResponse<Resource>> UpdateResourceAsync(Guid resourceId, ResourceDto model, Guid userId);
        Task<ServiceResponse<string>> DeleteResourceAsync(Guid resourceId, Guid userId);
    }
}
