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
    public class EnterpriseService : IEnterpriseService
    {
        private readonly IEnterpriseRepository _enterpriseRepository;
        public EnterpriseService(IEnterpriseRepository enterpriseRepository)
        {
            _enterpriseRepository = enterpriseRepository;
        }

        public async Task<ResultOperation<Enterprise>> Add(Enterprise entity)
        {
            var result = new ResultOperation<Enterprise>();

            try
            {
                entity.EnterpriseId = Guid.NewGuid();
                entity.Created = DateTime.Now;
                entity.Updated = DateTime.Now; 

                await _enterpriseRepository.Add(entity);
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

        public async Task<ResultOperation<IEnumerable<Enterprise>>> GetAll(Guid id)
        {
            var result = new ResultOperation<IEnumerable<Enterprise>>();

            try
            {
                result.Result = await _enterpriseRepository.GetAll(id);
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

        public async Task<ResultOperation<Enterprise>> GetById(Guid id)
        {
            var result = new ResultOperation<Enterprise>();

            try
            {  
                result.Result = await _enterpriseRepository.GetById(id);
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
        public async Task<ResultOperation<Enterprise>> GetEnterpriseAndStores(Guid id)
        {
            var result = new ResultOperation<Enterprise>();

            try
            {
                result.Result = await _enterpriseRepository.GetEnterpriseAndStores(id);
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
                await _enterpriseRepository.Remove(id); 
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

        public async Task<ResultOperation<Enterprise>> Update(Enterprise entity)
        {
            var result = new ResultOperation<Enterprise>();

            try
            {
                entity.Updated = DateTime.Now;

                await _enterpriseRepository.Update(entity);
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
