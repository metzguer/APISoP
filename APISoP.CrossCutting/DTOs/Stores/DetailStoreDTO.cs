using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoP.CrossCutting.DTOs.Stores
{
    public class DetailStoreDTO
    {
        public Guid StoreId { get; set; }
        public string StoreName { get; set; }
        public string StoreDescription { get; set; }
        public bool IsActive { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
    }
}
