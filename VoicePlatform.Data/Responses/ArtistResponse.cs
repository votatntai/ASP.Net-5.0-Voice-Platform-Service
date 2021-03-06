using System;

namespace VoicePlatform.Data.Responses
{
    public class ArtistResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
        public string Avatar { get; set; }
        public string Bio { get; set; }
        public double Price { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
