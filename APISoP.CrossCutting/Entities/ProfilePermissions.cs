using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoP.CrossCutting.Entities
{
    public class ProfilePermissions
    {
        [Key]
        public Guid ProfilePermissionsId { get; set; } 
        public bool Enable { get; set; }

        //relations
        public Guid ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
        public Guid PermissionId { get; set; }
        public virtual Permission Permission { get; set; }
    }
}
