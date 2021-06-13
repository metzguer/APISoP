using APISoP.UsersAuthManager.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APISoP.Models
{
    public class APIAuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public APIAuthDbContext(DbContextOptions<APIAuthDbContext> options) : base(options)
        {

        }

        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
