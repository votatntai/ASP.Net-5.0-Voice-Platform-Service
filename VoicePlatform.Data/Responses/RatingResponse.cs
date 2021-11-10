using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoicePlatform.Data.Responses
{
    public class RatingResponse
    {
        public Guid Id { get; set; }
        public string Avatar { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }

        public double Rate { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
