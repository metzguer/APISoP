using APISoP.CrossCutting.DTOs.Stores;
using APISoP.CrossCutting.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq; 

namespace APISoP.CrossCutting.DTOs.Enterprises
{
    public class DetailEnterpriseDTO
    {
        public Guid EnterpriseId { get; set; }
        [Required]
        [StringLength(maximumLength: 250)]
        public string Name { get; set; }
        public string Address { get; set; }

        [StringLength(maximumLength: 15)]
        public string Phone { get; set; }
        public string Description { get; set; }

        [Required]
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public virtual IEnumerable<DetailStoreDTO> Stores { get; set; }
    }
}
