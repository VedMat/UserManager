using UserManager.DTOs;
using UserManager.Models;

namespace UserManager.Services
{
    public interface IUserService
    {
        User Authenticate(string email, string password);
        Task<ServiceResponse<List<User>>> GetAllUsersAsync();
        Task<ServiceResponse<User>> CreateUserAsync(RegisterDto model, UserRole role);
        User GetById(Guid userId);
        Task<ServiceResponse<User>> UpdateUserAsync(Guid userId, UserDto model);
        Task<ServiceResponse<string>> DeleteUserAsync(Guid userId);
    }
}
