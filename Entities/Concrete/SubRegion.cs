using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class SubRegion
    {
        public int subRegion_ID { get; set; }
        public int region_ID { get; set; }
        public bool deleted { get; set; }
        public DateTime lastUpdateDate { get; set; }
        public int lastUpdateUser_ID { get; set; }
        public string name { get; set; }
        public int subRegion_Type { get; set; }
        public double area { get; set; }
        public bool isSilentZone { get; set; }
        public int slowDownType { get; set; }
        public int safezoneProfile { get; set; }
        public int? firmware_ID { get; set; }
        public int virtualSubRegionType { get; set; }
        public List<object> tbl_Tags { get; set; }
        public object region { get; set; }
        public List<object> tbl_SubRegionCoordinates { get; set; }
        public List<object> tbl_SubRegionDetail { get; set; }
        public object tbl_Locations { get; set; }
        public int isForLocation { get; set; }
    }
}
