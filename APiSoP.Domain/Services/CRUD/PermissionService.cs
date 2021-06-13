using APiSoP.Domain.Contracts.CRUD;
using APISoP.CrossCutting.Entities;
using APISoP.CrossCutting.Responses.Operation;
using APISoP.Data.Contracts.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APiSoP.Domain.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository; 
        }

        public async Task<ResultOperation<Permission>> Add(Permission entity)
        {
            var result = new ResultOperation<Permission>();

            try
            {
                entity.PermissionId = Guid.NewGuid();
                await _permissionRepository.Add(entity);
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

        public async Task<ResultOperation<IEnumerable<Permission>>> GetAll(Guid id)
        {
            var result = new ResultOperation<IEnumerable<Permission>>();

            try
            {
                result.Result = await _permissionRepository.GetAll(id);
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

        public async Task<ResultOperation<Permission>> GetById(Guid id)
        {
            var result = new ResultOperation<Permission>();

            try
            {
                result.Result = await _permissionRepository.GetById(id);
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

        public async Task<ResultOperation> Remove(Guid id)
        {
            var result = new ResultOperation();

            try
            { 
                await _permissionRepository.Remove(id); 
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

        public async Task<ResultOperation<Permission>> Update(Permission entity)
        {
            var result = new ResultOperation<Permission>();

            try
            { 
                await _permissionRepository.Update(entity);
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
