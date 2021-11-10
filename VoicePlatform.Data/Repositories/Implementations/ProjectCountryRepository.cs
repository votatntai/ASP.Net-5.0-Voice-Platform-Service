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
    public class ProjectCountryRepository : Repository<ProjectCountry>, IProjectCountryRepository
    {
        public ProjectCountryRepository(AppDbContext context) : base(context)
        {

        }
    }
}
