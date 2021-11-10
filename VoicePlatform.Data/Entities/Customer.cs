using System;
using System.Collections.Generic;

#nullable disable

namespace VoicePlatform.Data.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerProjectFiles = new HashSet<CustomerProjectFile>();
        }

        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Role { get; set; }
        public Guid Gender { get; set; }
        public string Avatar { get; set; }
        public int Status { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateBy { get; set; }

        public virtual Gender GenderNavigation { get; set; }
        public virtual User UpdateByNavigation { get; set; }
        public virtual ICollection<CustomerProjectFile> CustomerProjectFiles { get; set; }
        public virtual ICollection<CustomerToken> CustomerToken { get; set; }
    }
}
