using AhorraYa.Application.Dtos.Identity.User;
using AhorraYa.Entities.MicrosoftIdentity;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AhorraYa.WebApi.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<ProductsController> _logger;
        private readonly IMapper _mapper;

        public AuthController(UserManager<User> userManager,
              ILogger<ProductsController> logger,
              IMapper mapper)
        {
            _userManager = userManager;
            _logger = logger;
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
    }
}
