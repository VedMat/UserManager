using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using UserManager.Models;

namespace UserManager.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public UserRole Role { get; set; }
        public bool Active { get; set; }
    }
}
