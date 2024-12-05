using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UserManager.Models
{
    public class Contact
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [EmailAddress]
        public string? Mail { get; set; }
        public string? Role { get; set; }

        // Relazione con Startup: ogni contatto appartiene a una startup
        public Guid? StartupId { get; set; }
        [ForeignKey("StartupId")]
        [JsonIgnore]
        public Startup? Startup { get; set; }

        public Guid? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        [JsonIgnore]
        public Company? Company { get; set; }

        public string? Phone { get; set; }
        [Url]
        public string? Linkedin { get; set; }
        public string? Note { get; set; }
    }
}
