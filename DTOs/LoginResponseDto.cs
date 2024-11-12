using UserManager.Models;

namespace UserManager.DTOs
{
    public class LoginResponseDto
    {
        public User User { get; set; }
        public string Token { get; set; }
    }
}
