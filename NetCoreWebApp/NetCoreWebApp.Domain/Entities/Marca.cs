﻿using NetCoreWebApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApp.Domain.Entities
{
    public class Marca : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
