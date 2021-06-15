using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APiSoP.Services.CustomIdentity
{
    public interface IUserIdentity
    {
        bool UserLogged { get; }
        string UserName { get; }
        Guid? UserId { get; }
        Guid? EnterpriseId { get; }
        string EnterpriseName { get; }
        Guid? StoreId { get; }
        Guid? StoreMoneyId { get; }
        string StoreName { get; }
        string StoreMoneyName { get; }
        Guid? MembershipId { get; }
    }
}
