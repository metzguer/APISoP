using APiSoP.Domain.Contracts.CRUD;
using APISoP.CrossCutting.Entities;
using APISoP.CrossCutting.Responses.Operation;
using APISoP.Data.Contracts.CRUD;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APiSoP.Domain.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly IMembershipRepository _membershipRepository;
        public MembershipService(IMembershipRepository membershipRepository)
        {
            _membershipRepository = membershipRepository;
        }

        public async Task<ResultOperation<Membership>> Add(Membership entity)
        {
            var result = new ResultOperation<Membership>();

            try
            {
                entity.MembershipId = Guid.NewGuid();
                await _membershipRepository.Add(entity);
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

        public async Task<ResultOperation<IEnumerable<Membership>>> GetAll(Guid id)
        {
            var result = new ResultOperation<IEnumerable<Membership>>();

            try
            {
                result.Result = await _membershipRepository.GetAll(id);
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

        public async Task<ResultOperation<Membership>> GetById(Guid id)
        {
            var result = new ResultOperation<Membership>();

            try
            {  
                result.Result = await _membershipRepository.GetById(id);
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
                await _membershipRepository.Remove(id); 
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

        public async Task<ResultOperation<Membership>> Update(Membership entity)
        {
            var result = new ResultOperation<Membership>();

            try
            { 
                await _membershipRepository.Update(entity);
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
