﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoicePlatform.Data.Requests
{
    public class ModifyFileRequest
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
    }
}
