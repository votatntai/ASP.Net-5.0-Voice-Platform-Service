using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoicePlatform.Utility.Enums;

namespace VoicePlatform.Service.Interfaces
{
    public interface ISystemService
    {
        Task<string> GetUserFormDB(string email, Role role);
        Task<bool> SaveUserToken(Guid userId, string role, string token);
        Task<List<string>> GetUserToken(Guid userId, string role);
    }
}
