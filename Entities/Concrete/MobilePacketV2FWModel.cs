using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class MobilePacketV2FWModel : IEntity
    {
        public ushort MobileNodeCode { get; set; }
        public ushort DataCountNo { get; set; } 
        public ushort Distance { get; set; } 
        public byte DeviceState { get; set; } 
        public byte DataType { get; set; } 
        public ushort DataValue { get; set; } 

    }
}
