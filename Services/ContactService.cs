using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserManager.Models;
using UserManager.Data;
using UserManager.DTOs;
using UserManager.Services;

namespace UserManager.Services
{
    public class ContactService : IContactService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ContactService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<Contact>> CreateContactAsync(ContactDto contactDto)
        {
            var contact = _mapper.Map<Contact>(contactDto);

            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();

            return ServiceResponse<Contact>.SuccessResponse(contact, "Contact created successfully");
        }


        public async Task<ServiceResponse<List<Contact>>> GetAllContactsAsync()
        {
            var contacts = await _context.Contacts.Include(x=> x.Company).Include(x => x.Startup).ToListAsync();
            return ServiceResponse<List<Contact>>.SuccessResponse(contacts, "Contacts retrieved successfully");
        }

        public async Task<ServiceResponse<Contact>> GetContactByIdAsync(Guid id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
                return ServiceResponse<Contact>.ErrorResponse("Contact not found");

            return ServiceResponse<Contact>.SuccessResponse(contact, "Contact retrieved successfully");
        }

        public async Task<ServiceResponse<List<Contact>>> GetContactByStartupIdAsync(Guid startupId)
        {
            var contact = await _context.Contacts.Where(x => x.StartupId == startupId).ToListAsync();
            if (contact == null)
                return ServiceResponse<List<Contact>>.ErrorResponse("Contact not found");

            return ServiceResponse<List<Contact>>.SuccessResponse(contact, "Contact retrieved successfully");
        }

        public async Task<ServiceResponse<List<Contact>>> GetContactByCompanyIdAsync(Guid companyId)
        {
            var contact = await _context.Contacts.Where(x => x.CompanyId == companyId).ToListAsync();
            if (contact == null)
                return ServiceResponse<List<Contact>>.ErrorResponse("Contact not found");

            return ServiceResponse<List<Contact>>.SuccessResponse(contact, "Contact retrieved successfully");
        }

        public async Task<ServiceResponse<Contact>> UpdateContactAsync(Guid id, ContactDto contactDto)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
                return ServiceResponse<Contact>.ErrorResponse("Contact not found");

            _mapper.Map(contactDto, contact);
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
            contact = await _context.Contacts.Include(x => x.Company).FirstAsync(x => x.Id ==id);

            return ServiceResponse<Contact>.SuccessResponse(contact, "Contact updated successfully");
        }

        public async Task<ServiceResponse<string>> DeleteContactAsync(Guid id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
                return ServiceResponse<string>.ErrorResponse("Contact not found");

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return ServiceResponse<string>.SuccessResponse("Contact deleted successfully");
        }
    }
}
