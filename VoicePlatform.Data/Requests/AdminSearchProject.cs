using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoicePlatform.Utility.Enums;

namespace VoicePlatform.Data.Requests
{
    public class AdminSearchProject
    {
        public string SearchString { get; set; }
        public Dictionary<AdminSort, bool> Sort { get; set; } = new Dictionary<AdminSort, bool>();
        public Dictionary<AdminFilter, string> Filter { get; set; } = new Dictionary<AdminFilter, string>();
    }
}
