using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoicePlatform.Data.Application;
using VoicePlatform.Data.Entities;
using VoicePlatform.Data.Responses;

namespace VoicePlatform.Service.Interfaces
{
    public interface IGenderService
    {
        Task<Response> GetGenders(Pagination pagination, string searchString, bool isAsc);

        Task<MiniReqRes> GetGenderById(Guid id);

        Task<object> CreateGender(string gender);

        Task<bool> UpdateGender(MiniReqRes gender);

        Task<bool> DeleteGender(Guid id);
    }
}
