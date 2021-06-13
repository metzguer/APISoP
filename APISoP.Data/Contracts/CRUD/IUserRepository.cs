using APISoP.CrossCutting.Entities;
using System;
using System.Threading.Tasks;

namespace APISoP.Data.Contracts.CRUD
{
    public interface IUserRepository : IGenericRepositoryCRUD<User>
    {
        Task<User> GetUserForLogin(Guid guid);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByUsername(string email);
    }
}
