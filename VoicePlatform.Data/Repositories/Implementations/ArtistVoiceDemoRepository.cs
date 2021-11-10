using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoicePlatform.Data.Contexts;
using VoicePlatform.Data.Entities;
using VoicePlatform.Data.Repositories.Interfaces;

namespace VoicePlatform.Data.Repositories.Implementations
{
    public class ArtistVoiceDemoRepository : Repository<ArtistVoiceDemo>, IArtistVoiceDemoRepository
    {
        public ArtistVoiceDemoRepository(AppDbContext context) : base(context)
        {

        }
    }
}
