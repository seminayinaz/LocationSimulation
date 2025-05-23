using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Reader
    {
        public int readerNodeCode { get; set; }
        public int tag_ID { get; set; }
        public int power0 { get; set; }
        public int power1 { get; set; }
        public int power2 { get; set; }
        public int power3 { get; set; }
        public bool deleted { get; set; }
        public DateTime lastUpdateDate { get; set; }
        public object tbl_Tag { get; set; }
    }
}
