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
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        } 

        public async Task<ResultOperation<Profile>> Add(Profile entity)
        {
            var result = new ResultOperation<Profile>();

            try
            {
                entity.ProfileId = Guid.NewGuid();
                await _profileRepository.Add(entity);
                result.Result = entity;
                result.Success = true;
                result.Message = MessagesProfiles.Created;
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

        public async Task<ResultOperation<IEnumerable<Profile>>> GetAll(Guid id)
        {
            var result = new ResultOperation<IEnumerable<Profile>>();

            try
            {
                result.Result = await _profileRepository.GetAll(id);
                result.Success = true;
                result.Message = MessagesProfiles.ObtainAll;
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

        public async Task<ResultOperation<Profile>> GetById(Guid id)
        {
            var result = new ResultOperation<Profile>();

            try
            {  
                result.Result = await _profileRepository.GetById(id);
                result.Success = true;
                result.Message = MessagesProfiles.Obtain;
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
                await _profileRepository.Remove(id); 
                result.Success = true;
                result.Message = MessagesProfiles.Deleted;
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

        public async Task<ResultOperation<Profile>> Update(Profile entity)
        {
            var result = new ResultOperation<Profile>();

            try
            { 
                await _profileRepository.Update(entity);
                result.Result = entity;
                result.Success = true;
                result.Message = MessagesProfiles.Updated;
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
