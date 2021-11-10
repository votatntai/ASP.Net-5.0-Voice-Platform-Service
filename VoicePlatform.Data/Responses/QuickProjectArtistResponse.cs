using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoicePlatform.Data.Responses
{
    public class QuickProjectArtistResponse
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }

        public Guid CustomerId { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerAvatar { get; set; }

        public DateTime? InvitedDate { get; set; }
        public DateTime? RequestedDate { get; set; }
        public DateTime? JoinedDate { get; set; }
        public DateTime? CanceledDate { get; set; }
        public DateTime? FinishedDate { get; set; }
        public DateTime? ReviewDate { get; set; }
        public string Status { get; set; }
    }
}
