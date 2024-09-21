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

        public async Task<(bool Success, string Message)> CreateResourceAsync(ResourceDto model, Guid ownerId)
        {
            var resource = _mapper.Map<Resource>(model);
            resource.Id = Guid.NewGuid();
            resource.OwnerId = ownerId;

            _context.Resources.Add(resource);
            await _context.SaveChangesAsync();

            return (true, "Resource created successfully");
        }

        public IEnumerable<ResourceDto> GetResources(Guid userId, string role)
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
            return _mapper.Map<IEnumerable<ResourceDto>>(resources);
        }

        public async Task<(bool Success, string Message)> UpdateResourceAsync(Guid resourceId, ResourceDto model, Guid userId)
        {
            var resource = _context.Resources.Find(resourceId);
            if (resource == null)
                return (false, "Resource not found");

            if (resource.OwnerId != userId)
                return (false, "You are not authorized to update this resource");

            resource.Title = model.Title;
            resource.Url = model.Url;

            _context.Resources.Update(resource);
            await _context.SaveChangesAsync();

            return (true, "Resource updated successfully");
        }

        public async Task<(bool Success, string Message)> DeleteResourceAsync(Guid resourceId, Guid userId)
        {
            var resource = _context.Resources.Find(resourceId);
            if (resource == null)
                return (false, "Resource not found");

            if (resource.OwnerId != userId)
                return (false, "You are not authorized to delete this resource");

            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();

            return (true, "Resource deleted successfully");
        }
    }
}
