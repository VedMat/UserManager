using UserManager.DTOs;
using UserManager.Models;

namespace UserManager.Services
{
    public interface IUserService
    {
        User Authenticate(string email, string password);
        Task<ServiceResponse<string>> CreateUserAsync(RegisterDto model, UserRole role);
        Task<ServiceResponse<string>> CreateClientAsync(RegisterDto model, Guid managerId);
        User GetById(Guid userId);
        Task<ServiceResponse<User>> UpdateUserAsync(Guid userId, RegisterDto model);
        Task<ServiceResponse<string>> DeleteUserAsync(Guid userId);
    }
}
