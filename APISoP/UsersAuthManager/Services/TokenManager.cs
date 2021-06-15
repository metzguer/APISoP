using APiSoP.Domain.Contracts.CRUD;
using APISoP.Config.JWT;
using APISoP.CrossCutting.Entities;
using APISoP.CrossCutting.Utils;
using APISoP.Models;
using APISoP.UsersAuthManager.Models;
using APISoP.UsersAuthManager.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace APISoP.UsersAuthManager.Services
{
    public class TokenManager : ITokenManager
    {
        private readonly JwtConfig _jwtConfig;
        private readonly APIAuthDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUsersService _usersService;
        private readonly IEnterpriseService _enterpriseService;
        private readonly IStoreService _storeService;
        public TokenManager(IOptionsMonitor<JwtConfig> jwtConfig, APIAuthDbContext context, UserManager<ApplicationUser> userManager, 
            IUsersService usersService, IEnterpriseService enterpriseService, IStoreService storeService)
        {
            _context = context;
            _jwtConfig = jwtConfig.CurrentValue;
            _userManager = userManager;
            _usersService = usersService;
            _storeService = storeService;
            _enterpriseService = enterpriseService;
        }

        public async Task<ResultTokensGenerated> JwtGenerateToken(User user)
        {
            var response = new ResultTokensGenerated();
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] {
                    new Claim("Id", user.UserId.ToString()),
                    new Claim("Username", user.Username),
                    new Claim("TypeUser", user.TypeUser.ToString()),
                    new Claim("ProfileId", user.ProfileId.ToString()),
                    new Claim("ProfileName", user.Profile.Name),
                    new Claim("EnterpriseId", user.Store.EnterpriseId.ToString()),
                    new Claim("EnterpriseName", user.Store.Enterprise.Name),
                    new Claim("SroreId", user.StoreId.ToString()),
                    new Claim("StoreName", user.Store.StoreName),
                    new Claim("MembershipId", user.Store.Enterprise.MembershipId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                    Expires = DateTime.UtcNow.AddMinutes(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);

                var refreshToken = new RefreshToken
                {
                    JwtId = token.Id,
                    isUsed = false,
                    isRevoked = false,
                    UserId = user.UserId.ToString(),
                    EnterpriseId = user.Store.EnterpriseId.ToString(),
                    StoreId = user.StoreId.ToString(),
                    AddedDate = DateTime.UtcNow,
                    ExpireDate = DateTime.UtcNow,
                    Token = $"{await RandomString.Generate(35)}{Guid.NewGuid()}"
                };

                await _context.RefreshTokens.AddAsync(refreshToken);
                await _context.SaveChangesAsync();
                 
                response.Token = jwtToken;
                response.RefreshToken = refreshToken.Token;
                response.Success = true; 
                
            }
            catch (Exception ex)
            {
                var messageErrors = new List<string>();
                messageErrors.Add($"Exception ocurrida : {ex.Message}"); 
                response.Success = false;
                response.Errors = messageErrors;
            }

            return await Task.Run(() => response);
        }

        public async Task<ResultTokensGenerated> RefreshTokenJWT(TokenRequest tokenRequest)
        {
            var result = new ResultTokensGenerated();
            result.Errors = new List<string>();

            var checkToken = await VerifyToken(tokenRequest);
            if (checkToken.IsValid)
            {
                var userApp = await _context.RefreshTokens.FirstOrDefaultAsync( x => x.Token == tokenRequest.RefreshToken);

                if (userApp == null) {
                    result.Token = "";
                    result.RefreshToken = "";
                    result.Success = false;
                    result.Errors.Add("El usuario no existe");
                    return result;
                }

                var searchUser = await _usersService.GetUserForLogin(Guid.Parse(userApp.UserId));

                if (!searchUser.Success)
                {
                    result.Token = "";
                    result.RefreshToken = "";
                    result.Success = false;
                    result.Errors.Add("El usuario no existe");
                    return result;
                }

                var store = await _storeService.GetById(Guid.Parse(userApp.StoreId));
                var enterprise = await _enterpriseService.GetById(Guid.Parse(userApp.EnterpriseId));

                searchUser.Result.Store = store.Result;
                searchUser.Result.StoreId = store.Result.StoreId;
                searchUser.Result.Store.Enterprise = enterprise.Result;
                searchUser.Result.Store.Enterprise.EnterpriseId = enterprise.Result.EnterpriseId;
                //

                var resultNewTokenCreated = await JwtGenerateToken(searchUser.Result);

                if (resultNewTokenCreated.Success) {
                    result = resultNewTokenCreated;

                } else {
                    result.Token = "";
                    result.RefreshToken = "";
                    result.Success = false;
                    result.Errors = resultNewTokenCreated.Errors;
                }
            }
            else
            {
                result.Token = "";
                result.RefreshToken = "";
                result.Success = false;
                result.Errors = checkToken.Errors;
            }

            return result;
        }

        private async Task<VerifiedTokenResult> VerifyToken(TokenRequest tokenRequest)
        {
            var response = new VerifiedTokenResult();
            response.Errors = new List<string>();
             
            try
            {
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var parm = new ParametersTokens(_jwtConfig.Secret).TokenValidationParameters;
                parm.ValidateLifetime = false;
                //validation one - format token
                var tokenInVerification = jwtTokenHandler.ValidateToken(tokenRequest.Token, parm, out SecurityToken validatedToken);

                //validation two - encription
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase); 
                    if (!result)
                    {
                        response.IsValid = false;
                        response.Errors.Add("No fue posible validar el token");
                        return await Task.Run(() => response);
                    } 

                    //validation tree - expire time
                    var utcExpireDay = long.Parse(tokenInVerification.Claims.FirstOrDefault(t => t.Type == JwtRegisteredClaimNames.Exp).Value);
                    var expireDate = await UnixTimeStampToDateTime(utcExpireDay); 
                    if (expireDate > DateTime.UtcNow.ToLocalTime())
                    {
                        response.IsValid = false;
                        response.Errors.Add("El token no ha expirado");
                        return await Task.Run(() => response);
                    } 

                    //validation four - token existence
                    var storedToken = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == tokenRequest.RefreshToken); 
                    if (storedToken == null)
                    {
                        response.IsValid = false;
                        response.Errors.Add("El token no existe");
                        return await Task.Run(() => response); 
                    }

                    //valiation five - is used or not
                    if (storedToken.isUsed)
                    {
                        response.IsValid = false;
                        response.Errors.Add("El token ya ha sido utilizado");
                        return await Task.Run(() => response);
                    }
                     
                    //validation six - is revoke or not
                    if (storedToken.isRevoked)
                    {
                        response.IsValid = false;
                        response.Errors.Add("El token ya ha sido revocado");
                        return await Task.Run(() => response);
                    } 

                    //validation seven
                    var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                    if (storedToken.JwtId != jti)
                    {
                        response.IsValid = false;
                        response.Errors.Add("Token corrupto");
                        return await Task.Run(() => response);
                    } 

                    //update token
                    storedToken.isUsed = true;
                    _context.RefreshTokens.Update(storedToken);
                    await _context.SaveChangesAsync();
                     
                    response.UserId = storedToken.UserId;
                    response.IsValid = true;

                    return await Task.Run(() => response);
                }
                else {
                    response.IsValid = false;
                    response.Errors.Add("No es un token");
                    return await Task.Run(() => response);
                }
            }
            catch (Exception ex)
            {
                response.IsValid = false; 
                response.Errors.Add("No fue posible validar el token " + ex.Message);
                return await Task.Run(() => response);
            } 
        }

        private async Task<DateTime> UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToLocalTime();
            return await Task.Run(() => dateTimeVal);
        }
    }
}
