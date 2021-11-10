using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoicePlatform.Data.Application;
using VoicePlatform.Data.Entities;
using VoicePlatform.Data.Requests;
using VoicePlatform.Data.Responses;
using VoicePlatform.Utility.Enums;

namespace VoicePlatform.Service.Interfaces
{
    public interface ICustomerService
    {
        Task<object> GetAllCustomer(Pagination pagination, Dictionary<AdminFilterCustomer, string> filter,
            Dictionary<AdminSortCustomer, bool> sort, string searchString);
        Task<CustomerResponse> GetCustomerById(Guid Id);
        Task<object> RegisterCustomer(CustomerRequest customer);
        Task<object> UpdateCustomer(UpdateUserRequest customer, Guid id);
        Task<bool> UpdateAvatar(Guid userId, string url);
        Task<bool> UpdatePassword(string email, string password);
        Task<bool> UpdateStatusCustomer(Guid userId, Guid adminId, UserStatus status);
    }
}
