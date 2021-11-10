using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoicePlatform.Utility.Enums;

namespace VoicePlatform.Data.Requests
{
    public class AdminSearchArtist
    {
        public string SearchString { get; set; }
        public Dictionary<AdminFilterArtist, string> Filter { get; set; } = new Dictionary<AdminFilterArtist, string>();
        public Dictionary<AdminSortArtist, bool> Sort { get; set; } = new Dictionary<AdminSortArtist, bool>();
    }
}
