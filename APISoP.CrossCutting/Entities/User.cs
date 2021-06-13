using APISoP.CrossCutting.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoP.CrossCutting.Entities
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required] 
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public TypeUser TypeUser { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        //relations
        public Guid? ProfileId { get; set; }
        public virtual Profile Profile {get;set;}

        public Guid? StoreId { get; set; }
        public virtual Store Store { get; set; }
    }
}
