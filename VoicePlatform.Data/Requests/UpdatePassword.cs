using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoicePlatform.Data.Requests
{
    public class UpdatePassword
    {
        public string Email { get; set; }
        public string  Password { get; set; }
    }
}
