using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoP.CrossCutting.DTOs
{
    public class AddEnterpriseDTO
    {
        [Required]
        [StringLength(maximumLength: 250)]
        public string Name { get; set; }
        public string Address { get; set; }

        [StringLength(maximumLength: 15)]
        public string Phone { get; set; }
        public string Description { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
