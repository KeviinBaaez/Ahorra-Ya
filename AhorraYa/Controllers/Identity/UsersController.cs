using AhorraYa.Application.Dtos.Identity.User;
using AhorraYa.Entities.MicrosoftIdentity;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AhorraYa.WebApi.Controllers.Identity
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<UsersController> _logger;
        private readonly IMapper _mapper;

        public UsersController(RoleManager<Role> roleManager,
            UserManager<User> userManager,
            ILogger<UsersController> logger,
            IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("AddRoleToUser")]
        public async Task<IActionResult> Save(string userId, string roleId)
        {
            try
            {
                var user = _userManager.FindByIdAsync(userId).Result;
                var role = _roleManager.FindByIdAsync(roleId).Result;
                if (user is not null && role is not null)
                {
                    var status = await _userManager.AddToRoleAsync(user, role.Name);
                    if (status.Succeeded)
                    {
                        return Ok(new { user = user.UserName, rol = role.Name });
                    }
                }
                return BadRequest(new { userId = userId, roleId = roleId });
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Este método fue creado para poder asignarle roles a los usuarios,
        //como no sabia el Id de cada usuario, cree este método que solo lo debería ver el propietario.
        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = _userManager.Users.ToList();
                var userNewList = new List<UserResponseDto>();
                if (users.Count > 0)
                {
                    foreach (var item in users)
                    {
                        var newUser = new UserResponseDto()
                        {
                            Id = item.Id.ToString(),
                            Name = item.Name,
                            UserName = item.UserName ?? "Usuario Sin Nombre",
                            Email = item.Email ?? "Usuario Sin Email"
                        };
                        userNewList.Add(newUser);
                    }
                    return Ok(userNewList);
                }
                else
                {
                    return NotFound("No records were found.");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
