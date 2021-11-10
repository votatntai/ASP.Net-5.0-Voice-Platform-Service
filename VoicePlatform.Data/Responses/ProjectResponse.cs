using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoicePlatform.Data.Requests;

namespace VoicePlatform.Data.Responses
{
    public class ProjectResponse
    {
        public Guid Id { get; set; }
        public CustomerResponse Poster { get; set; }
        public string Name { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime ResponseDeadline { get; set; }
        public DateTime ProjectDeadline { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public List<ProjectFile> ArtistProjectsFiles { get; set; }
        public List<ProjectFile> CustomerProjectsFiles { get; set; }
        public List<ArtistProjectResponse> ArtistProject { get; set; }
        public List<string> ProjectCountries { get; set; }
        public List<string> ProjectGenders { get; set; }
        public List<string> ProjectUsagePurposes { get; set; }
        public List<string> ProjectVoiceStyles { get; set; }
    }
}
