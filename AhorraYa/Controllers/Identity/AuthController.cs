using AhorraYa.Application.Dtos.Identity.User;
using AhorraYa.Application.Dtos.Login;
using AhorraYa.Entities.MicrosoftIdentity;
using AhorraYa.Services.Interfaces;
using AhorraYa.WebApi.Configurations;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AhorraYa.WebApi.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AuthController> _logger;
        private readonly IServiceTokenHandler _serviceTokenHandler;
        private readonly IMapper _mapper;

        public AuthController(UserManager<User> userManager,
              ILogger<AuthController> logger,
              IServiceTokenHandler serviceTokenHandler,
              IMapper mapper)
        {
            _userManager = userManager;
            _logger = logger;
            _serviceTokenHandler = serviceTokenHandler;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequestDto user)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var userExist = await _userManager.FindByEmailAsync(user.Email);
                    if (userExist != null)
                    {
                        return BadRequest($"A user with the email address already exists.\nEmail: {user.Email}");
                    }
                    var newUser = await _userManager.CreateAsync(new User()
                    {
                        Name = user.UserName,
                        UserName = user.UserName,
                        Email = user.Email
                    }, user.Password);
                    if (newUser.Succeeded)
                    {
                        var newUserResponse = _mapper.Map<UserRegisterResponseDto>(user);
                        return Ok(newUserResponse);
                    }
                    else
                    {
                        return BadRequest(newUser.Errors.Select(e => e.Description).ToList());
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return BadRequest("The data is invalid");
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserRequestDto userLogin)
        {
            if (ModelState.IsValid)
            {
                User existUser = null;
                if (string.IsNullOrEmpty(userLogin.Email) || userLogin.Email == "string")
                {
                    existUser = await _userManager.FindByNameAsync(userLogin.UserName);
                }
                else
                {
                    existUser = await _userManager.FindByEmailAsync(userLogin.Email);
                }
                    
                if (existUser != null)
                {
                    var isCorrect = await _userManager.CheckPasswordAsync(existUser, userLogin.Password);
                    if (isCorrect)
                    {
                        try
                        {
                            var parameters = new TokenParameters()
                            {
                                Id = existUser.Id.ToString(),
                                PasswordHash = existUser.PasswordHash,
                                UserName = existUser.UserName,
                                Email = existUser.Email
                            };
                            var jwt = _serviceTokenHandler.GenerateJwtTokens(parameters);
                            return Ok(new LoginUserResponseDto()
                            {
                                Login = true,
                                Token = jwt,
                                UserName = existUser.UserName,
                                Mail = existUser.Email
                            });
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                }
            }
            return BadRequest(new LoginUserResponseDto()
            {
                Login = false,
                Errores = new List<string>()
                    {
                       "Incorrect username or password"
                    }
            });
        }
    }
}
