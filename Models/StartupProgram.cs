using System.ComponentModel.DataAnnotations;
using UserManager.Models.Enum;

namespace UserManager.Models
{
    public class StartupProgram
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string ClientName { get; set; }

        [Required]
        public string ProgramName { get; set; }

        public double Budget {  get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public StartupProgramType ProgramType { get; set; }

        public string? Description { get; set; }

        public ICollection<Startup>? Startups { get; set; }
    }
}
