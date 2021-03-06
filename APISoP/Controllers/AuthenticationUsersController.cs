using APiSoP.Domain.Contracts.CRUD;
using APiSoP.Services.CustomIdentity;
using APISoP.CrossCutting.Entities;
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

        public AuthenticationUsersController(ITokenManager tokenManager,APIAuthDbContext context, 
            UserManager<ApplicationUser> userManager, IUsersService usersService, IUserIdentity userIdentity)
        {
            _tokenManager = tokenManager;
            _context = context;
            _userManager = userManager;
            _usersService = usersService;
            _userIdentity = userIdentity;
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

                string exist = await Validations(user.Email, user.Username);
                if (exist != null) {
                    response.Success = false;
                    response.Errors.Add(exist);
                    return BadRequest(response);
                }

                //create user
                var userApp = new ApplicationUser {Email = user.Email, UserName = user.Username}; 
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
        public async Task<ActionResult<AuthenticationResult>> Login([FromBody] UserLogin user)
        {
            var response = new AuthenticationResult();
            response.Errors = new List<string>();

            if (ModelState.IsValid)
            { 
                var existUser = await _userManager.FindByEmailAsync(user.Email);
                if (existUser == null)
                {
                    response.Errors.Add("Inicio de sesión no valido");
                    return BadRequest(response);
                } 

                var isCorrect = await _userManager.CheckPasswordAsync(existUser, user.Password);
                if (!isCorrect)
                {
                    response.Errors.Add("Inicio de sesión no valido");
                    return BadRequest(response);
                }

                var userInternal = await _usersService.GetUserForLogin( Guid.Parse(existUser.Id) );
                 
                if (!userInternal.Success)
                {
                    response.Errors.Add("Inicio de sesión no valido");
                    return BadRequest(response);
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

        private async Task<string> Validations(string email, string username) {

            var existUser = await _userManager.FindByEmailAsync(email);
            if (existUser != null)
            {
                return $"Ya existe un usuario registrado con el email {email}";
            }

            var existUsername = await _userManager.FindByNameAsync(username);
            if (existUsername != null)
            {
                return $"Ya existe un usuario registrado con el usuario {username}";
            }

            return null;
        }
    }
}
