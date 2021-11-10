using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoicePlatform.Data.Entities;
using VoicePlatform.Data.Requests;
using VoicePlatform.Data.Responses;
using VoicePlatform.Utility.Enums;

namespace VoicePlatform.Service.Mapper
{
    public class AutoMappingProfile
    {
        public class AutoMapperProfile : Profile
        {
            // Map from - to
            public AutoMapperProfile()
            {
                // return user 
                CreateMap<Customer, CustomerResponse>();

                //for create project
                CreateMap<ProjectRequest, Project>()
                    .ForMember(dest => dest.ArtistProjectFiles, opt => opt.Ignore())
                    .ForMember(dest => dest.CustomerProjectFiles, opt => opt.Ignore())
                    .ForMember(dest => dest.ArtistProjects, opt => opt.Ignore())
                    .ForMember(dest => dest.ProjectCountries, opt => opt.Ignore())
                    .ForMember(dest => dest.ProjectGenders, opt => opt.Ignore())
                    .ForMember(dest => dest.ProjectUsagePurposes, opt => opt.Ignore())
                    .ForMember(dest => dest.ProjectVoiceStyles, opt => opt.Ignore());

                CreateMap<Project, AdminProjectResponse>()
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => ((ProjectStatus)src.Status).ToString()))
                    .ForMember(dest => dest.Poster, opt => opt.Ignore());

                //create customer
                CreateMap<CustomerRequest, Customer>()
                    .ForMember(dest => dest.Gender, opt => opt.Ignore());

                //response customer
                CreateMap<Customer, CustomerResponse>()
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => ((UserStatus)src.Status).ToString()))
                    .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.GenderNavigation.Name));

                //response artist
                CreateMap<Artist, QuickArtistResponse>()
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => ((UserStatus)src.Status).ToString()))
                    .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.GenderNavigation.Name))
                    .ForMember(dest => dest.VoiceStyles, opt => opt.MapFrom(src => src.ArtistVoiceStyles.Select(x => x.VoiceStyle.Name).ToList()))
                    .ForMember(dest => dest.Countries, opt => opt.MapFrom(src => src.ArtistCountries.Select(x => x.Country.Name).ToList()))
                    .ForMember(dest => dest.VoiceDemos, opt => opt.MapFrom(src => src.ArtistVoiceDemos.Select(x => x.VoiceDemo.Url).ToList()));

                //response quick project 
                CreateMap<Project, QuickProjectResponse>()
                    .ForMember(dest => dest.Poster, opt => opt.Ignore())
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => ((ProjectStatus)src.Status).ToString()))
                    .ForMember(dest => dest.Genders, opt => opt.MapFrom(src => src.ProjectGenders.Select(x => x.Gender.Name).ToList()))
                    .ForMember(dest => dest.VoiceStyles, opt => opt.MapFrom(src => src.ProjectVoiceStyles.Select(x => x.VoiceStyle.Name).ToList()))
                    .ForMember(dest => dest.Countries, opt => opt.MapFrom(src => src.ProjectCountries.Select(x => x.Country.Name).ToList()))
                    .ForMember(dest => dest.UsagePurposes, opt => opt.MapFrom(src => src.ProjectUsagePurposes.Select(x => x.UsagePurpose.Name).ToList()));
                
                CreateMap<Project, AdminQuickProjectResponse>()
                    .ForMember(dest => dest.Poster, opt => opt.Ignore())
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => ((ProjectStatus)src.Status).ToString()))
                    .ForMember(dest => dest.Genders, opt => opt.MapFrom(src => src.ProjectGenders.Select(x => x.Gender.Name).ToList()))
                    .ForMember(dest => dest.VoiceStyles, opt => opt.MapFrom(src => src.ProjectVoiceStyles.Select(x => x.VoiceStyle.Name).ToList()))
                    .ForMember(dest => dest.Countries, opt => opt.MapFrom(src => src.ProjectCountries.Select(x => x.Country.Name).ToList()))
                    .ForMember(dest => dest.UsagePurposes, opt => opt.MapFrom(src => src.ProjectUsagePurposes.Select(x => x.UsagePurpose.Name).ToList()))
                    .ForMember(dest => dest.ArtistProjects, opt => opt.Ignore());

                //response full project
                CreateMap<Project, ProjectResponse>()
                    .ForMember(dest => dest.Poster, opt => opt.Ignore())
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => ((ProjectStatus)src.Status).ToString()))
                    .ForMember(dest => dest.ArtistProjectsFiles, opt => opt.MapFrom(src => new List<ProjectFile>()))
                    .ForMember(dest => dest.CustomerProjectsFiles, opt => opt.MapFrom(src => new List<ProjectFile>()))
                    .ForMember(dest => dest.ArtistProject, opt => opt.MapFrom(src => new List<ArtistProjectResponse>()))
                    .ForMember(dest => dest.ProjectCountries, opt => opt.MapFrom(src => src.ProjectCountries.Select(x => x.Country.Name).ToList()))
                    .ForMember(dest => dest.ProjectGenders, opt => opt.MapFrom(src => src.ProjectGenders.Select(x => x.Gender.Name).ToList()))
                    .ForMember(dest => dest.ProjectUsagePurposes, opt => opt.MapFrom(src => src.ProjectUsagePurposes.Select(x => x.UsagePurpose.Name).ToList()))
                    .ForMember(dest => dest.ProjectVoiceStyles, opt => opt.MapFrom(src => src.ProjectVoiceStyles.Select(x => x.VoiceStyle.Name).ToList()))
                    .ForMember(dest => dest.UpdateDate, opt => opt.MapFrom(src => src.UpdateDate));

                //Project file
                CreateMap<ArtistProjectFile, ProjectFile>()
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => ((FileStatus)src.Status).ToString()));
                CreateMap<CustomerProjectFile, ProjectFile>();

                //project file
                CreateMap<ProjectFile, CustomerProjectFile>();
                CreateMap<ProjectFile, ArtistProjectFile>();

                //response gender, country, usagePurpose, voiceStyle
                CreateMap<Gender, MiniReqRes>();
                CreateMap<Country, MiniReqRes>();
                CreateMap<UsagePurpose, MiniReqRes>();
                CreateMap<VoiceStyle, MiniReqRes>();

                //Rating response
                CreateMap<ArtistProject, RatingResponse>();
                CreateMap<Customer, RatingResponse>();

                //Artist Project response
                CreateMap<ArtistProject, ArtistProjectResponse>()
                    .ForMember(dest => dest.QuickArtistResponse, opt => opt.Ignore())
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => ((InviteStatus)src.Status).ToString()));
            }
        }
    }
}
