using APiSoP.Domain.Contracts.CRUD;
using APISoP.CrossCutting.Entities;
using APISoP.CrossCutting.Responses.Operation;
using APISoP.Data.Contracts.CRUD;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APiSoP.Domain.Services
{ 
    public class CashRegisterService : ICashRegisterService
    {
        private readonly ICashRegisterRepository _cashRegisterRepository;
 
        public CashRegisterService(ICashRegisterRepository cashRegisterRepository)
        {
            _cashRegisterRepository = cashRegisterRepository;
        }

        public async Task<ResultOperation<CashRegister>> Add(CashRegister entity)
        {
            var result = new ResultOperation<CashRegister>();

            try
            {
                entity.CashRegisterId = Guid.NewGuid();
                await _cashRegisterRepository.Add(entity);
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

        public async Task<ResultOperation<IEnumerable<CashRegister>>> GetAll(Guid id)
        {
            var result = new ResultOperation<IEnumerable<CashRegister>>();

            try
            {  
                result.Result = await _cashRegisterRepository.GetAll(id);
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

        public async Task<ResultOperation<CashRegister>> GetById(Guid id)
        {
            var result = new ResultOperation<CashRegister>();

            try
            {
                result.Result = await _cashRegisterRepository.GetById(id);
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
                await _cashRegisterRepository.Remove(id);
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

        public async Task<ResultOperation<CashRegister>> Update(CashRegister entity)
        {
            var result = new ResultOperation<CashRegister>();

            try
            {
                await _cashRegisterRepository.Update(entity);
                result.Result = entity;
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
    }
}
