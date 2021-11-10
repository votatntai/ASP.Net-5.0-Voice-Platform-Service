using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoicePlatform.Data.Application;
using VoicePlatform.Data.Entities;
using VoicePlatform.Data.Requests;
using VoicePlatform.Data.Responses;
using VoicePlatform.Utility.Enums;

namespace VoicePlatform.Service.Interfaces
{
    public interface IProjectService
    {
        Task<QuickProjectResponse> CreateProject(Guid userId, ProjectRequest project);

        Task<object> GetOwnProject(Guid userId, Pagination pagination, bool isCustomer,
            List<string> filter, bool? isAsc, string searchString, bool? isProcess);

        Task<object> GetUserProject(Guid userId, Pagination pagination, bool isCustomer,
            List<string> filter, bool? isAsc, string searchString);

        Task<object> GetProject(Pagination pagination, 
            Dictionary<AdminFilter, string> filter, Dictionary<AdminSort, bool> sort, string searchString);

        Task<object> GetPendingProject(Pagination pagination, Dictionary<CustomerFilter, string> filter, bool? isAsc, string searchString);

        Task<object> GetProjectByArtist(Guid id, Pagination pagination, string status);

        Task<Response> GetArtistInProject(Guid id, string status, Pagination pagination);

        Task<QuickProjectResponse> UpdateProject(ProjectRequest project, Guid idProject, Guid userId);

        Task<ProjectResponse> GetProjectById(Guid projectId);

        Task<bool> ChangeFileStatus(ModifyFileRequest file);

        Task<bool> ChangeStatusProject(Guid idProject, Guid userId, ProjectStatus status);

        Task<bool> AdminCheckProject(Guid projectId, bool isAccept);

        Task<bool> InviteArtist(Guid projectId, Guid userId, Guid artistId);

        Task<bool> ArtistRequest(Guid projectId, Guid artistId);

        Task<bool> ResponseArtist(Guid projectId, Guid userId, Guid artistId, bool isAccept);

        Task<bool> ArtistReply(Guid projectId, Guid artistId, bool isAccept);

        Task<bool?> MakeDoneArtist(Guid projectId, Guid userId, Guid artistId);

        Task<bool> AddFile(Guid projectId, Guid userId, string role, ProjectFile file);

        Task<bool> DeleteFile(Guid projectId, Guid userId, string role, string url);

        Task<bool> RatingArtist(Guid projectId, Guid customerId, Guid artistId, RatingRequest rate);

        Task<Response> GetWaitingProject(Pagination pagination, Dictionary<AdminFilter, string> filter, Dictionary<AdminSort, bool> sort, string searchString);

        Task<string> GetProjectName(Guid id);
        Task<Guid?> GetProjectPoster(Guid id);
    }
}
