using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Produces("application/json")]
    [Authorize]
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

        /// <summary>
        /// Autentica un utente e restituisce un token JWT.
        /// </summary>
        /// <param name="model">Credenziali di login.</param>
        /// <returns>Token JWT se l'autenticazione ha successo.</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse<string>))]
        public IActionResult Login([FromBody] LoginDto model)
        {
            var user = _userService.Authenticate(model.Email, model.Password);
            if (user == null)
                return Unauthorized(ApiResponse<string>.ErrorResponse("Invalid credentials"));

            var token = JwtHelper.GenerateJwtToken(user, _configuration);

            return Ok(ApiResponse<string>.SuccessResponse(token, "Login successful"));
        }

        /// <summary>
        /// Richiede un reset della password inviando un token all'email dell'utente.
        /// </summary>
        /// <param name="model">Dettagli per il reset della password.</param>
        /// <returns>Conferma della generazione del token.</returns>
        [HttpPost("requestpasswordreset")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> RequestPasswordReset([FromBody] RequestPasswordResetDto model)
        {
            if (string.IsNullOrWhiteSpace(model.Email))
            {
                return BadRequest(ApiResponse<string>.ErrorResponse("Email is required"));
            }

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
                return Ok(ApiResponse<string>.SuccessResponse(null, "If the email is registered, a password reset link will be sent"));

            // Generate a secure token
            user.PasswordResetToken = GenerateSecureToken();
            user.PasswordResetTokenExpires = DateTime.UtcNow.AddHours(1); // Token valid for 1 hour

            // Update user in the database
            _context.Update(user);
            await _context.SaveChangesAsync();

            return Ok(ApiResponse<string>.SuccessResponse(user.PasswordResetToken, "Password reset token generated"));
        }

        /// <summary>
        /// Reimposta la password dell'utente utilizzando un token valido.
        /// </summary>
        /// <param name="model">Dettagli per la reimpostazione della password.</param>
        /// <returns>Conferma della reimpostazione della password.</returns>
        [HttpPost("resetpassword")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            if (string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.Token) ||
                string.IsNullOrWhiteSpace(model.NewPassword))
            {
                return BadRequest(ApiResponse<string>.ErrorResponse("All fields are required"));
            }

            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse("Invalid token or email"));
            }

            // Validate the token
            if (user.PasswordResetToken != model.Token || user.PasswordResetTokenExpires < DateTime.UtcNow)
            {
                return BadRequest(ApiResponse<string>.ErrorResponse("Invalid or expired token"));
            }

            // Hash the new password
            user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);

            // Invalidate the token
            user.PasswordResetToken = "";
            user.PasswordResetTokenExpires = null;

            // Update user in the database
            _context.Update(user);
            await _context.SaveChangesAsync();

            return Ok(ApiResponse<string>.SuccessResponse("", "Password has been reset successfully"));
        }

        // Metodi di utilità
        private string GenerateSecureToken()
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var tokenData = new byte[32];
                rngCryptoServiceProvider.GetBytes(tokenData);
                return Convert.ToBase64String(tokenData);
            }
        }
    }
}
