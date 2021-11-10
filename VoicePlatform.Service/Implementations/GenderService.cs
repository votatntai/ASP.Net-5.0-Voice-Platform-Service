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
using VoicePlatform.Service.Interfaces;
using VoicePlatform.Utility.Constants;

namespace VoicePlatform.Service.Implementations
{
    public class GenderService : BaseService, IGenderService
    {
        private readonly IGenderRepository _genderRepository;
        private readonly IMapper _mapper;

        public GenderService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _genderRepository = unitOfWork.Gender;
            _mapper = mapper;
        }

        public async Task<Response> GetGenders(Pagination pagination, string searchString, bool isAsc)
        {
            var listGenders = new List<MiniReqRes>();
            var genders = _genderRepository.GetAll();
            try
            {
                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    genders = genders.Where(x => x.Name.Contains(searchString));
                }

                if (isAsc)
                {
                    genders = genders.OrderBy(x => x.Name);
                }
                else
                {
                    genders = genders.OrderByDescending(x => x.Name);
                }
                var total = genders.Count();
                genders
                    .Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize)
                    .ToList().ForEach(x => listGenders.Add(_mapper.Map<MiniReqRes>(x)));
                return Response.OK(listGenders, total);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<object> CreateGender(string gender)
        {
            if (_genderRepository.GetMany(x => x.Name.Equals(gender)).Any())
            {
                return new Error()
                {
                    Title = "CreateGender",
                    Message = ErrorMessage.GENDER_EXISTED
                };
            }
            var id = Guid.NewGuid();
            try
            {
                _genderRepository.Add(new Data.Entities.Gender()
                {
                    Id = id,
                    Name = gender
                });
                await _unitOfWork.SaveChanges();
                return id;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }

        public async Task<bool> DeleteGender(Guid id)
        {
            try
            {
                _genderRepository.Remove(new Data.Entities.Gender()
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

        public async Task<MiniReqRes> GetGenderById(Guid id)
        {
            try
            {
                return _mapper.Map<MiniReqRes>(_genderRepository.FirstOrDefault(x => x.Id.Equals(id)));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateGender(MiniReqRes gender)
        {
            try
            {
                _genderRepository.Update(new Data.Entities.Gender()
                {
                    Id = gender.Id,
                    Name = gender.Name
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
