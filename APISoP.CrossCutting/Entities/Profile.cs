using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoP.CrossCutting.Entities
{
    public class Profile
    {
        [Key]
        public Guid ProfileId { get; set; } 
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        //relations
        public Guid StoreId { get; set; }
        public virtual Store Store { get; set; } 
        public virtual IEnumerable<ProfilePermissions> Permissions { get; set; } 
    }
}
