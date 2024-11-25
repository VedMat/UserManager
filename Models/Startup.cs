using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UserManager.Models
{
    public class Startup
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

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

        public string? Funding { get; set; }

        public string? EvolutionState { get; set; }
        public string? FundStage { get; set; }
        public string? LegalNature { get; set; }
        public string? PitchDeck { get; set; }
        public string? Note { get; set; }

        // Aggiungi la chiave esterna per StartupProgram
        public Guid? StartupProgramId { get; set; }

        // Navigational property
        [ForeignKey("StartupProgramId")]
        [JsonIgnore]
        public StartupProgram? StartupProgram { get; set; }

        // Nuovi campi per le informazioni di contatto
        public string? ContactName { get; set; }
        public string? ContactRole { get; set; }
        [EmailAddress]
        public string? ContactEmail { get; set; }
        [Phone]
        public string? ContactPhone { get; set; }
        public DateTime? LastContacted { get; set; }
        public string? ContactNotes { get; set; }
    }
}
