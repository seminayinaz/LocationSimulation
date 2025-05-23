using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class AuthRegion : IEntity
    {
        public int regionId { get; set; }
        public string regionName { get; set; }
        public bool isDeleted { get; set; }
        public double height { get; set; }
        public double imageHeight { get; set; }
        public double imageWidth { get; set; }
        public bool isDefaultRegion { get; set; }
        public DateTime lastUpdateDate { get; set; }
        public int lastUpdateUser_ID { get; set; }
        public double latitude1 { get; set; }
        public double latitude2 { get; set; }
        public double longitude1 { get; set; }
        public double longitude2 { get; set; }
        public byte[] picture { get; set; }
        public string pictureUrl { get; set; }
        public double? pixelFactorDistance { get; set; }
        public double? pixelFactorX { get; set; }
        public double? pixelFactorY { get; set; }
        public double? refPointX1 { get; set; }
        public double? refPointX2 { get; set; }
        public double? refPointY1 { get; set; }
        public double? refPointY2 { get; set; }
        public double regionType { get; set; }
        public double width { get; set; }
    }
}
