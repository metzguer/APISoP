using APiSoP.Domain.Contracts.CRUD;
using APiSoP.Services.CustomIdentity;
using APISoP.CrossCutting.DTOs;
using APISoP.CrossCutting.DTOs.Enterprises;
using APISoP.CrossCutting.Entities;
using APISoP.CrossCutting.Mappers;
using APISoP.CrossCutting.Responses.Operation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APISoP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EnterpisesController : ControllerBase
    {
        private readonly IEnterpriseService _enterpriseService;
        private readonly IUserIdentity _userIDentity;
        public EnterpisesController(IEnterpriseService enterpriseService, IUserIdentity userIDentity)
        {
            _enterpriseService = enterpriseService;
            _userIDentity = userIDentity;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll() {
            var response = await _enterpriseService.GetAll(_userIDentity.MembershipId.Value);

            var result = new ResultOperation<IEnumerable<ListEnterpriseDTO>>();
            result.Success = response.Success;
            result.Message = "Empressas registradas";
            result.Result = response.Result.Select(x => EnterpriseMapper.GetListEnterprise(x)).ToList();
            return Ok( response ); 
        } 

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOne(string id)
        { 
            var valid = Guid.TryParse(id, out Guid idType);

            if (valid) {
                var response = await _enterpriseService.GetById(Guid.Parse(id));
                
                if(!response.Success)
                    return NotFound();

                var result = new ResultOperation<DetailEnterpriseDTO>();
                result.Success = response.Success;
                result.Message = "Datos de la empresa";
                result.Result = EnterpriseMapper.GetDetailEnterprise(response.Result);

                return Ok( result );

            } else {
                return BadRequest("Ingresa un id valido");
            } 
        }

        [HttpPost]
        public async Task<ActionResult> SaveEnterprise(AddEnterpriseDTO enterprise) {

            Enterprise newEnterprise = EnterpriseMapper.SetEnterprise(enterprise);
            newEnterprise.MembershipId = _userIDentity.MembershipId.Value;

            var result = await _enterpriseService.Add(newEnterprise);
            var response = new ResultOperation<DetailEnterpriseDTO>();

            if (!result.Success)
            { 
                response.Success = result.Success;
                response.Message = "Ocurrio un error";
                response.Result = null;
                response.Errors = result.Errors;
                return BadRequest(response);
            }

            response.Success = result.Success;
            response.Message = "Empresa registrada";
            response.Result = EnterpriseMapper.GetDetailEnterprise(result.Result);

            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEnterprise(AddEnterpriseDTO enterprise, string id)
        {
            var valid = Guid.TryParse(id, out Guid idType);

            if (valid) {
                Enterprise newEnterprise = EnterpriseMapper.SetEnterprise(enterprise);
                newEnterprise.EnterpriseId = Guid.Parse(id);
                newEnterprise.MembershipId = _userIDentity.MembershipId.Value;

                var result = await _enterpriseService.Update(newEnterprise);
                var response = new ResultOperation<DetailEnterpriseDTO>();

                if (!result.Success) {
                    response.Success = false;
                    response.Message = "";
                    response.Errors = result.Errors;
                    return BadRequest(response);
                }
                   

               
                response.Success = result.Success;
                response.Message = "Datos actualizados de la empresa";
                response.Result = EnterpriseMapper.GetDetailEnterprise(result.Result);

                return Ok( response );

            } else {
                return BadRequest("Ingresa un id valido");
            }
              
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id){
            var valid = Guid.TryParse(id, out Guid idType);

            if (valid)
            {
                var result = await _enterpriseService.Remove(Guid.Parse(id));
                var response = new ResultOperation();
                if (!result.Success)
                {
                    response.Success = false;
                    response.Message = "Ocurrio un error";
                    response.Errors = result.Errors;
                    return BadRequest(response);
                }

                response.Success = result.Success;
                response.Message = "La empresa fue eliminada";
                return Ok(response);
            }
            else {
                return BadRequest("Ingresa un id valido");
            }
        }
    }
}
