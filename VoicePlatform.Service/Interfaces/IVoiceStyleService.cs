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
    public interface IVoiceStyleService
    {
        Task<Response> GetVoiceStyles(Pagination pagination, string searchString, bool isAsc);

        Task<MiniReqRes> GetVoiceStyleById(Guid id);

        Task<object> CreateVoiceStyle(string voiceStyle);

        Task<bool> UpdateVoiceStyle(MiniReqRes voiceStyle);

        Task<bool> DeleteVoiceStyle(Guid id);
    }
}
