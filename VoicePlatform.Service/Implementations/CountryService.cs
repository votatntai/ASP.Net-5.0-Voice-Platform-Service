using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoicePlatform.Data;
using VoicePlatform.Data.Application;
using VoicePlatform.Data.Entities;
using VoicePlatform.Data.Repositories.Interfaces;
using VoicePlatform.Data.Responses;
using VoicePlatform.Utility.Constants;

namespace VoicePlatform.Service.Implementations
{
    public class CountryService : BaseService, ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _countryRepository = unitOfWork.Country;
            _mapper = mapper;
        }

        public async Task<object> CreateCountry(string country)
        {
            var id = Guid.NewGuid();
            if (_countryRepository.GetMany(x => x.Name.Equals(country)).Any())
            {
                return new Error()
                {
                    Title = "CreateCountry",
                    Message = ErrorMessage.COUNTRY_EXISTED
                };
            }
            try
            {
                _countryRepository.Add(new Data.Entities.Country()
                {
                    Id = id,
                    Name = country
                });
                await _unitOfWork.SaveChanges();
                return id;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }

        public async Task<bool> DeleteCountry(Guid id)
        {
            try
            {
                _countryRepository.Remove(new Data.Entities.Country()
                {
                    Id = id,
                });
                await _unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Response> GetCountries(Pagination pagination, string searchString, bool isAsc)
        {
            var listCountries = new List<MiniReqRes>();
            var countries = _countryRepository.GetAll();
            try
            {
                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    countries = countries.Where(x => x.Name.Contains(searchString));
                }

                if (isAsc)
                {
                    countries = countries.OrderBy(x => x.Name);
                }
                else
                {
                    countries = countries.OrderByDescending(x => x.Name);
                }
                var total = countries.Count();
                countries
                    .Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize)
                    .ToList().ForEach(x => listCountries.Add(_mapper.Map<MiniReqRes>(x)));
                return Response.OK(listCountries, total);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<MiniReqRes> GetCountryById(Guid id)
        {
            try
            {
                return _mapper.Map<MiniReqRes>(_countryRepository.FirstOrDefault(x => x.Id.Equals(id)));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateCountry(MiniReqRes country)
        {
            try
            {
                _countryRepository.Update(new Data.Entities.Country()
                {
                    Id = country.Id,
                    Name = country.Name
                });
                await _unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
