using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class TagSuccess
    {
        public bool success { get; set; }
        public object errorCode { get; set; }
        public object errorMessage { get; set; }
        public List<Tag> result { get; set; }
    }
}
