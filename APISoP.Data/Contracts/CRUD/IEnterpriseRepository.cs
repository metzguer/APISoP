using APISoP.CrossCutting.Entities;
using System;
using System.Threading.Tasks;

namespace APISoP.Data.Contracts.CRUD
{
    public interface IEnterpriseRepository : IGenericRepositoryCRUD<Enterprise>
    {
        Task<Enterprise> GetEnterpriseAndStores(Guid id);
    }
}
