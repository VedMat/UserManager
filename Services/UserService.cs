using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
            var user = _context.Users.SingleOrDefault(x => x.Email == email && x.Active == true);
            if (user == null)
                return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (result == PasswordVerificationResult.Failed)
                return null;

            return user;
        }

        public async Task<ServiceResponse<List<User>>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();

            return ServiceResponse<List<User>>.SuccessResponse(users, "Startups retrieved successfully");
        }

        public async Task<ServiceResponse<User>> CreateUserAsync(RegisterDto model, UserRole role)
        {
            if (_context.Users.Any(x => x.Email == model.Email))
                return ServiceResponse<User>.ErrorResponse("Email is already taken");

            var user = _mapper.Map<User>(model);
            user.Id = Guid.NewGuid();
            user.Role = role;
            user.PasswordResetToken = "";
            user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);
            user.Active = true;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return ServiceResponse<User>.SuccessResponse(user, "User created successfully");
        }

        public User GetById(Guid userId)
        {
            return _context.Users.Find(userId);
        }

        public async Task<ServiceResponse<User>> UpdateUserAsync(Guid userId, UserDto model)
        {
            var user = _context.Users.Find(userId);
            if (user == null)
                return ServiceResponse<User>.ErrorResponse("User not found");

            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Email = model.Email;
            user.Active = model.Active;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return ServiceResponse<User>.SuccessResponse(user, "Profile updated successfully");
        }

        public async Task<ServiceResponse<string>> DeleteUserAsync(Guid userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null)
                return ServiceResponse<string>.ErrorResponse("User not found");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return ServiceResponse<string>.SuccessResponse("", "Profile deleted successfully");
        }
    }
}
