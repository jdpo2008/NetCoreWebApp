﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreWebApp.Application.DTOs.Email
{
    public class EmailRequest
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string From { get; set; }
        public List<string> Copy { get; set; }
        public string Files { get; set; }
    }
}
