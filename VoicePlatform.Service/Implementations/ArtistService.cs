using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoicePlatform.Data;
using VoicePlatform.Data.Application;
using VoicePlatform.Data.Entities;
using VoicePlatform.Data.Repositories.Interfaces;
using VoicePlatform.Data.Requests;
using VoicePlatform.Data.Responses;
using VoicePlatform.Service.Interfaces;
using VoicePlatform.Utility.Constants;
using VoicePlatform.Utility.Enums;

#nullable enable

namespace VoicePlatform.Service.Implementations
{
    public class ArtistService : BaseService, IArtistService
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGenderRepository _genderRepository;
        private readonly IVoiceStyleRepository _voiceStyleRepository;
        private readonly IArtistVoiceStyleRepository _artistVoiceStyleRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IArtistCountryRepository _artistCountryRepository;
        private readonly IArtistVoiceDemoRepository _artistVoiceDemoRepository;
        private readonly IVoiceDemoRepository _voiceDemoRepository;
        private readonly IArtistProjectRepository _artistProjectRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public ArtistService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _artistRepository = unitOfWork.Artist;
            _userRepository = unitOfWork.User;
            _genderRepository = unitOfWork.Gender;
            _voiceStyleRepository = unitOfWork.VoiceStyle;
            _countryRepository = unitOfWork.Country;
            _artistCountryRepository = unitOfWork.ArtistCountry;
            _artistVoiceStyleRepository = unitOfWork.ArtistVoiceStyle;
            _artistVoiceDemoRepository = unitOfWork.ArtistVoiceDemo;
            _voiceDemoRepository = unitOfWork.VoiceDemo;
            _artistProjectRepository = unitOfWork.ArtistProject;
            _customerRepository = unitOfWork.Customer;
            _mapper = mapper;
        }

        public async Task<object> AdminSearchArtist(Pagination pagination, string searchString, Dictionary<AdminFilterArtist, string> filter, Dictionary<AdminSortArtist, bool> sort)
        {
            var artists = await _artistRepository.GetAll()
                .Include(x => x.ArtistCountries).ThenInclude(x => x.Country)
                .Include(x => x.ArtistVoiceStyles).ThenInclude(x => x.VoiceStyle).Include(x => x.GenderNavigation)
                .Include(x => x.ArtistVoiceDemos).ThenInclude(x => x.VoiceDemo)
                .Include(x => x.ArtistProjects).ThenInclude(x => x.Artist)
                .ToListAsync();
            if (artists.Any())
            {
                if (!string.IsNullOrEmpty(searchString))
                {
                    artists = artists.Where(x => x.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    x.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                if (filter.Any())
                {
                    foreach (var fil in filter)
                    {
                        switch (fil.Key)
                        {
                            case AdminFilterArtist.Status:
                                Enum.TryParse(fil.Value, out UserStatus artistStatus);
                                switch (artistStatus)
                                {
                                    case UserStatus.Activated:
                                        artists = artists.Where(x => x.Status.Equals((int)UserStatus.Activated)).ToList();
                                        break;
                                    case UserStatus.Banned:
                                        artists = artists.Where(x => x.Status.Equals((int)UserStatus.Banned)).ToList();
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case AdminFilterArtist.Gender:
                                artists = artists.Where(x => x.Gender.Equals(_genderRepository.FirstOrDefault(y => y.Name.Equals(fil.Value)).Id)).ToList();
                                break;
                            case AdminFilterArtist.Country:
                                var artistCId = _artistCountryRepository.GetMany(x => x.Country.Name.Equals(fil.Value)).Select(x => x.ArtistId);
                                artists = artists.Where(x => artistCId.Contains(x.Id)).ToList();
                                break;
                            case AdminFilterArtist.VoiceStyle:
                                var artistVSId = _artistVoiceStyleRepository.GetMany(x => x.VoiceStyle.Name.Equals(fil.Value)).Select(x => x.ArtistId);
                                artists = artists.Where(x => artistVSId.Contains(x.Id)).ToList();
                                break;
                            default:
                                break;
                        }
                    }
                }
                if (sort.Any())
                {
                    var sortBy = sort.FirstOrDefault();
                    switch (sortBy.Key)
                    {
                        case AdminSortArtist.Name:
                            if (sortBy.Value)
                            {
                                artists = artists.OrderBy(x => x.FirstName).ToList();
                            }
                            else
                            {
                                artists = artists.OrderByDescending(x => x.FirstName).ToList();
                            }
                            break;
                        case AdminSortArtist.Email:
                            if (sortBy.Value)
                            {
                                artists = artists.OrderBy(x => x.Email).ToList();
                            }
                            else
                            {
                                artists = artists.OrderByDescending(x => x.Email).ToList();
                            }
                            break;
                        case AdminSortArtist.Gender:
                            if (sortBy.Value)
                            {
                                artists = artists.OrderBy(x => x.Gender).ToList();
                            }
                            else
                            {
                                artists = artists.OrderByDescending(x => x.Gender).ToList();
                            }
                            break;
                        case AdminSortArtist.Status:
                            if (sortBy.Value)
                            {
                                artists = artists.OrderBy(x => x.Status).ToList();
                            }
                            else
                            {
                                artists = artists.OrderByDescending(x => x.Status).ToList();
                            }
                            break;
                        default:
                            break;
                    }
                }
                List<QuickArtistResponse> listAritst = new List<QuickArtistResponse>() { };
                foreach (var artist in artists)
                {
                    //var totalRate = artist.ArtistProjects.Select(x => x.Rate).ToList().Average();
                    var art = new QuickArtistResponse()
                    {
                        Id = artist.Id,
                        Email = artist.Email,
                        Phone = artist.Phone,
                        FirstName = artist.FirstName,
                        LastName = artist.LastName,
                        Avatar = artist.Avatar,
                        Gender = artist.GenderNavigation.Name,
                        Bio = artist.Bio,
                        Price = artist.Price,
                        Rate = artist.Rate,
                        Studio = artist.Studio,
                        Status = ((UserStatus)artist.Status).ToString(),
                        Countries = artist.ArtistCountries.Select(x => x.Country.Name).ToList(),
                        VoiceStyles = artist.ArtistVoiceStyles.Select(x => x.VoiceStyle.Name).ToList(),
                        VoiceDemos = artist.ArtistVoiceDemos.Select(x => x.VoiceDemo.Url).ToList()
                    };
                    listAritst.Add(art);
                }
                var total = listAritst.Count;
                var result = listAritst.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();
                return Response.OK(result, total);
            }
            return null;
        }

        public async Task<object> CustomerSearchArtist(Pagination pagination, string searchString, Dictionary<CustomerFilterArtist, string> filter, bool? isAsc)
        {
            var artists = _artistRepository.GetMany(x => x.Status.Equals((int)UserStatus.Activated))
                .Include(x => x.ArtistCountries).ThenInclude(x => x.Country)
               .Include(x => x.ArtistVoiceStyles).ThenInclude(x => x.VoiceStyle).Include(x => x.GenderNavigation)
               .Include(x => x.ArtistVoiceDemos).ThenInclude(x => x.VoiceDemo).ToList();
            if (artists.Any())
            {
                if (!string.IsNullOrEmpty(searchString))
                {
                    artists = artists.Where(x => x.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase) || 
                    x.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                if (filter.Any())
                {
                    foreach (var fil in filter)
                    {
                        switch (fil.Key)
                        {
                            case CustomerFilterArtist.Gender:
                                var gender = _genderRepository.FirstOrDefault(x => x.Name.Equals(fil.Value));
                                if (gender != null)
                                {
                                    var genderId = gender.Id;
                                    artists = artists.Where(x => x.Gender.Equals(genderId)).ToList();
                                }
                                break;
                            case CustomerFilterArtist.Country:
                                var country = _countryRepository.FirstOrDefault(x => x.Name.Equals(fil.Value));
                                if (country != null)
                                {
                                    var countryId = country.Id;
                                    artists.ToList().ForEach(x => x.ArtistCountries = _artistCountryRepository.GetMany(y => y.ArtistId.Equals(x.Id)).ToList());
                                    artists = artists.Where(x => x.ArtistCountries.Select(i => i.CountryId).Contains(countryId)).ToList();
                                }
                                break;
                            case CustomerFilterArtist.VoiceStyle:
                                var voiceStyle = _voiceStyleRepository.FirstOrDefault(x => x.Name.Equals(fil.Value));
                                if (voiceStyle != null)
                                {
                                    var voiceStyleId = voiceStyle.Id;
                                    artists.ToList().ForEach(x => x.ArtistVoiceStyles = _artistVoiceStyleRepository.GetMany(y => y.ArtistId.Equals(x.Id)).ToList());
                                    artists = artists.Where(x => x.ArtistVoiceStyles.Select(i => i.VoiceStyleId).Contains(voiceStyleId)).ToList();
                                }
                                break;
                            case CustomerFilterArtist.PriceMax:
                                try
                                {
                                    artists = artists.Where(x => x.Price < double.Parse(fil.Value)).ToList();
                                }
                                catch (Exception)
                                {
                                }
                                break;
                            case CustomerFilterArtist.PriceMin:
                                try
                                {
                                    artists = artists.Where(x => x.Price > double.Parse(fil.Value)).ToList();
                                }
                                catch (Exception)
                                {
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }

                if (isAsc != null)
                {
                    if (isAsc == true)
                    {
                        artists = artists.OrderBy(x => x.Price).ToList();
                    }
                    else
                    {
                        artists = artists.OrderByDescending(x => x.Price).ToList();
                    }
                }
                else
                {
                    artists = artists.OrderBy(x => x.CreateDate).ToList();
                }

                List<QuickArtistResponse> listAritst = new List<QuickArtistResponse>() { };
                foreach (var artist in artists)
                {
                    //var totalRate = artist.ArtistProjects.Select(x => x.Rate).ToList().Average();
                    var art = new QuickArtistResponse()
                    {
                        Id = artist.Id,
                        Email = artist.Email,
                        Phone = artist.Phone,
                        FirstName = artist.FirstName,
                        LastName = artist.LastName,
                        Avatar = artist.Avatar,
                        Gender = artist.GenderNavigation.Name,
                        Bio = artist.Bio,
                        Price = artist.Price,
                        Rate = artist.Rate,
                        Studio = artist.Studio,
                        Status = ((UserStatus)artist.Status).ToString(),
                        Countries = artist.ArtistCountries.Select(x => x.Country.Name).ToList(),
                        VoiceStyles = artist.ArtistVoiceStyles.Select(x => x.VoiceStyle.Name).ToList(),
                        VoiceDemos = artist.ArtistVoiceDemos.Select(x => x.VoiceDemo.Url).ToList()
                    };
                    listAritst.Add(art);
                }
                var total = listAritst.Count;
                var result = listAritst.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();
                return Response.OK(result, total);
            }
            return null;
        }
        public async Task<QuickArtistResponse> GetArtistById(Guid id)
        {
            var artist = _artistRepository.GetMany(x => x.Id.Equals(id));
            if (artist.Any())
            {
                return await artist.Select(x => new QuickArtistResponse
                {
                    Id = x.Id,
                    Email = x.Email,
                    Phone = x.Phone,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Avatar = x.Avatar,
                    Bio = x.Bio,
                    Gender = x.GenderNavigation.Name,
                    Price = x.Price,
                    Rate = x.Rate,
                    Studio = x.Studio,
                    Status = ((UserStatus)x.Status).ToString(),
                    Countries = x.ArtistCountries.Select(x => x.Country.Name).ToList(),
                    VoiceStyles = x.ArtistVoiceStyles.Select(x => x.VoiceStyle.Name).ToList(),
                    VoiceDemos = x.ArtistVoiceDemos.Select(x => x.VoiceDemo.Url).ToList()
                }).FirstOrDefaultAsync();
            }
            return null;
        }

        public async Task<object> RegisterAnArtist(ArtistRequest artist)
        {
            List<Error> errors = new List<Error>();
            var errorTitle = "RegisterArtist";
            if (_artistRepository.GetMany(x => x.Username.Equals(artist.Username)).Any())
            {
                errors.Add(addError(errorTitle, ErrorMessage.USERNAME_EXISTED));
            }
            if (_artistRepository.GetMany(x => x.Email.Equals(artist.Email)).Any())
            {
                errors.Add(addError(errorTitle, ErrorMessage.EMAIL_EXISTED));
            }
            if (_artistRepository.GetMany(x => x.Phone.Equals(artist.Phone)).Any())
            {
                errors.Add(addError(errorTitle, ErrorMessage.PHONE_EXISTED));
            }
            if (_genderRepository.GetMany(x => x.Name.Equals(artist.Gender)).Select(x => x.Id).FirstOrDefault() == Guid.Empty)
            {
                errors.Add(addError(errorTitle, ErrorMessage.GENDER_INVALID));
            }
            if (!errors.Any())
            {
                var record = new Artist
                {
                    Id = Guid.NewGuid(),
                    Username = artist.Username,
                    Email = artist.Email,
                    Phone = artist.Phone,
                    Password = artist.Password,
                    FirstName = artist.FirstName,
                    LastName = artist.LastName,
                    Studio = artist.Studio,
                    Avatar = "",
                    Bio = "",
                    Price = 0,
                    Gender = _genderRepository.FirstOrDefault(x => x.Name.Equals(artist.Gender)).Id,
                    Status = 1,
                    CreateDate = DateTime.Now,
                };
                try
                {
                    _artistRepository.Add(record);
                    await _unitOfWork.SaveChanges();
                    return _mapper.Map<QuickArtistResponse>(record, opt => opt.AfterMap((src, dest) => dest.Gender = artist.Gender));
                }
                catch (Exception e)
                {
                    var message = e.Message;
                    return null;
                }
            }
            else
            {
                return errors;
            }
        }

        public async Task<object> UpdateArtist(UpdateUserRequest art, Guid id)
        {
            List<Error> errors = new List<Error>();
            var errorTitle = "UpdateCustomer";
            if (_artistRepository.GetMany(x => x.Email.Equals(art.Email) && !x.Id.Equals(id)).Any())
            {
                errors.Add(addError(errorTitle, ErrorMessage.EMAIL_EXISTED));
            }
            if (_artistRepository.GetMany(x => x.Phone.Equals(art.Phone) && !x.Id.Equals(id)).Any())
            {
                errors.Add(addError(errorTitle, ErrorMessage.PHONE_EXISTED));
            }
            if (_genderRepository.GetMany(x => x.Name.Equals(art.Gender)).Select(x => x.Id).FirstOrDefault() == Guid.Empty)
            {
                errors.Add(addError(errorTitle, ErrorMessage.GENDER_INVALID));
            }
            if (!errors.Any())
            {
                var artist = _artistRepository.FirstOrDefault(x => x.Id.Equals(id));
                try
                {
                    artist.FirstName = art.FirstName;
                    artist.LastName = art.LastName;
                    artist.Email = art.Email;
                    artist.Phone = art.Phone;
                    artist.Studio = art.Studio;
                    artist.Gender = _genderRepository.GetMany(x => x.Name.Equals(art.Gender)).Select(x => x.Id).FirstOrDefault();
                    artist.UpdateDate = DateTime.UtcNow;
                    _artistRepository.Update(artist);
                    await _unitOfWork.SaveChanges();
                }
                catch (Exception)
                {
                    return null;
                }
                return _mapper.Map<QuickArtistResponse>(artist, opt => opt.AfterMap((src, dest) => dest.Gender = art.Gender));
            }
            else
            {
                return errors;
            }
        }

        public async Task<bool> UpdateSubArtist(SubArtistRequest subArt, Guid id)
        {
            var artist = _artistRepository.FirstOrDefault(x => x.Id.Equals(id));
            if (artist != null)
            {
                if (subArt.VoiceStyles.Any())
                {
                    //delete voicestyle
                    _artistVoiceStyleRepository.GetMany(x => x.ArtistId.Equals(artist.Id)).ToList()
                        .ForEach(t =>
                        {
                            if (!subArt.VoiceStyles.Contains(_voiceStyleRepository.FirstOrDefault(x => x.Id.Equals(t.VoiceStyleId)).Name))
                            {
                                _artistVoiceStyleRepository.Remove(t);
                            }
                        });
                    //add more contries
                    subArt.VoiceStyles
                        .ForEach(t =>
                        {
                            if (_voiceStyleRepository.GetMany(x => x.Name.Equals(t)).Any())
                            {
                                if (!_artistVoiceStyleRepository.GetMany(x => x.ArtistId.Equals(artist.Id))
                            .Select(y => y.VoiceStyleId).ToList().Contains(_voiceStyleRepository.FirstOrDefault(z => z.Name.Equals(t)).Id))
                                {
                                    _artistVoiceStyleRepository.Add(new ArtistVoiceStyle
                                    {
                                        VoiceStyleId = _voiceStyleRepository.FirstOrDefault(a => a.Name.Equals(t)).Id,
                                        ArtistId = artist.Id
                                    });
                                }
                            }
                        });
                }

                if (subArt.Countries.Any())
                {
                    //delete voicestyle
                    _artistCountryRepository.GetMany(x => x.ArtistId.Equals(artist.Id)).ToList()
                        .ForEach(t =>
                        {
                            if (!subArt.Countries.Contains(_countryRepository.FirstOrDefault(x => x.Id.Equals(t.CountryId)).Name))
                            {
                                _artistCountryRepository.Remove(t);
                            }
                        });
                    //add more contries
                    subArt.Countries
                        .ForEach(t =>
                        {
                            if (_countryRepository.GetMany(x => x.Name.Equals(t)).Any())
                            {
                                if (!_artistCountryRepository.GetMany(x => x.ArtistId.Equals(artist.Id))
                            .Select(y => y.CountryId).ToList().Contains(_countryRepository.FirstOrDefault(z => z.Name.Equals(t)).Id))
                                {
                                    _artistCountryRepository.Add(new ArtistCountry
                                    {
                                        CountryId = _countryRepository.FirstOrDefault(a => a.Name.Equals(t)).Id,
                                        ArtistId = artist.Id
                                    });
                                }
                            }
                        });
                    try
                    {
                        await _unitOfWork.SaveChanges();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public async Task<bool> ChangeStatusAstirt(Guid artistId, Guid adminId, UserStatus status)
        {
            var artist = _artistRepository.FirstOrDefault(x => x.Id.Equals(artistId));
            try
            {
                artist.Status = (int)status;
                artist.UpdateDate = DateTime.UtcNow;
                artist.UpdateBy = _userRepository.FirstOrDefault(x => x.Id.Equals(adminId)).Id;
                _artistRepository.Update(artist);
                await _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateAvatar(Guid id, string url)
        {
            var artist = _artistRepository.FirstOrDefault(x => x.Id.Equals(id));
            artist.Avatar = url ?? artist.Avatar;
            try
            {
                artist.UpdateDate = DateTime.UtcNow;
                _artistRepository.Update(artist);
                await _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateBio(Guid id, string bio, double? price)
        {
            var artist = _artistRepository.FirstOrDefault(x => x.Id.Equals(id));
            artist.Bio = bio ?? artist.Bio;
            artist.Price = price ?? artist.Price;
            try
            {
                artist.UpdateDate = DateTime.UtcNow;
                _artistRepository.Update(artist);
                await _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdatePasword(string email, string pasword)
        {
            var artist = _artistRepository.FirstOrDefault(x => x.Email.Equals(email));
            if (artist is null)
            {
                return false;
            }
            artist.Password = pasword;
            try
            {
                artist.UpdateDate = DateTime.UtcNow;
                _artistRepository.Update(artist);
                await _unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddVoiceDemo(Guid userId, string url)
        {
            var artist = _artistRepository.FirstOrDefault(x => x.Id.Equals(userId));
            if (artist != null)
            {
                var voiceDemoId = Guid.NewGuid();
                _voiceDemoRepository.Add(new VoiceDemo()
                {
                    Id = voiceDemoId,
                    Url = url
                });
                _artistVoiceDemoRepository.Add(new ArtistVoiceDemo()
                {
                    ArtistId = artist.Id,
                    VoiceDemoId = voiceDemoId
                });
                try
                {
                    await _unitOfWork.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                }
            }
            return false;
        }

        public async Task<bool> DeleteVoiceDemo(Guid userId, string url)
        {
            var artist = _artistRepository.FirstOrDefault(x => x.Id.Equals(userId));
            var voiceDemo = _voiceDemoRepository.FirstOrDefault(x => x.Url.Equals(url));
            if (artist != null && voiceDemo != null)
            {
                _artistVoiceDemoRepository.Remove(new ArtistVoiceDemo()
                {
                    VoiceDemoId = voiceDemo.Id,
                    ArtistId = artist.Id
                });
                _voiceDemoRepository.Remove(voiceDemo);
                try
                {
                    await _unitOfWork.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                }
            }
            return false;
        }

        public async Task<Response> GetListRating(Guid id, Pagination pagination, IEnumerable<int> filter, Dictionary<SortRating, bool> sort)
        {
            bool isArtistExisted = _artistRepository.GetMany(x => x.Id.Equals(id)).Any();
            if (isArtistExisted)
            {
                var listRatingArtist = _artistProjectRepository.GetMany(x => x.ArtistId.Equals(id) && x.ReviewDate != null && x.Rate != null)
                    .Include(x => x.Project);
                if (listRatingArtist.Any())
                {
                    List<RatingResponse> listRatings = new List<RatingResponse>();
                    listRatingArtist.ToList().ForEach(x => {
                        var customer = _customerRepository.FirstOrDefault(y => y.Id.Equals(x.Project.Poster));
                        var rating = _mapper.Map<ArtistProject, RatingResponse>(x);
                        _mapper.Map<Customer, RatingResponse>(customer, rating);
                        rating.ProjectId = x.ProjectId;
                        rating.ProjectName = x.Project.Name;
                        listRatings.Add(rating);
                    });
                    if (filter.Any())
                    {
                        foreach (var fil in filter)
                        {
                            switch (fil)
                            {
                                case 1:
                                    listRatings = listRatings.Where(x => Math.Floor(x.Rate).Equals(1)).ToList();
                                    break;
                                case 2:
                                    listRatings = listRatings.Where(x => Math.Floor(x.Rate).Equals(2)).ToList();
                                    break;
                                case 3:
                                    listRatings = listRatings.Where(x => Math.Floor(x.Rate).Equals(3)).ToList();
                                    break;
                                case 4:
                                    listRatings = listRatings.Where(x => Math.Floor(x.Rate).Equals(4)).ToList();
                                    break;
                                case 5:
                                    listRatings = listRatings.Where(x => Math.Floor(x.Rate).Equals(5)).ToList();
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    if (sort.Any())
                    {
                        foreach (var s in sort.Keys)
                        {
                            bool value;
                            switch (s)
                            {
                                case SortRating.ReviewDate:
                                    if (sort.TryGetValue(s, out value))
                                    {
                                        if (value)
                                        {
                                            listRatings = listRatings.OrderBy(x => x.ReviewDate).ToList();
                                        }
                                        else
                                        {
                                            listRatings = listRatings.OrderByDescending(x => x.ReviewDate).ToList();
                                        }
                                    }
                                    break;
                                case SortRating.Star:
                                    if (sort.TryGetValue(s, out value))
                                    {
                                        if (value)
                                        {
                                            listRatings = listRatings.OrderBy(x => x.Rate).ToList();
                                        }
                                        else
                                        {
                                            listRatings = listRatings.OrderByDescending(x => x.Rate).ToList();
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else
                    {
                        listRatings = listRatings.OrderByDescending(x => x.ReviewDate).ToList();
                    }
                    var total = listRatings.Count();
                    var result = listRatings.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();
                    return Response.OK(result, total);
                }
            }
            return null;
        }

        private Error addError(string title, string message)
        {
            return new Error()
            {
                Title = title,
                Message = message
            };
        }

        public async Task<string> GetArtistName(Guid id)
        {
            var artist = await _artistRepository.GetMany(x => x.Id.Equals(id)).FirstOrDefaultAsync();
            if (artist == null)
            {
                return string.Empty;
            }
            return artist.LastName + " " + artist.FirstName;
        }
    }
}
