using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoicePlatform.Data.Responses
{
    public class ArtistFileResponse
    {
        public ICollection<FileResponse> ArtistProjectFile { get; set; }
    }
}
