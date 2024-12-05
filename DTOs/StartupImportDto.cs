namespace UserManager.DTOs
{
    public class StartupImportModel
    {
        // Campi per Startup
        public string? Source { get; set; }
        public string? Name { get; set; }
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
        public string? StartupProgramName { get; set; }
                     
        // Campi per ?Company
        public string? CompanyName { get; set; }
        public string? CompanyMail { get; set; }
        public string? CompanyPhone { get; set; }
        public string? CompanyLinkedin { get; set; }
        public string? CompanyWebsite { get; set; }
        public string? CompanyNote { get; set; }
        public string? CompanyIsClient { get; set; }
                     
        // Campi per ?Contact
        public string? ContactName { get; set; }
        public string? ContactSurname { get; set; }
        public string? ContactMail { get; set; }
        public string? ContactRole { get; set; }
        public string? ContactPhone { get; set; }
        public string? ContactLinkedin { get; set; }
        public string? ContactNote { get; set; }
    }

}
