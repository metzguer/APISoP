using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APiSoP.Domain.Contracts.CRUD;
using APISoP.CrossCutting.Entities;
using APISoP.CrossCutting.Responses.Operation;
using APISoP.Data.Contracts.CRUD;

namespace APiSoP.Domain.Services
{
    public class ProfilePermissionsService : IProfilePermissionService
    {
        private readonly IProfilePermissionRepository _profilePermissionRepository;
        public ProfilePermissionsService(IProfilePermissionRepository profilePermissionRepository)
        {
            _profilePermissionRepository = profilePermissionRepository;
        }

        public async Task<ResultOperation<ProfilePermissions>> Add(ProfilePermissions entity)
        {
            var result = new ResultOperation<ProfilePermissions>();

            try
            {
                entity.ProfilePermissionsId = Guid.NewGuid();
                await _profilePermissionRepository.Add(entity);
                result.Result = entity;
                result.Success = true; 
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

        public async Task<ResultOperation<IEnumerable<ProfilePermissions>>> GetAll(Guid id)
        {
            var result = new ResultOperation<IEnumerable<ProfilePermissions>>();

            try
            {
                result.Result = await _profilePermissionRepository.GetAll(id);
                result.Success = true;
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

        public async Task<ResultOperation<ProfilePermissions>> GetById(Guid id)
        {
            var result = new ResultOperation<ProfilePermissions>();

            try
            { 
                result.Result = await _profilePermissionRepository.GetById(id);
                result.Success = true;
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

        public async Task<ResultOperation> Remove(Guid id)
        {
            var result = new ResultOperation();

            try
            {
                await _profilePermissionRepository.Remove(id); 
                result.Success = true;
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

        public async Task<ResultOperation<ProfilePermissions>> Update(ProfilePermissions entity)
        {
            var result = new ResultOperation<ProfilePermissions>();

            try
            {
                await _profilePermissionRepository.Update(entity);
                result.Result = entity;
                result.Success = true;
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
    }
}
