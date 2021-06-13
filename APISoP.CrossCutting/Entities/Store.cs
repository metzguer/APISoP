using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoP.CrossCutting.Entities
{
    public class Store
    {
        public Guid StoreId { get; set; }
        public string StoreName { get; set; }
        public string StoreDescription { get; set; }
        public bool IsActive { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }

         
        //relations
        public Guid EnterpriseId { get; set; }
        public virtual Enterprise Enterprise { get; set; } 
        public virtual IEnumerable<CashRegister> CashRegisters { get; set; }
        public virtual IEnumerable<User> Users { get; set; }
        public virtual IEnumerable<Profile> Profiles { get; set; }
    }
}
