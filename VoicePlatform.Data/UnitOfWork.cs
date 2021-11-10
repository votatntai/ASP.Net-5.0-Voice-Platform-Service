using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using VoicePlatform.Data.Contexts;
using VoicePlatform.Data.Repositories.Implementations;
using VoicePlatform.Data.Repositories.Interfaces;

namespace VoicePlatform.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;

        private IArtistRepository _artist;
        private ICustomerRepository _customer;
        private IUserRepository _user;
        private IGenderRepository _gender;
        private ICountryRepository _country;
        private IProjectRepository _project;
        private IArtistProjectFileRepository _artistProjectFile;
        private ICustomerProjectFileRepository _customerProjectFile;
        private IArtistProjectRepository _artistProject;
        private IProjectCountryRepository _projectCountry;
        private IProjectGenderRepository _projectGender;
        private IProjectUsagePurposeRepository _projectUsagePurpose;
        private IProjectVoiceStyleRepository _projectVoiceStyle;
        private IUsagePurposeRepository _usagePurpose;
        private IVoiceStyleRepository _voiceStyle;
        private IArtistCountryRepository _artistCountry;
        private IArtistVoiceStyleRepository _artistVoiceStyle;
        private IArtistVoiceDemoRepository _artistVoiceDemo;
        private IVoiceDemoRepository _voiceDemo;
        private IArtistTokenRepository _artistToken;
        private ICustomerTokenRepository _customerToken;
        private IAdminTokenRepository _adminToken;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public IArtistRepository Artist
        {
            get { return _artist ??= new ArtistRepository(_context); }
        }
        public ICustomerRepository Customer
        {
            get { return _customer ??= new CustomerRepository(_context); }
        }
        public IUserRepository User
        {
            get { return _user ??= new UserRepository(_context); }
        }
        public IGenderRepository Gender
        {
            get { return _gender ??= new GenderRepository(_context); }
        }
        public ICountryRepository Country
        {
            get { return _country ??= new CountryRepository(_context); }
        }
        public IProjectRepository Project
        {
            get { return _project ??= new ProjectRepository(_context); }
        }
        public IArtistProjectFileRepository ArtistProjectFile
        {
            get { return _artistProjectFile ??= new ArtistProjectFileRepository(_context); }
        }
        public ICustomerProjectFileRepository CustomerProjectFile
        {
            get { return _customerProjectFile ??= new CustomerProjectFileRepository(_context); }
        }
        public IArtistProjectRepository ArtistProject
        {
            get { return _artistProject ??= new ArtistProjectRepository(_context); }
        }
        public IProjectCountryRepository ProjectCountry
        {
            get { return _projectCountry ??= new ProjectCountryRepository(_context); }
        }
        public IProjectGenderRepository ProjectGender
        {
            get { return _projectGender ??= new ProjectGenderRepository(_context); }
        }
        public IProjectUsagePurposeRepository ProjectUsagePurpose
        {
            get { return _projectUsagePurpose ??= new ProjectUsagePurposeRepository(_context); }
        }
        public IProjectVoiceStyleRepository ProjectVoiceStyle
        {
            get { return _projectVoiceStyle ??= new ProjectVoiceStyleRepository(_context); }
        }
        public IUsagePurposeRepository UsagePurpose
        {
            get { return _usagePurpose ??= new UsagePurposeRepository(_context); }
        }
        public IVoiceStyleRepository VoiceStyle
        {
            get { return _voiceStyle ??= new VoiceStyleRepository(_context); }
        }
        public IArtistCountryRepository ArtistCountry
        {
            get { return _artistCountry ??= new ArtistCountryRepository(_context); }
        }
        public IArtistVoiceStyleRepository ArtistVoiceStyle
        {
            get { return _artistVoiceStyle ??= new ArtistVoiceStyleRepository(_context); }
        }
        public IArtistVoiceDemoRepository ArtistVoiceDemo
        {
            get { return _artistVoiceDemo ??= new ArtistVoiceDemoRepository(_context); }
        }
        public IVoiceDemoRepository VoiceDemo
        {
            get { return _voiceDemo ??= new VoiceDemoRepository(_context); }
        }
        public IArtistTokenRepository ArtistToken
        {
            get { return _artistToken ??= new ArtistTokenRepository(_context); }
        }
        public ICustomerTokenRepository CustomerToken
        {
            get { return _customerToken ??= new CustomerTokenRepository(_context); }
        }
        public IAdminTokenRepository AdminToken
        {
            get { return _adminToken ??= new AdminTokenRepository(_context); }
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
