using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoicePlatform.Utility.Enums;

namespace VoicePlatform.Data.Requests
{
    public class CustomerSearchArtist
    {
        public string SearchString { get; set; }
        public Dictionary<CustomerFilterArtist, string> Filter { get; set; } = new Dictionary<CustomerFilterArtist, string>();
        public bool? isAsc { get; set; }
    }
}
