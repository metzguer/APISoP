using APiSoP.Domain.Contracts.CRUD; 
using APISoP.CrossCutting.Entities;
using APISoP.CrossCutting.Responses.Operation;
using APISoP.CrossCutting.Types;
using APISoP.Data.Contracts.CRUD;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APiSoP.Domain.Services
{ 
    public class UserService : IUsersService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        } 

        public async Task<ResultOperation<IEnumerable<User>>> GetAll(Guid id)
        {
            var result = new ResultOperation<IEnumerable<User>> ();

            try
            {
                result.Result = await _userRepository.GetAll(id);
                result.Success = true;
                result.Message = MessagesUsers.ObtainAll;
            }
            catch (Exception ex)
            {
                result.Success = false;

                result.Errors.Add(new ItemError
                {
                    Code = "Exception",
                    Description = ex.Message
                });
            }

            return result;
        }

        public async Task<ResultOperation<User>> GetById(Guid id)
        {
            var result = new ResultOperation<User>();

            try
            {
                result.Result = await _userRepository.GetById(id);
                result.Success = true;
                result.Message = MessagesUsers.Obtain + id;
            }
            catch (Exception ex)
            {
                result.Success = false;

                result.Errors.Add(new ItemError
                {
                    Code = "Exception",
                    Description = ex.Message
                });
            }

            return result;
        }

        public async Task<ResultOperation> Remove(Guid id)
        {
            var result = new ResultOperation();

            try
            { 
                await _userRepository.Remove(id); 
                result.Success = true;
                result.Message = MessagesUsers.Deleted;
            }
            catch (Exception ex)
            {
                result.Success = false; 

                result.Errors.Add(new ItemError
                {
                    Code = "Exception",
                    Description = ex.Message
                });
            }

            return result;
        }

        public async Task<ResultOperation<User>> Update(User entity)
        {
            var result = new ResultOperation<User>();

            try
            { 
                await _userRepository.Update(entity);
                result.Result = entity;
                result.Success = true;
                result.Message = MessagesUsers.Updated;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Result = null;

                result.Errors.Add(new ItemError
                {
                    Code = "Exception",
                    Description = ex.Message
                });
            }

            return result;
        }

        public async Task<ResultOperation<User>> Add(User entity)
        {
            var result = new ResultOperation<User>();
         
            try
            {
                entity.UserId = Guid.NewGuid();
                await _userRepository.Add(entity);
                result.Result = entity;
                result.Success = true;
                result.Message = MessagesUsers.Created;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Result = null;

                result.Errors.Add( new ItemError {
                    Code ="Exception",
                    Description = ex.Message
                } );
            }

            return result;
        }

        public async Task<ResultOperation<User>> CreateNewAccount(Guid guid, string username, string email, string nameUser, string nameEnterprise)
        {
            var result = new ResultOperation<User>();

            try
            {
                var validations = await ValidationsForNewAccount(email, username);

                if (validations == null)
                {
                    var membership = new Membership
                    {
                        MembershipId = Guid.NewGuid(),
                        IsActive = true,
                        TypeMembership = TypeMembership.Free
                    };

                    var enterprise = new Enterprise
                    {
                        EnterpriseId = Guid.NewGuid(),
                        Name = nameEnterprise,
                        IsActive = true,
                        MembershipId = membership.MembershipId,
                        Created = DateTime.Now,
                        Updated = DateTime.Now,

                    };

                    var store = new Store
                    {
                        StoreId = Guid.NewGuid(),
                        StoreName = enterprise.Name,
                        IsActive = true,
                        EnterpriseId = enterprise.EnterpriseId
                    };

                    var profile = new Profile
                    {
                        ProfileId = Guid.NewGuid(),
                        Name = "Administrador",
                        Description = "Usuario administrador",
                        IsActive = true,
                        StoreId = store.StoreId
                    };

                    var user = new User
                    {
                        UserId = guid,
                        Name = nameUser,
                        Username = username,
                        Email = email,
                        TypeUser = TypeUser.Owner,
                        IsActive = true,
                        StoreId = store.StoreId,
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        ProfileId = profile.ProfileId
                    };

                    enterprise.Membership = membership;
                    store.Enterprise = enterprise;
                    profile.Store = store;

                    user.Profile = profile;

                    await _userRepository.Add(user);

                    user.Store = store;

                    result.Result = user;
                    result.Success = true;
                    result.Message = "Cuenta registrada";
                }
                else{
                    result.Success = false;
                    result.Errors.Add(new ItemError
                    {
                        Code = "Error: ",
                        Description = $"{validations}"
                    });
                }

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Errors.Add(new ItemError {
                    Code = "Excepcion: ",
                    Description = $"{ex}"
                });
            }
            return result;
        }
        private async Task<string> ValidationsForNewAccount(string email, string username) {

            var existEmail = await _userRepository.GetUserByEmail(email);
            var existUsername = await _userRepository.GetUserByUsername(username);

            if (existEmail != null)
                return "El email ya existe";
            if (existUsername != null)
                return "El usuario ya existe";

            return null;
        } 

        public async Task<ResultOperation<User>> GetUserForLogin(Guid guid)
        {
            var result = new ResultOperation<User>();

            try
            {
                result.Result = await _userRepository.GetUserForLogin(guid);
                result.Success = true;
                result.Message = MessagesUsers.Obtain + guid;
            }
            catch (Exception ex)
            {
                result.Success = false;

                result.Errors.Add(new ItemError
                {
                    Code = "Exception",
                    Description = ex.Message
                });
            }

            return result;
        }
    }
}
