using System;
using System.Collections.Generic;

#nullable disable

namespace VoicePlatform.Data.Entities
{
    public partial class ArtistProject
    {
        public Guid ArtistId { get; set; }
        public Guid ProjectId { get; set; }
        public double? Rate { get; set; }
        public string Comment { get; set; }
        public DateTime? InvitedDate { get; set; }
        public DateTime? RequestedDate { get; set; }
        public DateTime? JoinedDate { get; set; }
        public DateTime? CanceledDate { get; set; }
        public DateTime? FinishedDate { get; set; }
        public DateTime? ReviewDate { get; set; }
        public int Status { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual Project Project { get; set; }
    }
}
