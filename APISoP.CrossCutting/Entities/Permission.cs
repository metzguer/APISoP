using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoP.CrossCutting.Entities
{
    public class Permission
    { 
        [Key]
        public Guid PermissionId { get; set; }

        [Required]
        [StringLength(maximumLength: 50)]
        public string Module { get; set; }
        
        [Required]
        [StringLength(maximumLength: 50)]
        public string Action { get; set; }

        [StringLength(maximumLength: 250)]
        public string Description { get; set; }

        [Required]
        public bool IsActive { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime Updated { get; set; }
    }
     
}
