using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class AuthSuccess : IEntity
    {
        public bool success { get; set; }
        public object errorMessage { get; set; }
        public Auth result { get; set; }
    }
}
