namespace UserManager.Services
{
    // LocationService.cs
    using AutoMapper;
    using UserManager.DTOs;
    using UserManager.Models;
    using Microsoft.EntityFrameworkCore;
    using UserManager.Data;

    public class LocationService : ILocationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public LocationService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<LocationDto>> CreateLocationAsync(LocationDto locationDto)
        {
            var location = _mapper.Map<Location>(locationDto);
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();

            return ApiResponse<LocationDto>.SuccessResponse(_mapper.Map<LocationDto>(location), "Location created successfully");
        }

        public async Task<ApiResponse<List<LocationDto>>> GetAllLocationsAsync()
        {
            var locations = await _context.Locations.Include(l => l.State).ToListAsync();
            var locationDtos = _mapper.Map<List<LocationDto>>(locations);
            return ApiResponse<List<LocationDto>>.SuccessResponse(locationDtos, "Locations retrieved successfully");
        }

        public async Task<ApiResponse<LocationDto>> GetLocationByIdAsync(int id)
        {
            var location = await _context.Locations.Include(l => l.State).FirstOrDefaultAsync(l => l.Id == id);
            if (location == null)
            {
                return ApiResponse<LocationDto>.ErrorResponse("Location not found");
            }
            return ApiResponse<LocationDto>.SuccessResponse(_mapper.Map<LocationDto>(location), "Location retrieved successfully");
        }

        public async Task<ApiResponse<string>> UpdateLocationAsync(int id, LocationDto locationDto)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return ApiResponse<string>.ErrorResponse("Location not found");
            }

            _mapper.Map(locationDto, location);
            _context.Entry(location).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return ApiResponse<string>.SuccessResponse("", "Location updated successfully");
        }

        public async Task<ApiResponse<string>> DeleteLocationAsync(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null)
            {
                return ApiResponse<string>.ErrorResponse("Location not found");
            }

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();

            return ApiResponse<string>.SuccessResponse("", "Location deleted successfully");
        }
    }

}
