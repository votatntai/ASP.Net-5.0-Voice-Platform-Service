using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoicePlatform.Data.Responses
{
    public class QuickArtistProjectResponse
    {
        public Guid ArtistId { get; set; }
        public string ArtistFirstName { get; set; }
        public string ArtistLastName { get; set; }
        public string ArtistAvatar { get; set; }
        public DateTime? InvitedDate { get; set; }
        public DateTime? RequestedDate { get; set; }
        public DateTime? JoinedDate { get; set; }
        public DateTime? CanceledDate { get; set; }
        public DateTime? FinishedDate { get; set; }
        public DateTime? ReviewDate { get; set; }
        public string Status { get; set; }
        public double? Rate { get; set; }
        public string Comment { get; set; }
    }
}
