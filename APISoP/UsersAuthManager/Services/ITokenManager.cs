using APISoP.CrossCutting.Entities;
using APISoP.Models;
using APISoP.UsersAuthManager.Models;
using APISoP.UsersAuthManager.Responses; 
using System.Threading.Tasks;

namespace APISoP.UsersAuthManager.Services
{
    public interface ITokenManager
    {
        Task<ResultTokensGenerated> JwtGenerateToken(User user);
        Task<ResultTokensGenerated> RefreshTokenJWT(TokenRequest tokenRequest);
    }
}
