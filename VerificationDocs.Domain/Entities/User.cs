using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerificationDocs.Domain.Entities
{
    public class User
    {
        public int ID { get; set; }
        public int DocID { get; set; }
        public string Name { get; set; }
        public string SecName { get; set; }
        public string ThName { get; set; }
        public int NumberInstitute { get; set; }
        public string Faculty { get; set; }
    }
}
