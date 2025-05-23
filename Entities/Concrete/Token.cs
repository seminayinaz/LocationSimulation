﻿using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Token : IEntity
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
    }
}
