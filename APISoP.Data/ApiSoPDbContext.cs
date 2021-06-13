using APISoP.CrossCutting.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoP.Data
{
    public class ApiSoPDbContext : DbContext 
    {
        public ApiSoPDbContext(DbContextOptions<ApiSoPDbContext> options) : base(options)
        {

        }
        public virtual DbSet<CashRegister> CashRegisters { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Enterprise> Enterprises { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ProfilePermissions> ProfilePermissions { get; set; }
        public virtual DbSet<Membership> Memberships { get; set; }
         
    }
}
