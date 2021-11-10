using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoicePlatform.Data.Responses
{
    public class AdminQuickProjectResponse
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

        public List<ArtistProjectResponse> ArtistProjects { get; set; } = new List<ArtistProjectResponse>();
        public IEnumerable<string> Countries { get; set; }
        public IEnumerable<string> VoiceStyles { get; set; }
        public IEnumerable<string> Genders { get; set; }
        public IEnumerable<string> UsagePurposes { get; set; }
    }
}
