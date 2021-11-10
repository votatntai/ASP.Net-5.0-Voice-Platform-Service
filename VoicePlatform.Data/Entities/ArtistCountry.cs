using System;
using System.Collections.Generic;

#nullable disable

namespace VoicePlatform.Data.Entities
{
    public partial class ArtistCountry
    {
        public Guid ArtistId { get; set; }
        public Guid CountryId { get; set; }

        public virtual Artist Artist { get; set; }
        public virtual Country Country { get; set; }
    }
}
