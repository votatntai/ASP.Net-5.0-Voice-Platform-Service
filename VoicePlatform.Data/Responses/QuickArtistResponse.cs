using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoicePlatform.Data.Responses
{
    public class QuickArtistResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Bio { get; set; }
        public string Gender { get; set; }
        public string Avatar { get; set; }
        public bool Studio { get; set; }
        public double Price { get; set; }
        public double? Rate { get; set; }
        public string Status { get; set; }
        public IEnumerable<string> Countries { get; set; }
        public IEnumerable<string> VoiceStyles { get; set; }
        public IEnumerable<string> VoiceDemos { get; set; }
    }
}
