using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoicePlatform.Data;
using VoicePlatform.Data.Repositories.Interfaces;
using VoicePlatform.Service.Interfaces;
using VoicePlatform.Utility.Enums;

namespace VoicePlatform.Service.Implementations
{
    public class SystemService : BaseService, ISystemService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly IUserRepository _userRepository;
        private readonly IArtistTokenRepository _artistTokenRepository;
        private readonly ICustomerTokenRepository _customerTokenRepository;
        private readonly IAdminTokenRepository _adminTokenRepository;

        public SystemService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _customerRepository = unitOfWork.Customer;
            _artistRepository = unitOfWork.Artist;
            _userRepository = unitOfWork.User;
            _artistTokenRepository = unitOfWork.ArtistToken;
            _customerTokenRepository = unitOfWork.CustomerToken;
            _adminTokenRepository = unitOfWork.AdminToken;
        }

        public async Task<string> GetUserFormDB(string email, Role role)
        {
            switch (role)
            {
                case Role.Admin:
                    var admin = _userRepository.FirstOrDefault(x => x.Email.Equals(email));
                    if (admin != null)
                    {
                        return admin.FirstName;
                    }
                    break;
                case Role.Artist:
                    var artist = _artistRepository.FirstOrDefault(x => x.Email.Equals(email));
                    if (artist != null)
                    {
                        return artist.FirstName;
                    }
                    break;
                case Role.Customer:
                    var customer = _customerRepository.FirstOrDefault(x => x.Email.Equals(email));
                    if (customer != null)
                    {
                        return customer.FirstName;
                    }
                    break;
                default:
                    break;
            }
            return null;
        }

        public async Task<bool> SaveUserToken(Guid userId, string role, string token)
        {

            switch (role)
            {
                case "Customer":
                    if (_customerRepository.GetMany(x => x.Id.Equals(userId)).Any())
                    {
                        if (!_customerTokenRepository.GetMany(x => x.Token.Equals(token)).Any())
                        {
                            _customerTokenRepository.Add(new Data.Entities.CustomerToken()
                            {
                                Id = Guid.NewGuid(),
                                CustomerId = userId,
                                Token = token
                            });
                            await _unitOfWork.SaveChanges();
                        }
                        return true;
                    }
                    break;
                case "Artist":
                    if (_artistRepository.GetMany(x => x.Id.Equals(userId)).Any())
                    {
                        if (!_artistTokenRepository.GetMany(x => x.Token.Equals(token)).Any())
                        {
                            _artistTokenRepository.Add(new Data.Entities.ArtistToken()
                            {
                                Id = Guid.NewGuid(),
                                ArtistId = userId,
                                Token = token
                            });
                            await _unitOfWork.SaveChanges();
                        }
                        return true;
                    }
                    break;
                case "Admin":
                    if (_userRepository.GetMany(x => x.Id.Equals(userId)).Any())
                    {
                        if (!_adminTokenRepository.GetMany(x => x.Token.Equals(token)).Any())
                        {
                            _adminTokenRepository.Add(new Data.Entities.AdminToken()
                            {
                                Id = Guid.NewGuid(),
                                UserId = userId,
                                Token = token
                            });
                            await _unitOfWork.SaveChanges();
                        }
                        return true;
                    }
                    break;
                default:
                    break;
            }
            return false;
        }

        public async Task<List<string>> GetUserToken(Guid userId, string role)
        {

            switch (role)
            {
                case "Customer":
                    var listCustToken = await _customerTokenRepository.GetMany(x => x.CustomerId.Equals(userId)).Select(x => x.Token).ToListAsync();
                    if (listCustToken.Any())
                    {
                        List<string> token = listCustToken.ToList();
                        return token;
                    }
                    break;
                case "Artist":
                    var listArtToken = await _artistTokenRepository.GetMany(x => x.ArtistId.Equals(userId)).Select(x => x.Token).ToListAsync();
                    if (listArtToken.Any())
                    {
                        List<string> token = listArtToken.ToList();
                        return token;
                    }
                    break;
                case "Admin":
                    var listAdToken = await _adminTokenRepository.GetMany(x => x.UserId.Equals(userId)).Select(x => x.Token).ToListAsync();
                    if (listAdToken.Any())
                    {
                        List<string> token = listAdToken.ToList();
                        return token;
                    }
                    break;
                default:
                    break;
            }
            return null;
        }
    }
}
