using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace VoicePlatform.Data.Entities
{
    public class ArtistToken
    {
        public Guid ArtistId { get; set; }
        public string Token { get; set; }
        public Guid Id { get; set; }

        public virtual Artist Artist { get; set; }
    }
}
