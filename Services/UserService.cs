using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UserManager.Data;
using UserManager.DTOs;
using UserManager.Models;

namespace UserManager.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly IMapper _mapper;

        public UserService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
            _mapper = mapper;
        }

        public User Authenticate(string email, string password)
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == email);
            if (user == null)
                return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (result == PasswordVerificationResult.Failed)
                return null;

            return user;
        }

        public async Task<(bool Success, string Message)> CreateUserAsync(RegisterDto model, UserRole role)
        {
            if (_context.Users.Any(x => x.Email == model.Email))
                return (false, "Email is already taken");

            var user = _mapper.Map<User>(model);
            user.Id = Guid.NewGuid();
            user.Role = role;
            user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return (true, "User created successfully");
        }

        public async Task<(bool Success, string Message)> CreateClientAsync(RegisterDto model, Guid managerId)
        {
            if (_context.Users.Any(x => x.Email == model.Email))
                return (false, "Email is already taken");

            var user = _mapper.Map<User>(model);
            user.Id = Guid.NewGuid();
            user.Role = UserRole.Client;
            user.ManagerId = managerId;
            user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return (true, "Client created successfully");
        }

        public User GetById(Guid userId)
        {
            return _context.Users.Find(userId);
        }

        public async Task<(bool Success, string Message)> UpdateUserAsync(Guid userId, RegisterDto model)
        {
            var user = _context.Users.Find(userId);
            if (user == null)
                return (false, "User not found");

            user.Email = model.Email;
            user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return (true, "Profile updated successfully");
        }

        public async Task<(bool Success, string Message)> DeleteUserAsync(Guid userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null)
                return (false, "User not found");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return (true, "Profile deleted successfully");
        }
    }
}
