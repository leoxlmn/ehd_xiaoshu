using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EHD.Model.DTO {
    public class DocumentDTO {
        public int Id { get; set; }
        public int Version { get; set; }
        public DateTime LoadedTime { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public string DocumentType { get; set; }
        public string DocumentName { get; set; }
        public string DocumentNote { get; set; }
        public DateTime DocumentCreationTime { get; set; }
    }
}
