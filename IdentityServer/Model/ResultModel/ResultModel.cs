﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Models.ResultModel
{
    public class ResultModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
    }
}
