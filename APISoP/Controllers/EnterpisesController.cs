using APiSoP.Domain.Contracts.CRUD;
using APiSoP.Services.CustomIdentity;
using APISoP.CrossCutting.Entities;
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
        public async Task<ActionResult> GetAll() => Ok( await _enterpriseService.GetById(_userIDentity.EnterpriseId.Value) ); 

        [HttpGet("{id}")]
        public async Task<ActionResult<Enterprise>> GetOne(string id)
        { 
            var valid = Guid.TryParse(id, out Guid idType);

            return valid ? Ok( await _enterpriseService.GetById(Guid.Parse(id)) ) : BadRequest("Ingresa un id valido");
        }
    }
}
