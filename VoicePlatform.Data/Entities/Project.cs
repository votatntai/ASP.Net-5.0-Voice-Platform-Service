using System;
using System.Collections.Generic;

#nullable disable

namespace VoicePlatform.Data.Entities
{
    public partial class Project
    {
        public Project()
        {
            ArtistProjectFiles = new HashSet<ArtistProjectFile>();
            ArtistProjects = new HashSet<ArtistProject>();
            CustomerProjectFiles = new HashSet<CustomerProjectFile>();
            ProjectCountries = new HashSet<ProjectCountry>();
            ProjectGenders = new HashSet<ProjectGender>();
            ProjectUsagePurposes = new HashSet<ProjectUsagePurpose>();
            ProjectVoiceStyles = new HashSet<ProjectVoiceStyle>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid Poster { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime ResponseDeadline { get; set; }
        public DateTime ProjectDeadline { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<ArtistProjectFile> ArtistProjectFiles { get; set; }
        public virtual ICollection<ArtistProject> ArtistProjects { get; set; }
        public virtual ICollection<CustomerProjectFile> CustomerProjectFiles { get; set; }
        public virtual ICollection<ProjectCountry> ProjectCountries { get; set; }
        public virtual ICollection<ProjectGender> ProjectGenders { get; set; }
        public virtual ICollection<ProjectUsagePurpose> ProjectUsagePurposes { get; set; }
        public virtual ICollection<ProjectVoiceStyle> ProjectVoiceStyles { get; set; }
    }
}
