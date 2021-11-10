using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoicePlatform.Data.Responses
{
    public class FileResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public Guid ProjectId { get; set; }
        public string MediaType { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public Guid CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
