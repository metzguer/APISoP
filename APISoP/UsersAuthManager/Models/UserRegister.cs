using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APISoP.UsersAuthManager.Models
{
    public class UserRegister
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string EnterpriseName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
