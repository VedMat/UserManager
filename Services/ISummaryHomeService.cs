using UserManager.DTOs;
using UserManager.Models;

namespace UserManager.Services
{
    public interface ISummaryHomeService
    {
        Task<ServiceResponse<Dictionary<string, double>>> GetSummaryHome();
    }
}
