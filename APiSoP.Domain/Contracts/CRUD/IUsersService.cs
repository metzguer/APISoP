
using APISoP.CrossCutting.Entities;
using APISoP.CrossCutting.Responses.Operation;
using System;
using System.Threading.Tasks;

namespace APiSoP.Domain.Contracts.CRUD
{
    public interface IUsersService : IGenericServiceCRUD<User>
    {
        Task<ResultOperation<User>> CreateNewAccount(Guid guid, string username, string Email, string name, string nameEnterprise);
        Task<ResultOperation<User>> GetUserForLogin(Guid guid);
        //Task<ResultOperation<User>> GetUserByEmail(string email);
    }
}
