using APISoP.CrossCutting.DTOs.Stores;
using APISoP.CrossCutting.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoP.CrossCutting.Mappers
{
    public class StoreMapper
    {
        public static DetailStoreDTO GetDetailStore(Store store)
        {
            return new DetailStoreDTO
            {
                StoreId = store.StoreId,
                StoreName = store.StoreName,
                StoreDescription = store.StoreDescription,
                Phone = store.Phone, 
                IsActive = store.IsActive,
                PostalCode = store.PostalCode
            };
        }
    }
}
