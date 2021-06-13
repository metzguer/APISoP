using APISoP.CrossCutting.Types;
using System; 
using System.ComponentModel.DataAnnotations; 

namespace APISoP.CrossCutting.Entities
{
    public class Membership
    {
        [Key]
        public Guid MembershipId { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public TypeMembership TypeMembership { get; set; }
 
        //relations   
        public virtual Enterprise Enterprise { get; set; }
    }
}
