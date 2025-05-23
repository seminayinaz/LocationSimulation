using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class TagMatch
    {
        public int match_ID { get; set; }
        public int tag_ID { get; set; }
        public int? person_ID { get; set; }
        public int? equipment_ID { get; set; }
        public bool deleted { get; set; }
        public DateTime lastUpdateDate { get; set; }
        public int lastUpdateUser_ID { get; set; }
        public object tag { get; set; }
        public object equipment { get; set; }
        public object person { get; set; }
        public object tbl_Locations { get; set; }
    }
}
