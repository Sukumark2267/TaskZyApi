﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class DeviceTokenRequest
    {
        public int UserId { get; set; }
        public string DeviceToken { get; set; }
    }

}
