using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoicePlatform.Data.Application;
using VoicePlatform.Data.Entities;
using VoicePlatform.Data.Responses;

namespace VoicePlatform.Service.Implementations
{
    public interface ICountryService
    {
        Task<Response> GetCountries(Pagination pagination, string searchString, bool isAsc);

        Task<MiniReqRes> GetCountryById(Guid id);

        Task<object> CreateCountry(string country);

        Task<bool> UpdateCountry(MiniReqRes country);

        Task<bool> DeleteCountry(Guid id);
    }
}
