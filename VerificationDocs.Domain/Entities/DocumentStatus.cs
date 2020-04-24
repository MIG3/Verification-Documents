using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerificationDocs.Domain.Entities
{
    public class DocumentStatus
    {
        public int ID { get; set; }
        public int DocID { get; set; }
        public DateTime DateCheck { get; set; }
        public int CountError { get; set; }
        public string Status { get; set; }
    }
}
