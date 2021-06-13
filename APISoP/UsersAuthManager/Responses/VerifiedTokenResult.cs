using APISoP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APISoP.UsersAuthManager.Responses
{
    public class VerifiedTokenResult
    {
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; }
        public string UserId { get; set; }
    }
}
