using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoP.Config.JWT
{
    public class ParametersTokens
    {
        public TokenValidationParameters TokenValidationParameters;
        public ParametersTokens(string key)
        {
            var keyEncode = Encoding.ASCII.GetBytes(key);
            TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(keyEncode),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                RequireExpirationTime = false,
                ClockSkew = TimeSpan.Zero
            };
        }
    }
}
