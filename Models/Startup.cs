using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UserManager.Models;
using System.Text.Json.Serialization;

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

    public Guid? StartupProgramId { get; set; }

    [ForeignKey("StartupProgramId")]
    [JsonIgnore]
    public StartupProgram? StartupProgram { get; set; }

    // One-to-many relationship with Contact
    [JsonIgnore]
    public ICollection<Contact>? Contacts { get; set; }
}
