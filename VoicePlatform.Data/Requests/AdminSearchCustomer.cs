using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoicePlatform.Utility.Enums;

namespace VoicePlatform.Data.Requests
{
    public class AdminSearchCustomer
    {
        public string SearchString { get; set; } = string.Empty;
        public Dictionary<AdminFilterCustomer, string> Filter { get; set; } = new Dictionary<AdminFilterCustomer, string>();
        public Dictionary<AdminSortCustomer, bool> Sort { get; set; } = new Dictionary<AdminSortCustomer, bool>();
    }
}
