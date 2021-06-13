using APISoP.CrossCutting.Responses.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APiSoP.Domain.Contracts.CRUD
{
    public interface IGenericServiceCRUD<T> where T : class
    {
        Task<ResultOperation<IEnumerable<T>>> GetAll(Guid id);
        Task<ResultOperation<T>> Add(T entity);
        Task<ResultOperation<T>> GetById(Guid id);
        Task<ResultOperation<T>> Update(T entity);
        Task<ResultOperation> Remove(Guid id);
    }
}
