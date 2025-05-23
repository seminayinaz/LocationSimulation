using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class MessageType44FWModel : IEntity
    {
        public byte MessageType { get; set; } = 44;
        public byte DataSize { get; set; } // 1
        public ushort AnchorCode { get; set; }
        public MobilePacketV2FWModel[] MobilePackets { get; set; } // Bir eleman alsın
    }
}
