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
    public class VoiceStyleService : BaseService, IVoiceStyleService
    {
        private readonly IVoiceStyleRepository _voiceStyleRepository;
        private readonly IMapper _mapper;

        public VoiceStyleService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _voiceStyleRepository = unitOfWork.VoiceStyle;
            _mapper = mapper;
        }

        public async Task<object> CreateVoiceStyle(string voiceStyle)
        {
            if (_voiceStyleRepository.GetMany(x => x.Name.Equals(voiceStyle)).Any())
            {
                return new Error()
                {
                    Title = "CreateVoiceStyle",
                    Message = ErrorMessage.VOICESTYLE_EXISTED
                };
            }
            var id = Guid.NewGuid();
            try
            {
                _voiceStyleRepository.Add(new Data.Entities.VoiceStyle()
                {
                    Id = id,
                    Name = voiceStyle
                });
                await _unitOfWork.SaveChanges();
                return id;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }

        public async Task<bool> DeleteVoiceStyle(Guid id)
        {
            try
            {
                _voiceStyleRepository.Remove(new Data.Entities.VoiceStyle()
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

        public async Task<MiniReqRes> GetVoiceStyleById(Guid id)
        {
            try
            {
                return _mapper.Map<MiniReqRes>(_voiceStyleRepository.FirstOrDefault(x => x.Id.Equals(id)));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Response> GetVoiceStyles(Pagination pagination, string searchString, bool isAsc)
        {
            var listVoiceStyles = new List<MiniReqRes>();
            var voiceStyles = _voiceStyleRepository.GetAll();
            try
            {
                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    voiceStyles = voiceStyles.Where(x => x.Name.Contains(searchString));
                }

                if (isAsc)
                {
                    voiceStyles = voiceStyles.OrderBy(x => x.Name);
                }
                else
                {
                    voiceStyles = voiceStyles.OrderByDescending(x => x.Name);
                }
                var total = voiceStyles.Count();
                voiceStyles
                    .Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize)
                    .ToList().ForEach(x => listVoiceStyles.Add(_mapper.Map<MiniReqRes>(x)));
                return Response.OK(listVoiceStyles, total);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateVoiceStyle(MiniReqRes voiceStyle)
        {
            try
            {
                _voiceStyleRepository.Update(new Data.Entities.VoiceStyle()
                {
                    Id = voiceStyle.Id,
                    Name = voiceStyle.Name
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
