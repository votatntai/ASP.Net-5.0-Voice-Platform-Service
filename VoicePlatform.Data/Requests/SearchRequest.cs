using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoicePlatform.Data.Entities;

namespace VoicePlatform.Data.Requests
{
    public class SearchRequest
    {
        public string SearchByName { get; set; }
        public string SortBy { get; set; }
        public IEnumerable<string> FilterBy { get; set; }
        public Pagination Pagination { get; set; }
    }
}
