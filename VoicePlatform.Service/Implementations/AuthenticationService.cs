using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VoicePlatform.Data;
using VoicePlatform.Data.Application;
using VoicePlatform.Data.Repositories.Interfaces;
using VoicePlatform.Data.Requests;
using VoicePlatform.Data.Responses;
using VoicePlatform.Service.Helpers;
using VoicePlatform.Service.Interfaces;
using VoicePlatform.Utility.Constants;
using VoicePlatform.Utility.Enums;
using VoicePlatform.Utility.Settings;

namespace VoicePlatform.Service.Implementations
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly AppSettings _appSettings;

        public AuthenticationService(IUnitOfWork unitOfWork, IOptions<AppSettings> appSettings) : base(unitOfWork)
        {
            _appSettings = appSettings.Value;
            _artistRepository = unitOfWork.Artist;
            _userRepository = unitOfWork.User;
            _customerRepository = unitOfWork.Customer;
        }

        public async Task<object> AuthenticateAdmin(AuthenticateRequest authenticate)
        {
            List<Error> errors = new List<Error>();
            var user = _userRepository.GetMany(x => x.Username.Equals(authenticate.Username) && x.Password.Equals(authenticate.Password));
            if (user.Count() > 0)
            {
                var ntoken = generateJwtToken(await user.Select(x => new Authenticate
                {
                    Id = x.Id,
                    Username = x.Username,
                    Role = ((Role)x.Role).ToString(),
                    Status = ((UserStatus)x.Status).ToString()
                }).FirstOrDefaultAsync());

                return Response.OK(await user.Select(x => new UserResponse
                {
                    Id = x.Id,
                    Username = x.Username,
                    Email = x.Email,
                    Phone = x.Phone,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    AvatarUrl = x.Avatar,
                    Gender = x.GenderNavigation.Name,
                    Role = ((Role)x.Role).ToString(),
                    Status = ((UserStatus)x.Status).ToString(),
                    Token = ntoken
                }).FirstOrDefaultAsync());
            }
            errors.Add(addError("Authenticate", ErrorMessage.LOGIN_FAIL));
            return errors;
        }

        public async Task<object> AuthenticateCustomer(AuthenticateRequest authenticate)
        {
            List<Error> errors = new List<Error>();
            var customer = _customerRepository.GetMany(x => x.Username.Equals(authenticate.Username) && x.Password.Equals(authenticate.Password));
            if (customer.Any())
            {
                var ntoken = generateJwtToken(await customer.Select(x => new Authenticate
                {
                    Id = x.Id,
                    Username = x.Username,
                    Role = ((Role)x.Role).ToString(),
                    Status = ((UserStatus)x.Status).ToString()
                }).FirstOrDefaultAsync());
                return Response.OK(await customer.Select(x => new UserResponse
                {
                    Id = x.Id,
                    Username = x.Username,
                    Email = x.Email,
                    Phone = x.Phone,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    AvatarUrl = x.Avatar,
                    Gender = x.GenderNavigation.Name,
                    Role = ((Role)x.Role).ToString(),
                    Status = ((UserStatus)x.Status).ToString(),
                    Token = ntoken
                }).FirstOrDefaultAsync());
            }
            errors.Add(addError("Authenticate", ErrorMessage.LOGIN_FAIL));
            return errors;
        }

        public async Task<object> AuthenticateArtist(AuthenticateRequest authenticate)
        {
            List<Error> errors = new List<Error>();
            var artist = _artistRepository.GetMany(x => x.Username.Equals(authenticate.Username) && x.Password.Equals(authenticate.Password));
            if (artist.Any())
            {
                var ntoken = generateJwtToken(await artist.Select(x => new Authenticate
                {
                    Id = x.Id,
                    Username = x.Username,
                    Role = ((Role)x.Role).ToString(),
                    Status = ((UserStatus)x.Status).ToString()
                }).FirstOrDefaultAsync());

                return Response.OK(await artist.Select(x => new UserResponse
                {
                    Id = x.Id,
                    Username = x.Username,
                    Email = x.Email,
                    Phone = x.Phone,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    AvatarUrl = x.Avatar,
                    Gender = x.GenderNavigation.Name,
                    Role = ((Role)x.Role).ToString(),
                    Status = ((UserStatus)x.Status).ToString(),
                    Token = ntoken
                }).FirstOrDefaultAsync());
            }
            errors.Add(addError("Authenticate", ErrorMessage.LOGIN_FAIL));
            return errors;
        }

        public async Task<Authenticate> GetUserById(Guid Id)
        {
            var customer = _customerRepository.FirstOrDefault(x => x.Id == Id);
            if (customer != null)
            {
                return new Authenticate
                {
                    Id = customer.Id,
                    Username = customer.Username,
                    Role = ((Role)customer.Role).ToString(),
                    Status = ((UserStatus)customer.Status).ToString()
                };
            }
            var artist = _artistRepository.FirstOrDefault(x => x.Id == Id);
            if (artist != null)
            {
                return new Authenticate
                {
                    Id = artist.Id,
                    Username = artist.Username,
                    Role = ((Role)artist.Role).ToString(),
                    Status = ((UserStatus)artist.Status).ToString()
                };
            }
            var user = _userRepository.FirstOrDefault(x => x.Id == Id);
            if (user != null)
            {
                return new Authenticate
                {
                    Id = user.Id,
                    Username = user.Username,
                    Role = ((Role)user.Role).ToString(),
                    Status = ((UserStatus)user.Status).ToString()
                };
            }
            return null;
        }

        public async Task<object> AuthenticateGoogleAdmin(string token)
        {
            List<Error> errors = new List<Error>();

            var fuser = await verifyFirebaseToken(token);
            if (fuser != null)
            {
                var user = _userRepository.GetMany(x => x.Email.Equals(fuser.Email));
                if (user.Any())
                {
                    var authen = await user.Select(x => new Authenticate
                    {
                        Id = x.Id,
                        Role = ((Role)x.Role).ToString(),
                        Status = ((UserStatus)x.Status).ToString(),
                        Username = x.Username
                    }).FirstOrDefaultAsync();
                    var rstoken = generateJwtToken(authen);
                    return Response.OK(await user.Select(x => new UserResponse
                    {
                        Id = x.Id,
                        Username = x.Username,
                        Email = x.Email,
                        Phone = x.Phone,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        AvatarUrl = x.Avatar,
                        Gender = x.GenderNavigation.Name,
                        Role = ((Role)x.Role).ToString(),
                        Status = ((UserStatus)x.Status).ToString(),
                        Token = rstoken,
                    }).FirstOrDefaultAsync());
                }
                errors.Add(addError("Authenticate", ErrorMessage.LOGIN_FAIL));
                return errors;
            }
            errors.Add(addError("Authenticate", ErrorMessage.TOKEN_INVALID));
            return errors;
        }

        public async Task<object> AuthenticateGoogleCustomer(string token)
        {
            List<Error> errors = new List<Error>();

            var user = await verifyFirebaseToken(token);
            if (user != null)
            {
                var customer = _customerRepository.GetMany(x => x.Email.Equals(user.Email));
                if (customer.Any())
                {
                    var authen = await customer.Select(x => new Authenticate
                    {
                        Id = x.Id,
                        Role = ((Role)x.Role).ToString(),
                        Status = ((UserStatus)x.Status).ToString(),
                        Username = x.Username
                    }).FirstOrDefaultAsync();
                    var rstoken = generateJwtToken(authen);
                    return Response.OK(await customer.Select(x => new UserResponse
                    {
                        Id = x.Id,
                        Username = x.Username,
                        Email = x.Email,
                        Phone = x.Phone,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        AvatarUrl = x.Avatar,
                        Gender = x.GenderNavigation.Name,
                        Role = ((Role)x.Role).ToString(),
                        Status = ((UserStatus)x.Status).ToString(),
                        Token = rstoken,
                    }).FirstOrDefaultAsync());
                }
                errors.Add(addError("Authenticate", ErrorMessage.LOGIN_FAIL));
                return errors;
            }
            errors.Add(addError("Authenticate", ErrorMessage.TOKEN_INVALID));
            return errors;
        }

        public async Task<object> AuthenticateGoogleArtist(string token)
        {
            List<Error> errors = new List<Error>();
            var user = await verifyFirebaseToken(token);
            if (user != null)
            {
                var artist = _artistRepository.GetMany(x => x.Email.Equals(user.Email));
                if (artist.Any())
                {
                    var authen = await artist.Select(x => new Authenticate
                    {
                        Id = x.Id,
                        Role = ((Role)x.Role).ToString(),
                        Status = ((UserStatus)x.Status).ToString(),
                        Username = x.Username
                    }).FirstOrDefaultAsync();
                    var rstoken = generateJwtToken(authen);
                    return Response.OK(await artist.Select(x => new UserResponse
                    {
                        Id = x.Id,
                        Username = x.Username,
                        Email = x.Email,
                        Phone = x.Phone,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        AvatarUrl = x.Avatar,
                        Gender = x.GenderNavigation.Name,
                        Role = ((Role)x.Role).ToString(),
                        Status = ((UserStatus)x.Status).ToString(),
                        Token = rstoken,
                    }).FirstOrDefaultAsync());
                }
                errors.Add(addError("Authenticate", ErrorMessage.LOGIN_FAIL));
                return errors;
            }
            errors.Add(addError("Authenticate", ErrorMessage.TOKEN_INVALID));
            return errors;
        }

        private string generateJwtToken(Authenticate user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()), new Claim("role", user.Role) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private async Task<UserRecord> verifyFirebaseToken(string token)
        {
            var firebaseApp = new InitFirebaseApp();
            var app = firebaseApp.Init();
            FirebaseAuth.GetAuth(app);
            try
            {
                var user = await FirebaseAuth.DefaultInstance.GetUserAsync(token);
                return user;
            }
            catch
            {
                return null;
            }
        }

        private Error addError(string title, string message)
        {
            return new Error()
            {
                Title = title,
                Message = message
            };
        }

    }
}