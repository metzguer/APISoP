using System;
using System.Collections.Generic; 
using System.Threading.Tasks;

namespace APISoP.Data.Contracts.CRUD
{
    public interface IGenericRepositoryCRUD<T> where T :class
    {
        Task<IEnumerable<T>> GetAll(Guid id);
        Task Add(T entity);
        Task<T> GetById(Guid guid);
        Task Update(T entity); 
        Task Remove(Guid guid);
    }
}
