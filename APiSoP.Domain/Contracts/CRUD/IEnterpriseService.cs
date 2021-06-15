using APISoP.CrossCutting.Entities;
using APISoP.CrossCutting.Responses.Operation;
using System;
using System.Threading.Tasks;

namespace APiSoP.Domain.Contracts.CRUD
{
    public interface IEnterpriseService : IGenericServiceCRUD<Enterprise>
    {
        Task<ResultOperation<Enterprise>> GetEnterpriseAndStores(Guid id);
    }
}
