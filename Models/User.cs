using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserManager.Models
{
    public enum UserRole
    {
        Admin,
        Manager,
        Client
    }

    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public UserRole Role { get; set; }

        // For Clients, reference to their Manager
        public Guid? ManagerId { get; set; }

        [ForeignKey("ManagerId")]
        public User Manager { get; set; }

        // For Managers, list of their Clients
        public ICollection<User> Clients { get; set; }

        public ICollection<Resource> Resources { get; set; }

        // Fields for password reset
        public string PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpires { get; set; }
    }
}
