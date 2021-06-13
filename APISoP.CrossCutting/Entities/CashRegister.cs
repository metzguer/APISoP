using System; 
using System.ComponentModel.DataAnnotations; 

namespace APISoP.CrossCutting.Entities
{
    public class CashRegister
    {
        [Key]
        public Guid CashRegisterId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }

        //relations
        public Guid StoreId { get; set; }
        public virtual Store Store { get; set; }
         
    }
}
