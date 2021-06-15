using APiSoP.Domain.Contracts.CRUD;
using APiSoP.Services.CustomIdentity;
using APISoP.CrossCutting.DTOs.Enterprises;
using APISoP.CrossCutting.Entities;
using APISoP.CrossCutting.Mappers;
using APISoP.CrossCutting.Responses.Operation;
using APISoP.CrossCutting.Types;
using APISoP.Models;
using APISoP.Response;
using APISoP.UsersAuthManager.Models;
using APISoP.UsersAuthManager.Responses;
using APISoP.UsersAuthManager.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APISoP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthenticationUsersController : ControllerBase
    {
        private readonly ITokenManager _tokenManager;
        private readonly APIAuthDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUsersService _usersService;
        private readonly IUserIdentity _userIdentity;
        private readonly IEnterpriseService _enterpriseService;
        private readonly IStoreService _storeService;
        public AuthenticationUsersController(ITokenManager tokenManager,APIAuthDbContext context, 
            UserManager<ApplicationUser> userManager, IUsersService usersService, IUserIdentity userIdentity, IEnterpriseService enterpriseService, IStoreService storeService)
        {
            _tokenManager = tokenManager;
            _context = context;
            _userManager = userManager;
            _usersService = usersService;
            _userIdentity = userIdentity;
            _enterpriseService = enterpriseService;
            _storeService = storeService;
        }

        // POST api/<AuthenticationUsersController>/Register
        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthenticationResult>> Register([FromBody] UserRegister user)
        {
            var response = new AuthenticationResult();
            response.Errors = new List<string>();

            if (ModelState.IsValid) {

                string exist = await Validations(user.Email);
                if (exist != null) {
                    response.Success = false;
                    response.Errors.Add(exist);
                    return BadRequest(response);
                }

                //create user
                var userApp = new ApplicationUser {Email = user.Email, UserName = user.Email}; 
                var userCreated = await _userManager.CreateAsync(userApp, user.Password);

                if (!userCreated.Succeeded)
                {
                    response.Success = false;
                    response.Errors.AddRange(await ErrorsUserCreated(userCreated));
                    return BadRequest(response);
                }

                //create resources for new account 
                var userStore = await _usersService.CreateNewAccount(
                    Guid.Parse(userApp.Id), userApp.UserName, userApp.Email, userApp.UserName, user.EnterpriseName);

                if (!userStore.Success) {
                    response.Success = false;
                    await _userManager.DeleteAsync(userApp);
                    response.Errors.AddRange( userStore.Errors.Select(x => x.Description).ToList() );
                    return BadRequest(response);
                }

                //create token
                ResultTokensGenerated generateToken = await _tokenManager.JwtGenerateToken(userStore.Result);

                if (!generateToken.Success)
                {
                    await _userManager.DeleteAsync(userApp);
                    response.Success = false;
                    await _userManager.DeleteAsync(userApp);
                    response.Errors.Add("Ocurrio un error, intenta nuevamente");
                    return BadRequest(response);
                }

                response.Token = generateToken.Token;
                response.RefreshToken = generateToken.RefreshToken;
                response.Success = true;
                response.State = "Logged";
                response.Message = "Usuario registrado";

                return Ok(response);
            }

            return BadRequest("Revisa los datos ingresadoss");
        }

        // POST api/<AuthenticationUsersController>/Login
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthenticationResult>> Login([FromBody] UserLogin user, string guidEnterprise, string guidStore)
        {
            var response = new AuthenticationResult();
            response.Errors = new List<string>();

            if (ModelState.IsValid)
            {
                var userInternal = await ValidationsLogin(user);

                if (userInternal == null) {
                    response.Errors.Add("Inicio de sesión no valido");
                    return BadRequest(response);
                }
                  
                TypeUser typeUser = userInternal.Result.TypeUser;
                Enterprise enterpriseSelected = new Enterprise();
                Store storeSelected = new Store();

                if (typeUser == TypeUser.Owner) {
                    //select enterprise
                    var enterprises = await _enterpriseService.GetAll( userInternal.Result.Store.Enterprise.MembershipId );
                   
                    if (enterprises.Result != null && enterprises.Result.Count() == 1) {
                        enterpriseSelected = enterprises.Result.FirstOrDefault();
                    }

                    if (enterprises.Result != null && enterprises.Result.Count() > 1 && string.IsNullOrWhiteSpace(guidEnterprise))
                    {
                        var selectResponse = new ResultOperation<IEnumerable<ListEnterpriseDTO>>();
                        selectResponse.Message = "Este usuario tiene mas de una empresa registrada, envia el id de la empresa para iniciar sesión,  &guidEnterprise={guid}";
                        selectResponse.Result = enterprises.Result.Select(x => EnterpriseMapper.GetListEnterprise(x)).ToList();
                        return Ok(selectResponse);
                    }
                     
                    if (enterprises.Result != null && enterprises.Result.Count() > 1 && !string.IsNullOrWhiteSpace(guidEnterprise))
                    {
                        bool idValid = Guid.TryParse(guidEnterprise, out Guid id); 
                        if (!idValid)
                        {
                            response.Errors.Add("Ingresa un id valido");
                            return BadRequest(response);
                        }
                        var enterprise = await _enterpriseService.GetById( Guid.Parse(guidEnterprise) );
                        enterpriseSelected = enterprise.Result;
                    }
                    //end select enterprise

                    //select store enterprise
                    var stores = await _storeService.GetAll(enterpriseSelected.EnterpriseId);

                    if (stores.Result == null || stores.Result.Count() <= 0) {
                        var newStore = new Store {
                            StoreName = enterpriseSelected.Name,
                            IsActive = true,
                            EnterpriseId = enterpriseSelected.EnterpriseId
                        };

                        var result = await _storeService.Add(newStore);
                        storeSelected = result.Result;
                    }

                    if (stores.Result != null && stores.Result.Count() > 1 && string.IsNullOrWhiteSpace(guidStore)) { 
                        var selectResponse = new ResultOperation<IEnumerable<Store>>();
                        selectResponse.Message = "Esta empresa tiene mas de una sucursal/tienda registrada, envia el id de la sucursal/tienda para iniciar sesión, &guidStore={guid}";
                        selectResponse.Result = stores.Result;
                        return Ok(selectResponse); 
                    } 

                    if (stores.Result != null && stores.Result.Count() > 1 && !string.IsNullOrWhiteSpace(guidStore))
                    {
                        bool isValid = Guid.TryParse(guidStore, out Guid idStore);

                        if (!isValid)
                        {
                            response.Errors.Add("Ingresa un id valido");
                            return BadRequest(response);
                        }
                        var store = await _storeService.GetById(Guid.Parse(guidStore));
                        storeSelected = store.Result;
                    }

                    if (stores.Result != null && stores.Result.Count() == 1)
                    {
                        storeSelected = stores.Result.FirstOrDefault();
                    }

                    //end select store enterprise 
                    userInternal.Result.Store = storeSelected;
                    userInternal.Result.Store.EnterpriseId = enterpriseSelected.EnterpriseId;
                    userInternal.Result.Store.Enterprise = enterpriseSelected;
                }

                if (typeUser == TypeUser.Guests) {
                    
                }

                ResultTokensGenerated createdToken = await _tokenManager.JwtGenerateToken(userInternal.Result); 

                if (!createdToken.Success)
                {
                    response.Errors.Add("Inicio de sesión no valido");
                    return BadRequest(response);
                }
                 
                //create resources for new enterprises

                response.Token = createdToken.Token;
                response.RefreshToken = createdToken.RefreshToken;
                response.Success = true;
                response.State = "Logged";
                response.Message = "Inicio de sesión correcto !";

                return Ok(response);
            }
            return BadRequest(response);

           
        }

        [HttpPost]
        [Route("RefreshToken")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthenticationResult>> RefreshToken([FromBody] TokenRequest token)
        {
            var response = new AuthenticationResult();
            response.Errors = new List<string>();

            if (ModelState.IsValid)
            { 
                ResultTokensGenerated tokenVerify = await _tokenManager.RefreshTokenJWT(token);

                if (tokenVerify.Success)
                {
                    response.Token = tokenVerify.Token;
                    response.RefreshToken = tokenVerify.RefreshToken;
                    response.Success = true;
                    response.State = "Logged";
                    response.Message = "Datos de sesión actualizados !";
                }
                else {
                    response.Token = "";
                    response.RefreshToken = "";
                    response.Success = false;
                    response.State = "Failed";
                    response.Message = "Ocurrio un error";
                    response.Errors = tokenVerify.Errors;
                } 
            }

            return (response.Errors != null && response.Errors.Count > 0) ? BadRequest(response) : Ok(response); 
        }

        // GET api/<AuthenticationUsersController>/5

        [HttpGet("Profile")] 
        public async Task<ActionResult> Profile()
        {
            if (!_userIdentity.UserLogged) 
                return Unauthorized("Inicia sesión"); 
  
            var user = await _usersService.GetById( _userIdentity.UserId.Value );
            return Ok(user); 

        }

        private async Task<List<string>> ErrorsUserCreated(IdentityResult isCreated)
        {

            var errors = new List<string>();

            if (isCreated.Errors != null && isCreated.Errors.Count() > 0)
            {
                foreach (var error in isCreated.Errors)
                {
                    if (error.Code == "PasswordTooShort")
                        errors.Add("La contraseña debe tener un minimo de 6 carácteres");
                    if (error.Code == "PasswordRequiresNonAlphanumeric")
                        errors.Add("La contraseña debe tener almenos un carácter especial");
                    if (error.Code == "PasswordRequiresLower")
                        errors.Add("La contraseña debe tener almenos una letra minúscuka a-z");
                    if (error.Code == "PasswordRequiresUpper")
                        errors.Add("La contraseña debe tener almenos una letra mayúscula A-Z");
                    if (error.Code == "PasswordRequiresDigit")
                        errors.Add("La contraseña debe tener almenos un carácter númerico 0-9");
                    if (error.Code == "InvalidUserName")
                        errors.Add("El campo username no debe tener espacios");
                }
            }
            else
            {
                errors.Add("Ocurrio un error durante el registro del usuario");
            }
            return await Task.Run(() => errors);
        }

        private async Task<string> Validations(string email) {

            var existUser = await _userManager.FindByEmailAsync(email);
            if (existUser != null)
            {
                return $"Ya existe un usuario registrado con el email {email}";
            } 
            return null;
        }

        private async Task<ResultOperation<User>> ValidationsLogin(UserLogin user) {

            var existUser = await _userManager.FindByEmailAsync(user.Email);
            if (existUser == null) 
                return null; 

            var isCorrect = await _userManager.CheckPasswordAsync(existUser, user.Password);
            if (!isCorrect) 
                return null;


            var userInternal = await _usersService.GetUserForLogin(Guid.Parse(existUser.Id));

            if (!userInternal.Success)
            {
                return null;
            }

            return userInternal;
        }
    }
}
