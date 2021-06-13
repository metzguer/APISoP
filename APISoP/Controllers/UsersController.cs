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
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _userService;
        private readonly IUserIdentity _userIdentity;
        public UsersController(IUsersService userService, IUserIdentity userIdentity)
        {
            _userService = userService;
            _userIdentity = userIdentity;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll() => Ok( await _userService.GetAll(_userIdentity.StoreId.Value) ); 

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetOne(string id)
        { 
            var valid = Guid.TryParse(id, out Guid idType);
            return valid ? Ok(await _userService.GetById(Guid.Parse(id))) : BadRequest("Ingresa un id valido");
        }
    }
}
