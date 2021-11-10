using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoicePlatform.Data.Requests
{
    public class SubArtistRequest
    {
        public List<string> VoiceStyles { get; set; }
        public List<string> Countries { get; set; }
    }
}
