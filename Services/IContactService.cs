using UserManager.Models;
using UserManager.DTOs;
using UserManager.Services;

namespace UserManager.Services
{
    public interface IContactService
    {
        Task<ServiceResponse<Contact>> CreateContactAsync(ContactDto contactDto);
        Task<ServiceResponse<List<Contact>>> GetAllContactsAsync();
        Task<ServiceResponse<Contact>> GetContactByIdAsync(Guid id);
        Task<ServiceResponse<List<Contact>>> GetContactByStartupIdAsync(Guid id);
        Task<ServiceResponse<List<Contact>>> GetContactByCompanyIdAsync(Guid id);
        Task<ServiceResponse<Contact>> UpdateContactAsync(Guid id, ContactDto contactDto);
        Task<ServiceResponse<string>> DeleteContactAsync(Guid id);
    }
}
