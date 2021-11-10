using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoicePlatform.Utility.Enums;

namespace VoicePlatform.Data.Requests
{
    public class ArtistSearchProject
    {
        public string SearchString { get; set; }
        public Dictionary<CustomerFilter, string> Filter { get; set; } = new Dictionary<CustomerFilter, string>();
        public bool? IsAsc { get; set; }
    }
}
