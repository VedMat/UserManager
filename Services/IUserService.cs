using UserManager.DTOs;
using UserManager.Models;

namespace UserManager.Services
{
    public interface IUserService
    {
        User Authenticate(string email, string password);
        Task<(bool Success, string Message)> CreateUserAsync(RegisterDto model, UserRole role);
        Task<(bool Success, string Message)> CreateClientAsync(RegisterDto model, Guid managerId);
        User GetById(Guid userId);
        Task<(bool Success, string Message)> UpdateUserAsync(Guid userId, RegisterDto model);
        Task<(bool Success, string Message)> DeleteUserAsync(Guid userId);
    }
}
