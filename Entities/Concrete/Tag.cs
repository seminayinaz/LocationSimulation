using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Tag : IEntity
    {
        public int tag_ID { get; set; }
        public int code { get; set; }
        public string type { get; set; }
        public int subType { get; set; }
        public double x1 { get; set; }
        public double z1 { get; set; }
        public int? region_ID { get; set; }
        public string regionName { get; set; }
        public string tagIP { get; set; }
        public DateTime lastUpdateDate { get; set; }
        public int lastUpdateUser_ID { get; set; }
        public int? subRegion_ID { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public double altitude { get; set; }
        public int? maxPower { get; set; }
        public object parentTag_ID { get; set; }
        public SubRegion subRegion { get; set; }
        public Reader readerMinRSSI { get; set; }
        public List<TagMatch> tbl_TagMatches { get; set; }
        public bool deleted { get; set; }

    }
}
