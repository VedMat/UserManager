using System.ComponentModel.DataAnnotations;
using UserManager.Models.Enum;

namespace UserManager.Models
{
    public class StartupProgram
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string ClientName { get; set; }

        [Required]
        public string ProgramName { get; set; }

        public double Budget { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public string ProgramType { get; set; }

        public string? Description { get; set; }
    }
}
