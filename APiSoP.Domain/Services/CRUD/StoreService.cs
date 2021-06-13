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
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        public StoreService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<ResultOperation<Store>> Add(Store entity)
        {
            var result = new ResultOperation<Store>();

            try
            {
                entity.StoreId = Guid.NewGuid();
                await _storeRepository.Add(entity);
                result.Result = entity;
                result.Success = true;
                result.Message = MessagesStores.Created;
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

        public async Task<ResultOperation<IEnumerable<Store>>> GetAll(Guid id)
        {
            var result = new ResultOperation<IEnumerable<Store>>();

            try
            {
                result.Result = await _storeRepository.GetAll(id);
                result.Success = true;
                result.Message = MessagesStores.ObtainAll;
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

        public async Task<ResultOperation<Store>> GetById(Guid id)
        {
            var result = new ResultOperation<Store>();

            try
            {  
                result.Result = await _storeRepository.GetById(id);
                result.Success = true;
                result.Message = MessagesStores.Obtain;
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
                await _storeRepository.Remove(id); 
                result.Success = true;
                result.Message = MessagesStores.Deleted;
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

        public async Task<ResultOperation<Store>> Update(Store entity)
        {
            var result = new ResultOperation<Store>();

            try
            { 
                await _storeRepository.Update(entity);
                result.Result = entity;
                result.Success = true;
                result.Message = MessagesStores.Updated;
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
