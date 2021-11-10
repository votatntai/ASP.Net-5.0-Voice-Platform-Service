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
    public interface IUsagePurposeService
    {
        Task<Response> GetUsagePurposes(Pagination pagination, string searchString, bool isAsc);

        Task<MiniReqRes> GetUsagePurposeById(Guid id);

        Task<object> CreateUsagePurpose(string usagePurpose);

        Task<bool> UpdateUsagePurpose(MiniReqRes usagePurpose);

        Task<bool> DeleteUsagePurpose(Guid id);
    }
}
