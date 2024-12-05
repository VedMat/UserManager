using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserManager.Models;
using UserManager.Data;
using UserManager.DTOs;
using UserManager.Services;
using CsvHelper;
using System.Globalization;
using Microsoft.AspNetCore.Hosting;

namespace UserManager.Services
{
    public class ImportEntityService : IImportEntityService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ImportEntityService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<ServiceResponse<string>> ImportDataAsync(IFormFile file)
        {
            using (var stream = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(stream, CultureInfo.InvariantCulture))
            {
                try
                {
                    var records = csv.GetRecords<StartupImportModel>().ToList();

                    foreach (var record in records)
                    {
                        Company company = null;
                        Contact contact = null;
                        Startup startup = null;

                        Startup existingStartup = await _context.Startups.FirstOrDefaultAsync(x => x.Name == record.Name);

                        // Creazione della startup
                        if (existingStartup == null)
                        {
                            startup = new Startup
                            {
                                Source = record.Source,
                                Name = record.Name,
                                Description = record.Description,
                                Website = record.Website,
                                Country = record.Country,
                                Logo = record.Logo,
                                Industry = record.Industry,
                                Technology = record.Technology,
                                Tags = record.Tags,
                                Funding = record.Funding,
                                EvolutionState = record.EvolutionState,
                                FundStage = record.FundStage,
                                LegalNature = record.LegalNature,
                                PitchDeck = record.PitchDeck,
                                Note = record.Note,
                            };
                        };

                        Company existingCompany = await _context.Companies.FirstOrDefaultAsync(x => x.Name == record.CompanyName);
                        // Creazione della company (se i dati sono presenti)
                        if (!string.IsNullOrEmpty(record.CompanyName) && existingStartup == null)
                        {
                            company = new Company
                            {
                                Name = record.CompanyName,
                                Mail = record.CompanyMail,
                                Phone = record.CompanyPhone,
                                Linkedin = record.CompanyLinkedin,
                                Website = record.CompanyWebsite,
                                Note = record.CompanyNote,
                                isClient = record.CompanyIsClient,
                                Startup = startup // Associa la startup alla company
                            };

                            _context.Companies.Add(company);
                        }

                        Contact existingContact = await _context.Contacts.FirstOrDefaultAsync(x => x.Name == record.ContactName && x.Surname == record.ContactSurname && x.Mail == record.ContactMail);
                        // Creazione del contatto (se i dati sono presenti)
                        if (!string.IsNullOrEmpty(record.ContactName) && !string.IsNullOrEmpty(record.ContactSurname) && existingContact == null)
                        {
                            contact = new Contact
                            {
                                Name = record.ContactName,
                                Surname = record.ContactSurname,
                                Mail = record.ContactMail,
                                Role = record.ContactRole,
                                Phone = record.ContactPhone,
                                Linkedin = record.ContactLinkedin,
                                Note = record.ContactNote,
                                Company = company,
                                Startup = startup // Associa la startup al contatto
                            };

                            _context.Contacts.Add(contact);
                        }

                        // Aggiungi la startup al contesto
                        _context.Startups.Add(startup);
                    }

                    await _context.SaveChangesAsync();

                    return ServiceResponse<string>.SuccessResponse("Startup updated successfully");
                }
                catch (Exception ex)
                {
                    return ServiceResponse<string>.ErrorResponse("Startup not found");
                }
            }
        }
    }
}
