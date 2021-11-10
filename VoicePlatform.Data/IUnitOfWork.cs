using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoicePlatform.Data.Repositories.Interfaces;

namespace VoicePlatform.Data
{
    public interface IUnitOfWork
    {
        public IArtistRepository Artist { get; }
        public ICustomerRepository Customer { get; }
        public IUserRepository User { get; }
        public IGenderRepository Gender { get; }
        public ICountryRepository Country { get; }
        public IProjectRepository Project { get; }
        public IArtistProjectFileRepository ArtistProjectFile { get; }
        public ICustomerProjectFileRepository CustomerProjectFile { get; }
        public IArtistProjectRepository ArtistProject { get; }
        public IProjectCountryRepository ProjectCountry { get; }
        public IProjectGenderRepository ProjectGender { get; }
        public IProjectUsagePurposeRepository ProjectUsagePurpose { get; }
        public IProjectVoiceStyleRepository ProjectVoiceStyle { get; }
        public IUsagePurposeRepository UsagePurpose { get; }
        public IVoiceStyleRepository VoiceStyle { get; }
        public IArtistCountryRepository ArtistCountry { get; }
        public IArtistVoiceStyleRepository ArtistVoiceStyle { get; }
        public IArtistVoiceDemoRepository ArtistVoiceDemo { get; }
        public IVoiceDemoRepository VoiceDemo { get; }
        public IArtistTokenRepository ArtistToken { get; }
        public ICustomerTokenRepository CustomerToken { get; }
        public IAdminTokenRepository AdminToken { get; }

        Task<int> SaveChanges();
    }
}
