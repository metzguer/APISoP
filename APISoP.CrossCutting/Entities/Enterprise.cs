using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoP.CrossCutting.Entities
{
    public class Enterprise
    {
        [Key]
        public Guid EnterpriseId { get; set; }

        [Required]
        [StringLength(maximumLength: 250)]
        public string Name { get; set; } 
        public string Address { get; set; }
        
        [StringLength(maximumLength:15)]
        public string Phone { get; set; }
        public string Description { get; set; }
        
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime Updated { get; set; }

        //relations
        public Guid MembershipId { get; set; }
        public virtual Membership Membership { get; set; }
        public virtual IEnumerable<Store> Stores { get; set; } 
         
    }
}
