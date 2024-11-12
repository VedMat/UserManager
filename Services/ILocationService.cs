namespace UserManager.Services
{
    // ILocationService.cs
    using UserManager.DTOs;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILocationService
    {
        Task<ApiResponse<LocationDto>> CreateLocationAsync(LocationDto locationDto);
        Task<ApiResponse<List<LocationDto>>> GetAllLocationsAsync();
        Task<ApiResponse<LocationDto>> GetLocationByIdAsync(int id);
        Task<ApiResponse<string>> UpdateLocationAsync(int id, LocationDto locationDto);
        Task<ApiResponse<string>> DeleteLocationAsync(int id);
    }

}
