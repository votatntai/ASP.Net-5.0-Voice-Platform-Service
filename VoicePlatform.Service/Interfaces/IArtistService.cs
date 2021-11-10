using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VoicePlatform.Data.Application;
using VoicePlatform.Data.Entities;
using VoicePlatform.Data.Requests;
using VoicePlatform.Data.Responses;
using VoicePlatform.Utility.Enums;

namespace VoicePlatform.Service.Interfaces
{
    public interface IArtistService
    {
        Task<QuickArtistResponse> GetArtistById(Guid Id);
        Task<object> AdminSearchArtist(Pagination pagination, 
            string searchString, Dictionary<AdminFilterArtist, string> filter, Dictionary<AdminSortArtist, bool> sort);
        Task<object> CustomerSearchArtist(Pagination pagination,
            string searchString, Dictionary<CustomerFilterArtist, string> filter, bool? isAsc);
        Task<object> RegisterAnArtist(ArtistRequest artist);
        Task<object> UpdateArtist(UpdateUserRequest artist, Guid id);
        Task<bool> UpdateSubArtist(SubArtistRequest artist, Guid id);
        Task<bool> ChangeStatusAstirt(Guid artistId, Guid adminId, UserStatus status);
        Task<bool> UpdateAvatar(Guid userId, string url);
        Task<bool> UpdateBio(Guid userId, string bio, double? price);
        Task<bool> UpdatePasword(string email, string pasword);
        Task<bool> AddVoiceDemo(Guid userId, string url);
        Task<bool> DeleteVoiceDemo(Guid userId, string url);
        Task<string> GetArtistName(Guid id);
        Task<Response> GetListRating(Guid id, Pagination pagination, IEnumerable<int> filter, Dictionary<SortRating, bool> sort);
    }
}
