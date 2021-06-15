using APISoP.CrossCutting.DTOs;
using APISoP.CrossCutting.DTOs.Enterprises;
using APISoP.CrossCutting.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoP.CrossCutting.Mappers
{
    public class EnterpriseMapper
    {
        public static AddEnterpriseDTO GetEnterprise(Enterprise enterprise) {
            return new AddEnterpriseDTO {
                
            };
        }

        public static Enterprise SetEnterprise(AddEnterpriseDTO enterprise)
        {
            return new Enterprise
            {
                Name = enterprise.Name,
                Address = enterprise.Address,
                Phone = enterprise.Phone, 
                Description = enterprise.Description,
                IsActive = enterprise.IsActive
            };
        }

        public static Enterprise SetUpdateEnterprise(AddEnterpriseDTO enterprise)
        {
            return new Enterprise
            {
                Name = enterprise.Name,
                Address = enterprise.Address,
                Phone = enterprise.Phone,
                Description = enterprise.Description,
                IsActive = enterprise.IsActive
            };
        }

        public static ListEnterpriseDTO GetListEnterprise(Enterprise enterprise)
        {
            return new ListEnterpriseDTO
            {
                EnterpriseId = enterprise.EnterpriseId, 
                Name = enterprise.Name,
                Address = enterprise.Address,
                Phone = enterprise.Phone,
                Description = enterprise.Description,
                IsActive = enterprise.IsActive,
                Created = enterprise.Created,
                Updated = enterprise.Updated
            };
        }

        public static DetailEnterpriseDTO GetDetailEnterprise(Enterprise enterprise)
        {
            return new DetailEnterpriseDTO
            {
                EnterpriseId = enterprise.EnterpriseId,
                Name = enterprise.Name,
                Address = enterprise.Address,
                Phone = enterprise.Phone,
                Description = enterprise.Description,
                IsActive = enterprise.IsActive,
                Created = enterprise.Created,
                Updated = enterprise.Updated,

                Stores = (enterprise.Stores != null && enterprise.Stores.Count() > 0) ? 
                enterprise.Stores.Select(x => StoreMapper.GetDetailStore(x)).ToList() : null
            };
        }
    }
}
