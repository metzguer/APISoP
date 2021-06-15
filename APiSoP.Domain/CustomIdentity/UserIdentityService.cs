using APISoP.CrossCutting.Entities;
using APISoP.Data.Contracts.CRUD;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APiSoP.Services.CustomIdentity
{
    public class UserIdentityService : IUserIdentity
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUserRepository _userData;
        private readonly User _user;
        public UserIdentityService(IHttpContextAccessor httpContext, IUserRepository userData)
        {
            _httpContext = httpContext;
            _userData = userData;
            //_user = _userData.GetById( Guid.Parse(_httpContext.HttpContext.User.Claims.FirstOrDefault(t => t.Type == "Id").Value) ).Result;
        }

        public bool UserLogged => _httpContext.HttpContext.User.Identity.IsAuthenticated;

        public string UserName => _httpContext.HttpContext.User.Claims.FirstOrDefault(t => t.Type == "Username").Value;

        public Guid? UserId => Guid.Parse(_httpContext.HttpContext.User.Claims.FirstOrDefault(t => t.Type == "Id").Value);

        public Guid? EnterpriseId => Guid.Parse(_httpContext.HttpContext.User.Claims.FirstOrDefault(t => t.Type == "EnterpriseId").Value);

        public string EnterpriseName => _httpContext.HttpContext.User.Claims.FirstOrDefault(t => t.Type == "EnterpriseName").Value;

        public Guid? StoreId => Guid.Parse(_httpContext.HttpContext.User.Claims.FirstOrDefault(t => t.Type == "SroreId").Value);
        public string StoreName => _httpContext.HttpContext.User.Claims.FirstOrDefault(t => t.Type == "StoreName").Value;
        public Guid? ProfileId => Guid.Parse(_httpContext.HttpContext.User.Claims.FirstOrDefault(t => t.Type == "ProfileId").Value);
        public string ProfileName => _httpContext.HttpContext.User.Claims.FirstOrDefault(t => t.Type == "ProfileName").Value;

        public Guid? StoreMoneyId => throw new NotImplementedException();
          
        public string StoreMoneyName => throw new NotImplementedException();

        public Guid? MembershipId => Guid.Parse(_httpContext.HttpContext.User.Claims.FirstOrDefault(t => t.Type == "MembershipId").Value);
    }
}
