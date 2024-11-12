using System.ComponentModel.DataAnnotations;

namespace UserManager.DTOs
{
    public class StartupDto
    {
        [Required]
        [StringLength(50)]
        public string Source { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Url]
        public string? Website { get; set; }
        public string? Country { get; set; }

        public string? Logo { get; set; }

        public string? Industry { get; set; }

        public string? Technology { get; set; }

        public string? Tags { get; set; }

        [Range(0, (double)decimal.MaxValue)]
        public decimal? Funding { get; set; }

        public string? EvolutionState { get; set; }

        public string? FundStage { get; set; }

        public string? LegalNature { get; set; }

        public string? PitchDeck { get; set; }

        public string? Note { get; set; }
    }
}
