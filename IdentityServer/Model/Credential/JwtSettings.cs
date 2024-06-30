using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Model.Credential
{
    public class JwtSettings
    {
        public string JwtSecurityKey { get; set; }
        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }
        public int JwtExpiryInDays { get; set; }
    }
}
