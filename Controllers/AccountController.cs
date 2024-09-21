using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using UserManager.Data;
using UserManager.DTOs;
using UserManager.Helpers;
using UserManager.Models;
using UserManager.Services;

namespace UserManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AccountController(ApplicationDbContext context, IUserService userService, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _userService = userService;
            _configuration = configuration;
            _mapper = mapper;
            _passwordHasher = new PasswordHasher<User>();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto model)
        {
            var user = _userService.Authenticate(model.Email, model.Password);
            if (user == null)
                return Unauthorized(new { message = "Invalid credentials" });

            var token = JwtHelper.GenerateJwtToken(user, _configuration);

            return Ok(new { token });
        }

        [HttpPost("requestpasswordreset")]
        public async Task<IActionResult> RequestPasswordReset([FromBody] RequestPasswordResetDto model)
        {
            if (string.IsNullOrWhiteSpace(model.Email))
            {
                return BadRequest("Email is required.");
            }

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
                return Ok(new { message = "If the email is registered, a password reset link will be sent." });

            // Generate a secure token
            user.PasswordResetToken = GenerateSecureToken();
            user.PasswordResetTokenExpires = DateTime.UtcNow.AddHours(1); // Token valid for 1 hour

            // Update user in the database
            _context.Update(user);
            await _context.SaveChangesAsync();

            // For testing purposes, we'll return the token in the response
            // THIS TOKEN SHOULD BE RETURNED IN A HYPERLINK TO REDIRECT IN A FORM AND RESET A PASSWORD
            return Ok(new { message = "Password reset token generated.", token = user.PasswordResetToken });
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            if (string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.Token) ||
                string.IsNullOrWhiteSpace(model.NewPassword))
            {
                return BadRequest("All fields are required.");
            }

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
            {
                return BadRequest("Invalid token or email.");
            }

            // Validate the token
            if (user.PasswordResetToken != model.Token || user.PasswordResetTokenExpires < DateTime.UtcNow)
            {
                return BadRequest("Invalid or expired token.");
            }

            // Hash the new password
            user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
            //user.PasswordHash = HashPassword(model.NewPassword);

            // Invalidate the token
            user.PasswordResetToken = "";
            user.PasswordResetTokenExpires = null;

            // Update user in the database
            _context.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Password has been reset successfully." });
        }

        // Utility methods
        private string GenerateSecureToken()
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var tokenData = new byte[32];
                rngCryptoServiceProvider.GetBytes(tokenData);
                return Convert.ToBase64String(tokenData);
            }
        }

        private string HashPassword(string password)
        {
            // Use a secure hashing algorithm like PBKDF2
            using (var deriveBytes = new Rfc2898DeriveBytes(password, 16, 10000))
            {
                byte[] salt = deriveBytes.Salt;
                byte[] key = deriveBytes.GetBytes(32);

                var hashBytes = new byte[48]; // 16 bytes salt + 32 bytes key
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(key, 0, hashBytes, 16, 32);

                return Convert.ToBase64String(hashBytes);
            }
        }

        // Optional: Method to verify password hash
        private bool VerifyPassword(string password, string storedHash)
        {
            var hashBytes = Convert.FromBase64String(storedHash);

            var salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                byte[] key = deriveBytes.GetBytes(32);

                for (int i = 0; i < 32; i++)
                {
                    if (hashBytes[i + 16] != key[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
