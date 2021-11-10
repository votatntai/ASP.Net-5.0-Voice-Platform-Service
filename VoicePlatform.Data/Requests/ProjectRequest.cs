using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoicePlatform.Data.Requests
{
    public class ProjectRequest
    {
        public string Name { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime ResponseDeadline { get; set; }
        public DateTime ProjectDeadline { get; set; }
        public List<ProjectFile> CustomerProjectsFile { get; set; }
        public List<string> ProjectCountries { get; set; }
        public List<string> ProjectGenders { get; set; }
        public List<string> ProjectUsagePurposes { get; set; }
        public List<string> ProjectVoiceStyles { get; set; }
    }
}
