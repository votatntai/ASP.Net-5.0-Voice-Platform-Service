using System;
using System.Threading.Tasks;
using VoicePlatform.Data.Application;
using VoicePlatform.Data.Requests;

namespace VoicePlatform.Service.Interfaces
{
    public interface IAuthenticationService
    {
        Task<object> AuthenticateAdmin(AuthenticateRequest authenticate);
        Task<object> AuthenticateCustomer(AuthenticateRequest authenticate);
        Task<object> AuthenticateArtist(AuthenticateRequest authenticate);
        Task<object> AuthenticateGoogleAdmin(string token);
        Task<object> AuthenticateGoogleCustomer(string token);
        Task<object> AuthenticateGoogleArtist(string token);
        Task<Authenticate> GetUserById(Guid Id);
    }
}
