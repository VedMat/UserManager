using UserManager.DTOs;
using UserManager.Models;

namespace UserManager.Services
{
    public interface IUserService
    {
        User Authenticate(string email, string password);
        Task<ServiceResponse<User>> CreateUserAsync(RegisterDto model, UserRole role);
        Task<ServiceResponse<User>> CreateClientAsync(RegisterDto model, Guid managerId);
        User GetById(Guid userId);
        Task<ServiceResponse<User>> UpdateUserAsync(Guid userId, RegisterDto model);
        Task<ServiceResponse<string>> DeleteUserAsync(Guid userId);
    }
}
