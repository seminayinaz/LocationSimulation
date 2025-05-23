using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Auth : IEntity
    {
        public bool result { get; set; }
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public int validFor { get; set; }
    }
}
