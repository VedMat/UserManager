namespace UserManager.DTOs
{
    public class CompanyDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Mail { get; set; }

        public string? Phone { get; set; }

        public string? Linkedin { get; set; }

        public string? Website { get; set; }

        public string? Note { get; set; }

        public string? IsClient { get; set; }

        public List<Guid>? ContactIds { get; set; }
    }
}
