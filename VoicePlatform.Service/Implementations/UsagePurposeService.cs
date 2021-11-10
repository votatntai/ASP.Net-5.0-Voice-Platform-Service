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
    public class UsagePurposeService : BaseService, IUsagePurposeService
    {
        private readonly IUsagePurposeRepository _usagePurposeRepository;
        private readonly IMapper _mapper;

        public UsagePurposeService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _usagePurposeRepository = unitOfWork.UsagePurpose;
            _mapper = mapper;
        }

        public async Task<Response> GetUsagePurposes(Pagination pagination, string searchString, bool isAsc)
        {
            var listUsagePurposes = new List<MiniReqRes>();
            var usagePurposes = _usagePurposeRepository.GetAll();
            try
            {
                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    usagePurposes = usagePurposes.Where(x => x.Name.Contains(searchString));
                }

                if (isAsc)
                {
                    usagePurposes = usagePurposes.OrderBy(x => x.Name);
                }
                else
                {
                    usagePurposes = usagePurposes.OrderByDescending(x => x.Name);
                }
                var total = usagePurposes.Count();
                usagePurposes
                    .Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize)
                    .ToList().ForEach(x => listUsagePurposes.Add(_mapper.Map<MiniReqRes>(x)));
                return Response.OK(listUsagePurposes, total);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<object> CreateUsagePurpose(string usagePurpose)
        {
            if (_usagePurposeRepository.GetMany(x => x.Name.Equals(usagePurpose)).Any())
            {
                return new Error()
                {
                    Title = "CreateUsagePurpose",
                    Message = ErrorMessage.USAGEPURPOSE_EXISTED
                };
            }
            var id = Guid.NewGuid();
            try
            {
                _usagePurposeRepository.Add(new Data.Entities.UsagePurpose()
                {
                    Id = id,
                    Name = usagePurpose
                });
                await _unitOfWork.SaveChanges();
                return id;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }

        public async Task<bool> DeleteUsagePurpose(Guid id)
        {
            try
            {
                _usagePurposeRepository.Remove(new Data.Entities.UsagePurpose()
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

        public async Task<MiniReqRes> GetUsagePurposeById(Guid id)
        {
            try
            {
                return _mapper.Map<MiniReqRes>(_usagePurposeRepository.FirstOrDefault(x => x.Id.Equals(id)));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateUsagePurpose(MiniReqRes gender)
        {
            try
            {
                _usagePurposeRepository.Update(new Data.Entities.UsagePurpose()
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
