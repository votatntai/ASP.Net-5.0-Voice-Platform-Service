using System;
using System.Collections.Generic;

#nullable disable

namespace VoicePlatform.Data.Entities
{
    public partial class Artist
    {
        public Artist()
        {
            ArtistCountries = new HashSet<ArtistCountry>();
            ArtistProjectFiles = new HashSet<ArtistProjectFile>();
            ArtistProjects = new HashSet<ArtistProject>();
            ArtistVoiceDemos = new HashSet<ArtistVoiceDemo>();
            ArtistVoiceStyles = new HashSet<ArtistVoiceStyle>();
        }

        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Role { get; set; }
        public Guid Gender { get; set; }
        public string Avatar { get; set; }
        public string Bio { get; set; }
        public double Price { get; set; }
        public bool Studio { get; set; }
        public double? Rate { get; set; }
        public int Status { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateBy { get; set; }

        public virtual Gender GenderNavigation { get; set; }
        public virtual User UpdateByNavigation { get; set; }
        public virtual ICollection<ArtistCountry> ArtistCountries { get; set; }
        public virtual ICollection<ArtistProjectFile> ArtistProjectFiles { get; set; }
        public virtual ICollection<ArtistProject> ArtistProjects { get; set; }
        public virtual ICollection<ArtistVoiceDemo> ArtistVoiceDemos { get; set; }
        public virtual ICollection<ArtistVoiceStyle> ArtistVoiceStyles { get; set; }
        public virtual ICollection<ArtistToken> ArtistTokens { get; set; }
    }
}
