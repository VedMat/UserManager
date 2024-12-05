namespace UserManager.DTOs
{
    public class ContactDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string? Mail { get; set; }

        public string? Role { get; set; }

        public string? CompanyId { get; set; }

        public string? CompanyName { get; set; }

        public string? StartupId { get; set; }

        public string? StartupName { get; set; }

        public string? Phone { get; set; }

        public string? Linkedin { get; set; }

        public string? Note { get; set; }
    }
}
