﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ConfirmWorkRequest
    {
        public int WorkId { get; set; }
        public int UserId { get; set; }
    }
}
