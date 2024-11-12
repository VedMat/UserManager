using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManager.Models
{
    public class Startup
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Source { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Website { get; set; }
        public string? Country { get; set; }
        public string? Logo { get; set; }
        public string? Industry { get; set; }
        public string? Technology { get; set; }
        public string? Tags { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Funding { get; set; }
        public string? EvolutionState { get; set; }
        public string? FundStage { get; set; }
        public string? LegalNature { get; set; }
        public string? PitchDeck { get; set; }
        public string? Note { get; set; }

        public string? StartupProgramId { get; set; }
        public StartupProgram? StartupPrograms { get; set; }
    }
}