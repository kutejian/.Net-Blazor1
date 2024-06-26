﻿using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Models.ViewModels
{
    public class GoogleUserViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
        
        [JsonProperty("verified_email")]
        public bool VerifyEmail { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("given_name")]
        public string GivenName { get; set; }

        [JsonProperty("family_name")]
        public string FamilyName { get; set; }

        [JsonProperty("picture")]
        public string PictureUrl { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }
    }
}
