using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoicePlatform.Data;
using VoicePlatform.Data.Application;
using VoicePlatform.Data.Entities;
using VoicePlatform.Data.Repositories.Interfaces;
using VoicePlatform.Data.Requests;
using VoicePlatform.Data.Responses;
using VoicePlatform.Service.Helpers;
using VoicePlatform.Service.Interfaces;
using VoicePlatform.Utility.Enums;

namespace VoicePlatform.Service.Implementations
{
    public class ProjectService : BaseService, IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IGenderRepository _genderRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly IArtistProjectFileRepository _artistProjectFileRepository;
        private readonly ICustomerProjectFileRepository _customerProjectFileRepository;
        private readonly IArtistProjectRepository _artistProjectRepository;
        private readonly IProjectCountryRepository _projectCountryRepository;
        private readonly IProjectGenderRepository _projectGenderRepository;
        private readonly IProjectUsagePurposeRepository _projectUsagePurposeRepository;
        private readonly IProjectVoiceStyleRepository _projectVoiceStyleRepository;
        private readonly IUsagePurposeRepository _usagePurposeRepository;
        private readonly IVoiceStyleRepository _voiceStyleRepository;
        private readonly IArtistVoiceStyleRepository _artistVoiceStyleRepository;
        private readonly IArtistVoiceDemoRepository _artistVoiceDemoRepository;
        private readonly IMapper _mapper;

        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _projectRepository = unitOfWork.Project;
            _customerRepository = unitOfWork.Customer;
            _countryRepository = unitOfWork.Country;
            _genderRepository = unitOfWork.Gender;
            _artistRepository = unitOfWork.Artist;
            _artistProjectFileRepository = unitOfWork.ArtistProjectFile;
            _customerProjectFileRepository = unitOfWork.CustomerProjectFile;
            _artistProjectRepository = unitOfWork.ArtistProject;
            _projectCountryRepository = unitOfWork.ProjectCountry;
            _projectGenderRepository = unitOfWork.ProjectGender;
            _projectUsagePurposeRepository = unitOfWork.ProjectUsagePurpose;
            _projectVoiceStyleRepository = unitOfWork.ProjectVoiceStyle;
            _usagePurposeRepository = unitOfWork.UsagePurpose;
            _voiceStyleRepository = unitOfWork.VoiceStyle;
            _artistVoiceDemoRepository = unitOfWork.ArtistVoiceDemo;
            _artistVoiceStyleRepository = unitOfWork.ArtistVoiceStyle;
            _mapper = mapper;
        }

        //Edit CreateProject function with Customer ID Authorize
        public async Task<QuickProjectResponse> CreateProject(Guid userId, ProjectRequest request)
        {
            var project = _mapper.Map<Project>(request, opt => opt.AfterMap((src, dest) =>
            {
                dest.Poster = userId;
                dest.Id = Guid.NewGuid();
                dest.CreateDate = DateTime.UtcNow;
                dest.Status = (int)ProjectStatus.Waiting;
            }));
            try
            {
                _projectRepository.Add(project);

                AddPreProject(request, userId, project.Id);

                await _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {
                return null;
            }
            return _mapper.Map<QuickProjectResponse>(project);
        }

        public async Task<object> GetOwnProject(Guid userId, Pagination pagination, bool isCustomer,
            List<string> filter, bool? isAsc, string searchString, bool? isProcess)
        {
            var projects = await _projectRepository.GetAll()
                .Include(x => x.ArtistProjects).ThenInclude(x => x.Artist)
                .Include(x => x.ProjectUsagePurposes).ThenInclude(x => x.UsagePurpose)
                .Include(x => x.ProjectVoiceStyles).ThenInclude(x => x.VoiceStyle)
                .Include(x => x.ProjectGenders).ThenInclude(x => x.Gender)
                .Include(x => x.ProjectCountries).ThenInclude(x => x.Country)
                .ToListAsync();
            if (isCustomer)
            {
                projects = projects.Where(i => i.Poster.Equals(userId)).ToList();
            }
            else
            {
                projects = projects.Where(i => i.ArtistProjects.Select(i => i.ArtistId).Contains(userId)).ToList();
            }

            if (projects.Any())
            {
                var searchProjects = new List<Project>();

                if (!string.IsNullOrEmpty(searchString))
                {
                    searchProjects = projects.Where(x => x.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                else
                {
                    searchProjects = projects.ToList();
                }

                if (filter.Any())
                {
                    foreach (var fil in filter)
                    {
                        if (isCustomer)
                        {
                            switch (fil)
                            {
                                case nameof(ProjectStatus.Waiting):
                                    searchProjects = searchProjects.Where(x => x.Status.Equals((int)ProjectStatus.Waiting)).ToList();
                                    break;
                                case nameof(ProjectStatus.Pending):
                                    searchProjects = searchProjects.Where(x => x.Status.Equals((int)ProjectStatus.Pending)).ToList();
                                    break;
                                case nameof(ProjectStatus.Process):
                                    searchProjects = searchProjects.Where(x => x.Status.Equals((int)ProjectStatus.Process)).ToList();
                                    break;
                                case nameof(ProjectStatus.Done):
                                    searchProjects = searchProjects.Where(x => x.Status.Equals((int)ProjectStatus.Done)).ToList();
                                    break;
                                case nameof(ProjectStatus.Delete):
                                    searchProjects = searchProjects.Where(x => x.Status.Equals((int)ProjectStatus.Delete)).ToList();
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            switch (fil)
                            {
                                case nameof(InviteStatus.InvitePending):
                                    searchProjects = searchProjects.Where(x => x.ArtistProjects.Where(x => x.ArtistId.Equals(userId)).Select(y => y.Status).Contains((int)InviteStatus.InvitePending)).ToList();
                                    break;
                                case nameof(InviteStatus.ResponsePending):
                                    searchProjects = searchProjects.Where(x => x.ArtistProjects.Where(x => x.ArtistId.Equals(userId)).Select(y => y.Status).Contains((int)InviteStatus.ResponsePending)).ToList();
                                    break;
                                case nameof(InviteStatus.Accept):
                                    searchProjects = searchProjects.Where(x => x.ArtistProjects.Where(x => x.ArtistId.Equals(userId)).Select(y => y.Status).Contains((int)InviteStatus.Accept)).ToList();
                                    break;
                                case nameof(InviteStatus.Deny):
                                    searchProjects = searchProjects.Where(x => x.ArtistProjects.Where(x => x.ArtistId.Equals(userId)).Select(y => y.Status).Contains((int)InviteStatus.Deny)).ToList();
                                    break;
                                case nameof(InviteStatus.Done):
                                    searchProjects = searchProjects.Where(x => x.ArtistProjects.Where(x => x.ArtistId.Equals(userId)).Select(y => y.Status).Contains((int)InviteStatus.Done)).ToList();
                                    break;
                            }
                        }
                    }
                }

                if (isAsc != null)
                {
                    if (isAsc == true)
                    {
                        searchProjects = searchProjects.OrderBy(x => x.CreateDate).ToList();
                    }
                    else
                    {
                        searchProjects = searchProjects.OrderByDescending(x => x.CreateDate).ToList();
                    }
                }
                else
                {
                    if (isCustomer)
                    {
                        searchProjects = searchProjects.OrderByDescending(x => x.CreateDate).ToList();
                    }
                    else
                    {
                        searchProjects = searchProjects.OrderByDescending(x => x.ArtistProjects.Select(x => x.InvitedDate ?? x.RequestedDate ?? x.JoinedDate).FirstOrDefault()).ToList();
                    }
                }

                if (isProcess != null)
                {
                    if (isCustomer)
                    {
                        if (isProcess is true)
                        {
                            searchProjects = searchProjects.Where(x => x.Status == (int)ProjectStatus.Waiting || x.Status == (int)ProjectStatus.Process || x.Status == (int)ProjectStatus.Pending).ToList();
                        }
                        if (isProcess is false)
                        {
                            searchProjects = searchProjects.Where(x => x.Status == (int)ProjectStatus.Done || x.Status == (int)ProjectStatus.Deny || x.Status == (int)ProjectStatus.Delete).ToList();
                        }
                    }
                    else
                    {
                        if (isProcess is true)
                        {
                            searchProjects = searchProjects.Where(x => x.ArtistProjects.FirstOrDefault(x => x.ArtistId.Equals(userId)).Status == (int)InviteStatus.Accept ||
                                x.ArtistProjects.FirstOrDefault(x => x.ArtistId.Equals(userId)).Status == (int)InviteStatus.InvitePending ||
                                x.ArtistProjects.FirstOrDefault(x => x.ArtistId.Equals(userId)).Status == (int)InviteStatus.ResponsePending).ToList();
                        }
                        if (isProcess is false)
                        {
                            searchProjects = searchProjects.Where(x => x.ArtistProjects.FirstOrDefault(x => x.ArtistId.Equals(userId)).Status == (int)InviteStatus.Done ||
                                x.ArtistProjects.FirstOrDefault(x => x.ArtistId.Equals(userId)).Status == (int)InviteStatus.Deny).ToList();
                        }
                    }
                }

                var listProject = new List<QuickProjectResponse>();

                foreach (var project in searchProjects)
                {
                    listProject.Add(_mapper.Map<QuickProjectResponse>(project, opt => opt
                    .AfterMap((desc, src) =>
                    {
                        src.Poster = _mapper.Map<CustomerResponse>(_customerRepository.FirstOrDefault(x => x.Id.Equals(project.Poster)));
                        project.ArtistProjects.ToList().ForEach(x => src.ArtistsStatus.Add(x.ArtistId, ((InviteStatus)x.Status).ToString()));
                    })));
                }
                var total = listProject.Count;
                var data = listProject.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();
                return Response.OK(data, total);
            }

            return null;
        }

        public async Task<object> GetUserProject(Guid userId, Pagination pagination, bool isCustomer,
            List<string> filter, bool? isAsc, string searchString)
        {
            var projects = await _projectRepository.GetAll()
                .Include(x => x.ArtistProjects).ThenInclude(x => x.Artist)
                .Include(x => x.ProjectUsagePurposes).ThenInclude(x => x.UsagePurpose)
                .Include(x => x.ProjectVoiceStyles).ThenInclude(x => x.VoiceStyle)
                .Include(x => x.ProjectGenders).ThenInclude(x => x.Gender)
                .Include(x => x.ProjectCountries).ThenInclude(x => x.Country)
                .ToListAsync();
            if (isCustomer)
            {
                projects = projects.Where(i => i.Poster.Equals(userId)).ToList();
            }
            else
            {
                projects = projects.Where(i => i.ArtistProjects.Select(i => i.ArtistId).Contains(userId)).ToList();
            }

            if (projects.Any())
            {
                var searchProjects = new List<Project>();

                if (!string.IsNullOrEmpty(searchString))
                {
                    searchProjects = projects.Where(x => x.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                else
                {
                    searchProjects = projects.ToList();
                }

                if (filter.Any())
                {
                    foreach (var fil in filter)
                    {
                        switch (fil)
                        {
                            case nameof(ProjectStatus.Waiting):
                                searchProjects = searchProjects.Where(x => x.Status.Equals((int)ProjectStatus.Waiting)).ToList();
                                break;
                            case nameof(ProjectStatus.Pending):
                                searchProjects = searchProjects.Where(x => x.Status.Equals((int)ProjectStatus.Pending)).ToList();
                                break;
                            case nameof(ProjectStatus.Process):
                                searchProjects = searchProjects.Where(x => x.Status.Equals((int)ProjectStatus.Process)).ToList();
                                break;
                            case nameof(ProjectStatus.Done):
                                searchProjects = searchProjects.Where(x => x.Status.Equals((int)ProjectStatus.Done)).ToList();
                                break;
                            case nameof(ProjectStatus.Delete):
                                searchProjects = searchProjects.Where(x => x.Status.Equals((int)ProjectStatus.Delete)).ToList();
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
                        searchProjects = searchProjects.OrderBy(x => x.CreateDate).ToList();
                    }
                    else
                    {
                        searchProjects = searchProjects.OrderByDescending(x => x.CreateDate).ToList();
                    }
                }
                else
                {
                    searchProjects = searchProjects.OrderBy(x => x.CreateDate).ToList();
                }

                var listProject = new List<AdminQuickProjectResponse>();

                foreach (var project in searchProjects)
                {
                    listProject.Add(_mapper.Map<AdminQuickProjectResponse>(project, opt => opt
                    .AfterMap((desc, src) =>
                    {
                        src.Poster = _mapper.Map<CustomerResponse>(_customerRepository.FirstOrDefault(x => x.Id.Equals(project.Poster)));
                        project.ArtistProjects.ToList().ForEach(x =>
                        {
                            var artist = x.Artist;
                            artist.GenderNavigation = _genderRepository.FirstOrDefault(x => x.Id.Equals(artist.Gender));
                            artist.ArtistVoiceStyles = _artistVoiceStyleRepository.GetMany(x => x.ArtistId.Equals(artist.Id)).Include(x => x.VoiceStyle).ToList();
                            artist.ArtistVoiceDemos = _artistVoiceDemoRepository.GetMany(x => x.ArtistId.Equals(artist.Id)).Include(x => x.VoiceDemo).ToList();
                            src.ArtistProjects.Add(_mapper.Map<ArtistProjectResponse>(x, opt =>
                            opt.AfterMap((desc, src) => src.QuickArtistResponse = _mapper.Map<QuickArtistResponse>(artist))));
                        });
                    })));

                }
                var total = listProject.Count;
                var data = listProject.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();
                return Response.OK(data, total);
            }

            return null;
        }

        public async Task<object> GetProject(Pagination pagination,
            Dictionary<AdminFilter, string> filter, Dictionary<AdminSort, bool> sort, string searchString)
        {
            var projects = await _projectRepository.GetAll().ToListAsync();
            if (projects.Any())
            {
                var listProject = new List<AdminProjectResponse>();
                foreach (var project in projects)
                {
                    var customer = await _customerRepository.GetMany(x => x.Id.Equals(project.Poster)).FirstOrDefaultAsync();
                    listProject.Add(_mapper.Map<AdminProjectResponse>(project, opt =>
                    {
                        opt.AfterMap((desc, src) => src.Poster = _mapper.Map<CustomerResponse>(customer));
                    }));
                }
                if (searchString != null)
                {
                    listProject = listProject.Where(x => x.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                if (filter.Any())
                {
                    foreach (var fil in filter)
                    {
                        switch (fil.Key)
                        {
                            case AdminFilter.Status:
                                Enum.TryParse(fil.Value, out ProjectStatus projectStatus);
                                switch (projectStatus)
                                {
                                    case ProjectStatus.Waiting:
                                        listProject = listProject.Where(x => x.Status.Equals(ProjectStatus.Waiting.ToString())).ToList();
                                        break;
                                    case ProjectStatus.Pending:
                                        listProject = listProject.Where(x => x.Status.Equals(ProjectStatus.Pending.ToString())).ToList();
                                        break;
                                    case ProjectStatus.Process:
                                        listProject = listProject.Where(x => x.Status.Equals(ProjectStatus.Process.ToString())).ToList();
                                        break;
                                    case ProjectStatus.Done:
                                        listProject = listProject.Where(x => x.Status.Equals(ProjectStatus.Done.ToString())).ToList();
                                        break;
                                    case ProjectStatus.Delete:
                                        listProject = listProject.Where(x => x.Status.Equals(ProjectStatus.Delete.ToString())).ToList();
                                        break;
                                    case ProjectStatus.Deny:
                                        listProject = listProject.Where(x => x.Status.Equals(ProjectStatus.Deny.ToString())).ToList();
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case AdminFilter.PriceMin:
                                try
                                {
                                    listProject = listProject.Where(x => x.Price >= decimal.Parse(fil.Value)).ToList();
                                }
                                catch (Exception)
                                {
                                }
                                break;
                            case AdminFilter.PriceMax:
                                try
                                {
                                    listProject = listProject.Where(x => x.Price <= decimal.Parse(fil.Value)).ToList();
                                }
                                catch (Exception)
                                {
                                }
                                break;
                            case AdminFilter.CreateDate:
                                listProject = listProject.Where(x => x.CreateDate.Date.Equals(DateTime.Parse(fil.Value).Date)).ToList();
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
                        case AdminSort.Name:
                            if (sortBy.Value)
                            {
                                listProject = listProject.OrderBy(x => x.Name).ToList();
                            }
                            else
                            {
                                listProject = listProject.OrderByDescending(x => x.Name).ToList();
                            }
                            break;
                        case AdminSort.Poster:
                            if (sortBy.Value)
                            {
                                listProject = listProject.OrderBy(x => x.Poster.FirstName).ToList();
                            }
                            else
                            {
                                listProject = listProject.OrderByDescending(x => x.Poster.FirstName).ToList();
                            }
                            break;
                        case AdminSort.Price:
                            if (sortBy.Value)
                            {
                                listProject = listProject.OrderBy(x => x.Price).ToList();
                            }
                            else
                            {
                                listProject = listProject.OrderByDescending(x => x.Price).ToList();
                            }
                            break;
                        case AdminSort.CreateDate:
                            if (sortBy.Value)
                            {
                                listProject = listProject.OrderBy(x => x.CreateDate).ToList();
                            }
                            else
                            {
                                listProject = listProject.OrderByDescending(x => x.CreateDate).ToList();
                            }
                            break;
                        case AdminSort.Status:
                            if (sortBy.Value)
                            {
                                listProject = listProject.OrderBy(x => x.Status).ToList();
                            }
                            else
                            {
                                listProject = listProject.OrderByDescending(x => x.Status).ToList();
                            }
                            break;
                        default:
                            break;
                    }
                }
                if (!sort.ContainsKey(AdminSort.CreateDate))
                {
                    listProject = listProject.OrderByDescending(x => x.CreateDate).ToList();
                }
                var total = listProject.Count;
                var data = listProject.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();
                return Response.OK(data, total);
            }

            return null;
        }

        public async Task<object> GetPendingProject(Pagination pagination, Dictionary<CustomerFilter, string> filter, bool? isAsc, string searchString)
        {
            var projects = _projectRepository.GetMany(x => x.Status.Equals((int)ProjectStatus.Pending))
                .Include(x => x.ProjectUsagePurposes).ThenInclude(x => x.UsagePurpose)
                .Include(x => x.ProjectVoiceStyles).ThenInclude(x => x.VoiceStyle)
                .Include(x => x.ProjectGenders).ThenInclude(x => x.Gender)
                .Include(x => x.ProjectCountries).ThenInclude(x => x.Country)
                .Include(x => x.ArtistProjects)
                .AsEnumerable();

            if (projects.Any())
            {
                if (!string.IsNullOrEmpty(searchString))
                {
                    projects = projects.Where(x => x.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
                }

                if (filter.Any())
                {
                    foreach (var fil in filter)
                    {
                        switch (fil.Key)
                        {
                            case CustomerFilter.UsagePurpose:
                                var usagePurpose = _usagePurposeRepository.FirstOrDefault(x => x.Name.Equals(fil.Value));
                                if (usagePurpose != null)
                                {
                                    var usagePurposeId = usagePurpose.Id;
                                    projects.ToList().ForEach(x => x.ProjectUsagePurposes = _projectUsagePurposeRepository.GetMany(y => y.ProjectId.Equals(x.Id)).ToList());
                                    projects = projects.Where(x => x.ProjectUsagePurposes.Select(i => i.UsagePurposeId).Contains(usagePurposeId));
                                }
                                break;
                            case CustomerFilter.Country:
                                var country = _countryRepository.FirstOrDefault(x => x.Name.Equals(fil.Value));
                                if (country != null)
                                {
                                    var countryId = country.Id;
                                    projects.ToList().ForEach(x => x.ProjectCountries = _projectCountryRepository.GetMany(y => y.ProjectId.Equals(x.Id)).ToList());
                                    projects = projects.Where(x => x.ProjectCountries.Select(i => i.CountryId).Contains(countryId));
                                }
                                break;
                            case CustomerFilter.VoiceStyle:
                                var voiceStyle = _voiceStyleRepository.FirstOrDefault(x => x.Name.Equals(fil.Value));
                                if (voiceStyle != null)
                                {
                                    var voiceStyleId = voiceStyle.Id;
                                    projects.ToList().ForEach(x => x.ProjectVoiceStyles = _projectVoiceStyleRepository.GetMany(y => y.ProjectId.Equals(x.Id)).ToList());
                                    projects = projects.Where(x => x.ProjectVoiceStyles.Select(i => i.VoiceStyleId).Contains(voiceStyleId));
                                }
                                break;
                            case CustomerFilter.Gender:
                                var gender = _genderRepository.FirstOrDefault(x => x.Name.Equals(fil.Value));
                                if (gender != null)
                                {
                                    var genderId = gender.Id;
                                    projects.ToList().ForEach(x => x.ProjectGenders = _projectGenderRepository.GetMany(y => y.ProjectId.Equals(x.Id)).ToList());
                                    projects = projects.Where(x => x.ProjectGenders.Select(i => i.GenderId).Contains(genderId));
                                }
                                break;
                            case CustomerFilter.PriceMin:
                                try
                                {
                                    projects = projects.Where(x => x.Price >= decimal.Parse(fil.Value));
                                }
                                catch (Exception)
                                {
                                }
                                break;
                            case CustomerFilter.PriceMax:
                                try
                                {
                                    projects = projects.Where(x => x.Price <= decimal.Parse(fil.Value));
                                }
                                catch (Exception)
                                {
                                }
                                break;
                            case CustomerFilter.AgeMin:
                                try
                                {
                                    projects = projects.Where(x => x.MinAge >= int.Parse(fil.Value));
                                }
                                catch (Exception)
                                {
                                }
                                break;
                            case CustomerFilter.AgeMax:
                                try
                                {
                                    projects = projects = projects.Where(x => x.MaxAge < int.Parse(fil.Value));
                                }
                                catch (Exception)
                                {
                                }
                                break;
                            case CustomerFilter.CreateDateMin:
                                try
                                {
                                    projects = projects.Where(x => x.CreateDate >= DateTime.Parse(fil.Value));
                                }
                                catch (Exception)
                                {
                                }
                                break;
                            case CustomerFilter.CreateDateMax:
                                try
                                {
                                    projects = projects.Where(x => x.CreateDate <= DateTime.Parse(fil.Value));
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
                        projects = projects.OrderBy(x => x.Price);
                    }
                    else
                    {
                        projects = projects.OrderByDescending(x => x.Price);
                    }
                }
                else
                {
                    projects = projects.OrderBy(x => x.CreateDate);
                }

                var listProject = new List<QuickProjectResponse>();

                foreach (var project in projects)
                {
                    listProject.Add(_mapper.Map<QuickProjectResponse>(project, opt => opt.AfterMap((desc, src) =>
                    {
                        src.Poster = _mapper.Map<CustomerResponse>(_customerRepository.FirstOrDefault(x => x.Id.Equals(project.Poster)));
                        project.ArtistProjects.ToList().ForEach(x => src.ArtistsStatus.Add(x.ArtistId, ((InviteStatus)x.Status).ToString()));
                    })));
                }
                var total = listProject.Count;
                var data = listProject.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();
                return Response.OK(data, total);
            }

            return null;
        }

        public async Task<Response> GetWaitingProject(Pagination pagination, Dictionary<AdminFilter, string> filter, Dictionary<AdminSort, bool> sort, string searchString)
        {
            var projects = _projectRepository.GetMany(x => x.Status.Equals((int)ProjectStatus.Waiting))
                .Include(x => x.ProjectUsagePurposes).ThenInclude(x => x.UsagePurpose)
                .Include(x => x.ProjectVoiceStyles).ThenInclude(x => x.VoiceStyle)
                .Include(x => x.ProjectGenders).ThenInclude(x => x.Gender)
                .Include(x => x.ProjectCountries).ThenInclude(x => x.Country)
                .AsEnumerable();
            if (projects.Any())
            {
                var total = projects.ToList().Count;
                var listProject = new List<AdminProjectResponse>();
                foreach (var project in projects)
                {
                    var customer = await _customerRepository.GetMany(x => x.Id.Equals(project.Poster)).FirstOrDefaultAsync();
                    listProject.Add(_mapper.Map<AdminProjectResponse>(project, opt =>
                    {
                        opt.AfterMap((desc, src) => src.Poster = _mapper.Map<CustomerResponse>(customer));
                    }));
                }
                if (searchString != null)
                {
                    listProject = listProject.Where(x => x.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
                }
                if (filter.Any())
                {
                    foreach (var fil in filter)
                    {
                        switch (fil.Key)
                        {
                            case AdminFilter.Status:
                                Enum.TryParse(fil.Value, out ProjectStatus projectStatus);
                                switch (projectStatus)
                                {
                                    case ProjectStatus.Waiting:
                                        listProject = listProject.Where(x => x.Status.Equals((int)ProjectStatus.Waiting)).ToList();
                                        break;
                                    case ProjectStatus.Pending:
                                        listProject = listProject.Where(x => x.Status.Equals((int)ProjectStatus.Pending)).ToList();
                                        break;
                                    case ProjectStatus.Process:
                                        listProject = listProject.Where(x => x.Status.Equals((int)ProjectStatus.Process)).ToList();
                                        break;
                                    case ProjectStatus.Done:
                                        listProject = listProject.Where(x => x.Status.Equals((int)ProjectStatus.Done)).ToList();
                                        break;
                                    case ProjectStatus.Delete:
                                        listProject = listProject.Where(x => x.Status.Equals((int)ProjectStatus.Delete)).ToList();
                                        break;
                                    case ProjectStatus.Deny:
                                        listProject = listProject.Where(x => x.Status.Equals((int)ProjectStatus.Deny)).ToList();
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case AdminFilter.PriceMin:
                                try
                                {
                                    listProject = listProject.Where(x => x.Price >= decimal.Parse(fil.Value)).ToList();
                                }
                                catch (Exception)
                                {
                                }
                                break;
                            case AdminFilter.PriceMax:
                                try
                                {
                                    listProject = listProject.Where(x => x.Price <= decimal.Parse(fil.Value)).ToList();
                                }
                                catch (Exception)
                                {
                                }
                                break;
                            case AdminFilter.CreateDate:
                                listProject = listProject.Where(x => x.CreateDate.Date.Equals(DateTime.Parse(fil.Value).Date)).ToList();
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
                        case AdminSort.Name:
                            if (sortBy.Value)
                            {
                                listProject = listProject.OrderBy(x => x.Name).ToList();
                            }
                            else
                            {
                                listProject = listProject.OrderByDescending(x => x.Name).ToList();
                            }
                            break;
                        case AdminSort.Poster:
                            if (sortBy.Value)
                            {
                                listProject = listProject.OrderBy(x => x.Poster.FirstName).ToList();
                            }
                            else
                            {
                                listProject = listProject.OrderByDescending(x => x.Poster.FirstName).ToList();
                            }
                            break;
                        case AdminSort.Price:
                            if (sortBy.Value)
                            {
                                listProject = listProject.OrderBy(x => x.Price).ToList();
                            }
                            else
                            {
                                listProject = listProject.OrderByDescending(x => x.Price).ToList();
                            }
                            break;
                        case AdminSort.CreateDate:
                            if (sortBy.Value)
                            {
                                listProject = listProject.OrderBy(x => x.CreateDate).ToList();
                            }
                            else
                            {
                                listProject = listProject.OrderByDescending(x => x.CreateDate).ToList();
                            }
                            break;
                        case AdminSort.Status:
                            if (sortBy.Value)
                            {
                                listProject = listProject.OrderBy(x => x.Status).ToList();
                            }
                            else
                            {
                                listProject = listProject.OrderByDescending(x => x.Status).ToList();
                            }
                            break;
                        default:
                            break;
                    }
                }
                if (!sort.ContainsKey(AdminSort.CreateDate))
                {
                    listProject = listProject.OrderByDescending(x => x.CreateDate).ToList();
                }
                var data = listProject.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();
                return Response.OK(data, total);
            }
            return null;
        }

        public async Task<ProjectResponse> GetProjectById(Guid projectId)
        {
            var projects = _projectRepository.GetMany(x => x.Id.Equals(projectId));
            if (projects.Any())
            {
                var project = await projects
                    .Include(x => x.ArtistProjectFiles)
                    .Include(x => x.CustomerProjectFiles)
                    .Include(x => x.ArtistProjects).ThenInclude(x => x.Artist)
                    .Include(x => x.ProjectCountries).ThenInclude(x => x.Country)
                    .Include(x => x.ProjectGenders).ThenInclude(x => x.Gender)
                    .Include(x => x.ProjectUsagePurposes).ThenInclude(x => x.UsagePurpose)
                    .Include(x => x.ProjectVoiceStyles).ThenInclude(x => x.VoiceStyle).FirstOrDefaultAsync();
                var projectResponse = _mapper.Map<ProjectResponse>(project);
                project.ArtistProjects.ToList().ForEach(x =>
                {
                    var artist = x.Artist;
                    artist.GenderNavigation = _genderRepository.FirstOrDefault(x => x.Id.Equals(artist.Gender));
                    artist.ArtistVoiceStyles = _artistVoiceStyleRepository.GetMany(x => x.ArtistId.Equals(artist.Id)).Include(x => x.VoiceStyle).ToList();
                    artist.ArtistVoiceDemos = _artistVoiceDemoRepository.GetMany(x => x.ArtistId.Equals(artist.Id)).Include(x => x.VoiceDemo).ToList();
                    projectResponse.ArtistProject.Add(_mapper.Map<ArtistProjectResponse>(x, opt =>
                    opt.AfterMap((desc, src) => src.QuickArtistResponse = _mapper.Map<QuickArtistResponse>(artist))));
                });
                project.ArtistProjectFiles.ToList().ForEach(x => projectResponse.ArtistProjectsFiles
                .Add(_mapper.Map<ProjectFile>(x)));
                project.CustomerProjectFiles.ToList().ForEach(x => projectResponse.CustomerProjectsFiles
                .Add(_mapper.Map<ProjectFile>(x)));
                projectResponse.Poster = _mapper.Map<CustomerResponse>(_customerRepository.FirstOrDefault(x => x.Id.Equals(project.Poster)));
                return projectResponse;
            }
            return null;
        }

        public async Task<bool> ChangeFileStatus(ModifyFileRequest file)
        {
            var artistFile = _artistProjectFileRepository.GetMany(x => x.Id.Equals(file.Id)).FirstOrDefault();
            if (artistFile != null)
            {
                artistFile.Status = (int) Enum.Parse(typeof(FileStatus) ,file.Status);
                artistFile.Comment = file.Comment;
                try
                {
                    _artistProjectFileRepository.Update(artistFile);
                    await _unitOfWork.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public async Task<QuickProjectResponse> UpdateProject(ProjectRequest project, Guid projectId, Guid userId)
        {
            var ownProject = _projectRepository.GetMany(i => i.Id.Equals(projectId));
            if (ownProject.Any() &&
                ownProject.FirstOrDefault().Status != ((int)ProjectStatus.Delete) ||
                ownProject.FirstOrDefault().Status != ((int)ProjectStatus.Done) &&
                ownProject.FirstOrDefault().Poster.Equals(userId))
            {
                var p = ownProject.FirstOrDefault();
                p.MinAge = project.MinAge;
                p.MaxAge = project.MaxAge;
                p.Price = project.Price;
                p.Description = project.Description;
                p.ResponseDeadline = project.ResponseDeadline;
                p.ProjectDeadline = project.ProjectDeadline;
                p.UpdateDate = DateTime.UtcNow;
                p.Status = (int)ProjectStatus.Waiting;
                UpdatePreProject(project, projectId);

                try
                {
                    _projectRepository.Update(p);
                    await _unitOfWork.SaveChanges();
                    return _mapper.Map<QuickProjectResponse>(p);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> ChangeStatusProject(Guid projectId, Guid userId, ProjectStatus status)
        {
            var isSucess = false;
            var ownProject = await _projectRepository.GetMany(i => i.Id.Equals(projectId) && i.Poster.Equals(userId))
                .Include(x => x.ArtistProjects).ToListAsync();
            if (ownProject.Any())
            {
                switch (status)
                {
                    case ProjectStatus.Process:
                        if (ownProject.FirstOrDefault().Status.Equals((int)ProjectStatus.Pending))
                        {
                            var isArtist = ownProject.FirstOrDefault().ArtistProjects.Any();
                            if (isArtist)
                            {
                                ownProject.FirstOrDefault().Status = (int)ProjectStatus.Process;
                                isSucess = true;
                            }
                        }
                        break;
                    case ProjectStatus.Done:
                        if (ownProject.FirstOrDefault().Status.Equals((int)ProjectStatus.Process))
                        {
                            var incompleteArtist = ownProject.FirstOrDefault().ArtistProjects?.Where(x => x.Status.Equals((int)InviteStatus.Accept));
                            if (!incompleteArtist.Any())
                            {
                                ownProject.FirstOrDefault().Status = (int)ProjectStatus.Done;
                                isSucess = true;
                            }
                        }
                        break;
                    case ProjectStatus.Delete:
                        if (ownProject.FirstOrDefault().Status.Equals((int)ProjectStatus.Pending) || ownProject.FirstOrDefault().Status.Equals((int)ProjectStatus.Waiting))
                        {
                            ownProject.FirstOrDefault().Status = (int)ProjectStatus.Delete;
                            ownProject.FirstOrDefault().ArtistProjects?.ToList().ForEach(x =>
                            {
                                x.Status = (int)InviteStatus.Deny;
                                x.CanceledDate = DateTime.UtcNow;
                            });
                            isSucess = true;
                        }
                        break;
                    default:
                        isSucess = false;
                        break;
                }

                ownProject.FirstOrDefault().UpdateDate = DateTime.UtcNow;
                try
                {
                    if (isSucess)
                    {
                        _projectRepository.Update(ownProject.FirstOrDefault());
                        await _unitOfWork.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    isSucess = false;
                }
            }
            else
            {
                isSucess = false;
            }
            return isSucess;
        }

        public async Task<Response> GetArtistInProject(Guid id, string status, Pagination pagination)
        {
            var artProject = await _artistProjectRepository.GetMany(x => x.ProjectId.Equals(id))
                .Include(x => x.Artist)
                .ToListAsync();
            if (!string.IsNullOrEmpty(status))
            {
                try
                {
                    var stt = Enum.Parse(typeof(InviteStatus), status);
                    artProject = artProject.Where(x => x.Status.Equals((int)stt)).ToList();
                }
                catch (Exception)
                {
                    return null;
                }
            }
            if (artProject.Any())
            {
                var total = artProject.Count();
                var data = artProject.OrderByDescending(x => x.ArtistId)
                    .Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();
                return Response.OK(data.Select(x => new QuickArtistProjectResponse
                {
                    ArtistId = x.ArtistId,
                    ArtistFirstName = x.Artist.FirstName,
                    ArtistLastName = x.Artist.LastName,
                    ArtistAvatar = x.Artist.Avatar,
                    CanceledDate = x.CanceledDate,
                    Comment = x.Comment,
                    FinishedDate = x.FinishedDate,
                    InvitedDate = x.InvitedDate,
                    JoinedDate = x.JoinedDate,
                    RequestedDate = x.RequestedDate,
                    Status = ((InviteStatus)x.Status).ToString(),
                    Rate = x.Rate
                }).ToList(), total);
            }
            return null;
        }

        public async Task<object> GetProjectByArtist(Guid id, Pagination pagination, string status)
        {
            var artProject = await _artistProjectRepository.GetMany(x => x.ArtistId.Equals(id))
                .Include(x => x.Artist)
                .Include(x => x.Project)
                .ToListAsync();
            if (artProject.Any())
            {
                if (!string.IsNullOrEmpty(status))
                {
                    var stt = Enum.Parse(typeof(InviteStatus), status);
                    artProject = artProject.Where(x => x.Status.Equals((int)stt)).ToList();
                }
                var listProjectArtist = new List<QuickProjectArtistResponse>();
                artProject.ToList().ForEach(x =>
                {
                    var customer = _customerRepository.FirstOrDefault(y => y.Id.Equals(x.Project.Poster));
                    listProjectArtist.Add(new QuickProjectArtistResponse
                    {
                        ProjectId = x.ProjectId,
                        ProjectName = x.Project.Name,
                        CustomerId = x.Project.Poster,
                        CustomerAvatar = customer.Avatar,
                        CustomerFirstName = customer.FirstName,
                        CustomerLastName = customer.LastName,
                        CanceledDate = x.CanceledDate,
                        FinishedDate = x.FinishedDate,
                        InvitedDate = x.InvitedDate,
                        JoinedDate = x.JoinedDate,
                        RequestedDate = x.RequestedDate,
                        Status = ((InviteStatus)x.Status).ToString()
                    });
                });
                var total = listProjectArtist.Count();
                var data = listProjectArtist.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();
                return Response.OK(data, total);
            }
            return null;
        }

        public async Task<bool> AdminCheckProject(Guid projectId, bool isAccept)
        {
            var project = _projectRepository.FirstOrDefault(x => x.Id.Equals(projectId));
            if (project != null)
            {
                project.Status = isAccept ? (int)ProjectStatus.Pending : (int)ProjectStatus.Deny;
                project.UpdateDate = DateTime.UtcNow;
                _projectRepository.Update(project);
                await _unitOfWork.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> InviteArtist(Guid projectId, Guid userId, Guid artistId)
        {

            if (_artistProjectRepository.FirstOrDefault(x => x.ProjectId.Equals(projectId) && x.ArtistId.Equals(artistId)) is null)
            {
                try
                {
                    _artistProjectRepository.Add(new ArtistProject()
                    {
                        ArtistId = artistId,
                        ProjectId = projectId,
                        Status = (int)InviteStatus.InvitePending,
                        InvitedDate = DateTime.UtcNow
                    });
                    await _unitOfWork.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return true;
            }

        }


        public async Task<bool> ArtistRequest(Guid projectId, Guid artistId)
        {
            if (_artistProjectRepository.FirstOrDefault(x => x.ProjectId.Equals(projectId) && x.ArtistId.Equals(artistId)) is null)
            {
                try
                {
                    _artistProjectRepository.Add(new ArtistProject()
                    {
                        ArtistId = artistId,
                        ProjectId = projectId,
                        Status = (int)InviteStatus.ResponsePending,
                        RequestedDate = DateTime.UtcNow
                    });
                    await _unitOfWork.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> ResponseArtist(Guid projectId, Guid userId, Guid artistId, bool isAccept)
        {
            var CustomerProject = _projectRepository.GetMany(i => i.Id.Equals(projectId) && i.Poster.Equals(userId));
            if (CustomerProject.Any())
            {
                if (_artistProjectRepository.GetMany(x => x.ProjectId.Equals(projectId) && x.ArtistId.Equals(artistId) &&
                x.Status.Equals((int)InviteStatus.ResponsePending)).Any())
                {
                    try
                    {
                        _artistProjectRepository.Update(new ArtistProject()
                        {
                            ArtistId = artistId,
                            ProjectId = projectId,
                            Status = isAccept ? (int)InviteStatus.Accept : (int)InviteStatus.Deny,
                            JoinedDate = isAccept ? DateTime.UtcNow : null,
                            CanceledDate = isAccept ? null : DateTime.UtcNow
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
            return false;
        }

        public async Task<bool> ArtistReply(Guid projectId, Guid artistId, bool isAccept)
        {
            if (_artistProjectRepository.GetMany(x => x.ProjectId.Equals(projectId) &&
            x.ArtistId.Equals(artistId) && x.Status.Equals((int)InviteStatus.InvitePending)).Any())
            {
                try
                {
                    _artistProjectRepository.Update(new ArtistProject()
                    {
                        ArtistId = artistId,
                        ProjectId = projectId,
                        Status = isAccept ? (int)InviteStatus.Accept : (int)InviteStatus.Deny,
                        JoinedDate = isAccept ? DateTime.UtcNow : null,
                        CanceledDate = isAccept ? null : DateTime.UtcNow
                    });
                    await _unitOfWork.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public async Task<bool?> MakeDoneArtist(Guid projectId, Guid userId, Guid artistId)
        {
            if (_artistProjectRepository.GetMany(x => x.ProjectId.Equals(projectId) && x.ArtistId.Equals(artistId) && x.Status.Equals((int)InviteStatus.Accept)).Any())
            {
                try
                {
                    _artistProjectRepository.Update(new ArtistProject()
                    {
                        ArtistId = artistId,
                        ProjectId = projectId,
                        Status = (int)InviteStatus.Done,
                        FinishedDate = DateTime.UtcNow
                    });
                    await _unitOfWork.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return null;
        }

        public async Task<object> GetArtistFilesInProject(Guid artistId, Guid projecId)
        {
            var artistProjectFile = _artistProjectFileRepository.GetMany(x => x.CreateBy.Equals(artistId) && x.ProjectId.Equals(projecId));
            if (artistProjectFile.Any())
            {
                var files = new List<FileResponse>();
                var artistFile = new ArtistFileResponse
                {
                    ArtistProjectFile = artistProjectFile.Select(x => new FileResponse
                    {
                        Id = x.Id,
                        Name = x.Name,
                        ProjectId = x.ProjectId,
                        Url = x.Url,
                        MediaType = x.MediaType.ToString(),
                        CreateBy = x.CreateBy,
                        CreateDate = x.CreateDate
                    }).ToList()
                };
                return Response.OK(artistFile);
            }
            return null;
        }

        public async Task<object> GetCustomerFilesInProject(Guid customerId, Guid projecId)
        {
            var artistProjectFile = _artistProjectFileRepository.GetMany(x => x.CreateBy.Equals(customerId) && x.ProjectId.Equals(projecId));
            if (artistProjectFile.Any())
            {
                var files = new List<FileResponse>();
                var artistFile = new CustomerFileResponse
                {
                    CustomerProjectFile = artistProjectFile.Select(x => new FileResponse
                    {
                        Id = x.Id,
                        Name = x.Name,
                        ProjectId = x.ProjectId,
                        Url = x.Url,
                        MediaType = x.MediaType.ToString(),
                        CreateBy = x.CreateBy,
                        CreateDate = x.CreateDate
                    }).ToList()
                };
                return Response.OK(artistFile);
            }
            return null;
        }

        public async Task<bool> AddFile(Guid projectId, Guid userId, string role, ProjectFile file)
        {
            try
            {
                if (role.Equals(Role.Artist.ToString()))
                {
                    _artistProjectFileRepository.Add(_mapper.Map<ArtistProjectFile>(file, opt => opt.AfterMap((src, dest) =>
                    {
                        dest.Id = Guid.NewGuid();
                        dest.CreateBy = userId;
                        dest.ProjectId = projectId;
                        dest.CreateDate = DateTime.UtcNow;
                        dest.Status = 0;
                    })));
                }
                else if (role.Equals(Role.Customer.ToString()))
                {
                    _customerProjectFileRepository.Add(_mapper.Map<CustomerProjectFile>(file, opt => opt.AfterMap((src, dest) =>
                    {
                        dest.Id = Guid.NewGuid();
                        dest.CreateBy = userId;
                        dest.ProjectId = projectId;
                        dest.CreateDate = DateTime.UtcNow;
                        dest.Status = 0;
                    })));
                }
                await _unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Must be use ID to delete file instead of Url
        public async Task<bool> DeleteFile(Guid projectId, Guid userId, string role, string url)
        {
            bool result = false;
            try
            {
                if (role.Equals(Role.Artist.ToString()))
                {
                    var file = _artistProjectFileRepository.FirstOrDefault(x => x.ProjectId.Equals(projectId) && x.CreateBy.Equals(userId) && x.Url.Equals(url));
                    if (file != null)
                    {
                        _artistProjectFileRepository.Remove(file);
                        result = true;
                    }
                }
                else if (role.Equals(Role.Customer.ToString()))
                {
                    var file = _customerProjectFileRepository.FirstOrDefault(x => x.ProjectId.Equals(projectId) && x.CreateBy.Equals(userId) && x.Url.Equals(url));
                    if (file != null)
                    {
                        _customerProjectFileRepository.Remove(file);
                        result = true;
                    }
                }
                if (result)
                {
                    await _unitOfWork.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        public async Task<bool> RatingArtist(Guid projectId, Guid customerId, Guid artistId, RatingRequest rate)
        {
            var artistProjects = _artistProjectRepository.GetMany(x => x.ProjectId.Equals(projectId) && x.ArtistId.Equals(artistId) && x.Status.Equals((int)InviteStatus.Done))
                .Include(x => x.Project);
            if (artistProjects.Where(x => x.Project.Poster.Equals(customerId)).Any())
            {
                var a = artistProjects.FirstOrDefault();
                a.Rate = rate.Rate;
                a.Comment = rate.Comment;
                a.ReviewDate = DateTime.UtcNow;
                _artistProjectRepository.Update(a);
                await _unitOfWork.SaveChanges();
                var artist = await _artistRepository.GetMany(x => x.Id.Equals(artistId)).Include(x => x.ArtistProjects).FirstOrDefaultAsync();
                var totalRate = artist.ArtistProjects.Select(x => x.Rate).ToList().Average();
                artist.Rate = totalRate;
                _artistRepository.Update(artist);
                await _unitOfWork.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void AddPreProject(ProjectRequest request, Guid userId, Guid projectId)
        {
            //add customer project file
            request.CustomerProjectsFile
                .ForEach(x => _customerProjectFileRepository
                .Add(_mapper.Map<CustomerProjectFile>(x, opt =>
                    opt.AfterMap((src, dest) =>
                    {
                        dest.CreateBy = userId;
                        dest.ProjectId = projectId;
                        dest.Id = Guid.NewGuid();
                        dest.CreateDate = DateTime.UtcNow;
                    })
                )));

            //add project country
            request.ProjectCountries
                .ForEach(x =>
                {
                    if (_countryRepository.GetMany(t => t.Name.Equals(x)).Any())
                    {
                        _projectCountryRepository.Add(new ProjectCountry
                        {
                            CountryId = _countryRepository.FirstOrDefault(t => t.Name.Equals(x)).Id,
                            ProjectId = projectId
                        });
                    }
                });

            //add project gender
            request.ProjectGenders
                .ForEach(x =>
                {
                    if (_genderRepository.GetMany(t => t.Name.Equals(x)).Any())
                    {
                        _projectGenderRepository.Add(new ProjectGender
                        {
                            GenderId = _genderRepository.FirstOrDefault(t => t.Name.Equals(x)).Id,
                            ProjectId = projectId
                        });
                    }
                });

            //add project purpose
            request.ProjectUsagePurposes
                .ForEach(x =>
                {
                    if (_usagePurposeRepository.GetMany(t => t.Name.Equals(x)).Any())
                    {
                        _projectUsagePurposeRepository.Add(new ProjectUsagePurpose
                        {
                            UsagePurposeId = _usagePurposeRepository.FirstOrDefault(t => t.Name.Equals(x)).Id,
                            ProjectId = projectId
                        });
                    }
                });

            //add project voice style
            request.ProjectVoiceStyles
                .ForEach(x =>
                {
                    if (_voiceStyleRepository.GetMany(t => t.Name.Equals(x)).Any())
                    {
                        _projectVoiceStyleRepository.Add(new ProjectVoiceStyle
                        {
                            VoiceStyleId = _voiceStyleRepository.FirstOrDefault(t => t.Name.Equals(x)).Id,
                            ProjectId = projectId
                        });
                    }
                });
        }

        private void UpdatePreProject(ProjectRequest project, Guid projectId)
        {
            if (project.ProjectCountries != null)
            {
                //detele country
                _projectCountryRepository.GetMany(x => x.ProjectId.Equals(projectId)).ToList()
                    .ForEach(t =>
                    {
                        if (!project.ProjectCountries.Contains(_countryRepository.FirstOrDefault(x => x.Id.Equals(t.CountryId)).Name))
                        {
                            _projectCountryRepository.Remove(t);
                        }
                    });
                //add more contries
                project.ProjectCountries
                    .ForEach(t =>
                    {
                        if (_countryRepository.GetMany(x => x.Name.Equals(t)).Any())
                        {
                            if (!_projectCountryRepository.GetMany(x => x.ProjectId.Equals(projectId))
                        .Select(y => y.CountryId).ToList().Contains(_countryRepository.FirstOrDefault(z => z.Name.Equals(t)).Id))
                            {
                                _projectCountryRepository.Add(new ProjectCountry
                                {
                                    CountryId = _countryRepository.FirstOrDefault(a => a.Name.Equals(t)).Id,
                                    ProjectId = projectId
                                });
                            }
                        }
                    });
            }

            if (project.ProjectGenders != null)
            {
                //detele gender
                _projectGenderRepository.GetMany(x => x.ProjectId.Equals(projectId)).ToList()
                    .ForEach(t =>
                    {
                        if (!project.ProjectGenders.Contains(_genderRepository.FirstOrDefault(x => x.Id.Equals(t.GenderId)).Name))
                        {
                            _projectGenderRepository.Remove(t);
                        }
                    });
                //add more gender
                project.ProjectGenders
                    .ForEach(t =>
                    {
                        if (_genderRepository.GetMany(x => x.Name.Equals(t)).Any())
                        {
                            if (!_projectGenderRepository.GetMany(x => x.ProjectId.Equals(projectId))
                        .Select(y => y.GenderId).ToList().Contains(_genderRepository.FirstOrDefault(z => z.Name.Equals(t)).Id))
                            {
                                _projectGenderRepository.Add(new ProjectGender
                                {
                                    GenderId = _genderRepository.FirstOrDefault(a => a.Name.Equals(t)).Id,
                                    ProjectId = projectId
                                });
                            }
                        }
                    });
            }

            if (project.ProjectUsagePurposes != null)
            {
                //detele usage purpose
                _projectUsagePurposeRepository.GetMany(x => x.ProjectId.Equals(projectId)).ToList()
                    .ForEach(t =>
                    {
                        if (!project.ProjectUsagePurposes.Contains(_usagePurposeRepository.FirstOrDefault(x => x.Id.Equals(t.UsagePurposeId)).Name))
                        {
                            _projectUsagePurposeRepository.Remove(t);
                        }
                    });
                //add more usage purpose
                project.ProjectUsagePurposes
                    .ForEach(t =>
                    {
                        if (_usagePurposeRepository.GetMany(x => x.Name.Equals(t)).Any())
                        {
                            if (!_projectUsagePurposeRepository.GetMany(x => x.ProjectId.Equals(projectId))
                        .Select(y => y.UsagePurposeId).ToList().Contains(_usagePurposeRepository.FirstOrDefault(z => z.Name.Equals(t)).Id))
                            {
                                _projectUsagePurposeRepository.Add(new ProjectUsagePurpose
                                {
                                    UsagePurposeId = _usagePurposeRepository.FirstOrDefault(a => a.Name.Equals(t)).Id,
                                    ProjectId = projectId
                                });
                            }
                        }
                    });
            }

            if (project.ProjectVoiceStyles != null)
            {
                //detele voice style
                _projectVoiceStyleRepository.GetMany(x => x.ProjectId.Equals(projectId)).ToList()
                    .ForEach(t =>
                    {
                        if (!project.ProjectVoiceStyles.Contains(_voiceStyleRepository.FirstOrDefault(x => x.Id.Equals(t.VoiceStyleId)).Name))
                        {
                            _projectVoiceStyleRepository.Remove(t);
                        }
                    });
                //add more voice style
                project.ProjectVoiceStyles
                    .ForEach(t =>
                    {
                        if (_voiceStyleRepository.GetMany(x => x.Name.Equals(t)).Any())
                        {
                            if (!_projectVoiceStyleRepository.GetMany(x => x.ProjectId.Equals(projectId))
                        .Select(y => y.VoiceStyleId).ToList().Contains(_voiceStyleRepository.FirstOrDefault(z => z.Name.Equals(t)).Id))
                            {
                                _projectVoiceStyleRepository.Add(new ProjectVoiceStyle
                                {
                                    VoiceStyleId = _voiceStyleRepository.FirstOrDefault(a => a.Name.Equals(t)).Id,
                                    ProjectId = projectId
                                });
                            }
                        }
                    });
            }
        }

        public async Task<string> GetProjectName(Guid id)
        {
            return await _projectRepository.GetMany(x => x.Id.Equals(id)).Select(x => x.Name).FirstOrDefaultAsync();
        }

        public async Task<Guid?> GetProjectPoster(Guid id)
        {
            return await _projectRepository.GetMany(x => x.Id.Equals(id)).Select(x => x.Poster).FirstOrDefaultAsync();
        }
    }
}
