using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class DistancesFromStabilTags:IEntity, IConvertible
    {
        public int Id { get; set; }
        public int InterpolatedId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Altitude { get; set; }
        public double Distance { get; set; }

        public TypeCode GetTypeCode()
        {
            throw new NotImplementedException();
        }

        public bool ToBoolean(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public byte ToByte(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public char ToChar(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public DateTime ToDateTime(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public decimal ToDecimal(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public double ToDouble(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public short ToInt16(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public int ToInt32(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public long ToInt64(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public sbyte ToSByte(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public float ToSingle(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public string ToString(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public object ToType(Type conversionType, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public ushort ToUInt16(IFormatProvider? provider)
        {
            return Convert.ToUInt16(Distance);
        }

        public uint ToUInt32(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public ulong ToUInt64(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }
    }
}
