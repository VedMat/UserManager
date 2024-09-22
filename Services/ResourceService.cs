using AutoMapper;
using UserManager.Data;
using UserManager.DTOs;
using UserManager.Models;

namespace UserManager.Services
{
    public class ResourceService : IResourceService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ResourceService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ResourceDto>> CreateResourceAsync(ResourceDto model, Guid ownerId)
        {
            var resource = _mapper.Map<Resource>(model);
            resource.Id = Guid.NewGuid();
            resource.OwnerId = ownerId;

            _context.Resources.Add(resource);
            await _context.SaveChangesAsync();

            return new ServiceResponse<ResourceDto>(_mapper.Map<ResourceDto>(resource), true, "Resource created successfully");
        }

        public ServiceResponse<List<Resource>> GetResources(Guid userId, string role)
        {
            IQueryable<Resource> query = _context.Resources;

            if (role == UserRole.Client.ToString())
            {
                query = query.Where(r => r.OwnerId == userId);
            }
            else if (role == UserRole.Manager.ToString())
            {
                var clientIds = _context.Users
                    .Where(u => u.ManagerId == userId)
                    .Select(u => u.Id)
                    .ToList();

                query = query.Where(r => clientIds.Contains(r.OwnerId));
            }

            var resources = query.ToList();
            return new ServiceResponse<List<Resource>> (resources, true, "");
        }

        public async Task<ServiceResponse<string>> UpdateResourceAsync(Guid resourceId, ResourceDto model, Guid userId)
        {
            var resource = _context.Resources.Find(resourceId);
            if (resource == null)
                return new ServiceResponse<string> ("", false, "Resource not found");

            if (resource.OwnerId != userId)
                return new ServiceResponse<string>("", false, "You are not authorized to update this resource");

            resource.Title = model.Title;
            resource.Url = model.Url;

            _context.Resources.Update(resource);
            await _context.SaveChangesAsync();

            return new ServiceResponse<string>("", true, "Resource updated successfully");
        }

        public async Task<ServiceResponse<string>> DeleteResourceAsync(Guid resourceId, Guid userId)
        {
            var resource = _context.Resources.Find(resourceId);
            if (resource == null)
                return new ServiceResponse<string>("", false, "Resource not found");

            if (resource.OwnerId != userId)
                return new ServiceResponse<string>("", false, "You are not authorized to delete this resource");

            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();

            return new ServiceResponse<string>("", true, "Resource deleted successfully");
        }
    }
}
