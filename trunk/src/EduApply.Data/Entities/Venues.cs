﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduApply.Data.Entities
{
    public class Venues:BaseEntity<int>
    {
        public string   Name { get; set; }
        public int Capacity { get; set; }
        public bool Active { get; set; }
    }
}
