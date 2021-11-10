using System;
using System.Collections.Generic;

#nullable disable

namespace VoicePlatform.Data.Entities
{
    public partial class ProjectUsagePurpose
    {
        public Guid ProjectId { get; set; }
        public Guid UsagePurposeId { get; set; }

        public virtual Project Project { get; set; }
        public virtual UsagePurpose UsagePurpose { get; set; }
    }
}
