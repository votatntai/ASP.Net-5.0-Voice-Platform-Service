using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoicePlatform.Data.Entities
{
    public class CustomerToken
    {
        public Guid CustomerId { get; set; }
        public string Token { get; set; }
        public Guid Id { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
