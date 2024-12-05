using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace UserManager.Models
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; }
        [EmailAddress]
        public string? Mail { get; set; }
        public string? Phone { get; set; }
        [Url]
        public string? Linkedin { get; set; }
        [Url]
        public string? Website { get; set; }
        public string? Note { get; set; }
        public string? isClient { get; set; }

        public Guid? StartupId { get; set; }
        [ForeignKey("StartupId")]
        [JsonIgnore]
        public Startup? Startup { get; set; }

        public ICollection<Contact>? Contacts { get; set; }
    }
}
