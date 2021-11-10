using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoicePlatform.Utility.Enums;

namespace VoicePlatform.Data.Requests
{
    public class RatingsRequest
    {
        public IEnumerable<int> Filter { get; set; } = new List<int>();
        public Dictionary<SortRating, bool> Sort { get; set; } = new Dictionary<SortRating, bool>();
    }
}
