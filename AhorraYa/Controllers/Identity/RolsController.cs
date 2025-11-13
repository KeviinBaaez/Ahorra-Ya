using AhorraYa.Application.Dtos.Identity.Rols;
using AhorraYa.Entities.MicrosoftIdentity;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AhorraYa.WebApi.Controllers.Identity
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class RolsController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<RolsController> _logger;
        private readonly IMapper _mapper;

        public RolsController(RoleManager<Role> roleManager, 
            ILogger<RolsController> logger, 
            IMapper mapper)
        {
            _roleManager = roleManager;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var rols = _mapper.Map<IList<RolResponseDto>>(_roleManager.Roles.ToList());
                if(rols.Count > 0)
                {
                    return Ok(rols);
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

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(Guid? id)
        {
            if (id.HasValue)
            {
                try
                {
                    var role = _roleManager.FindByIdAsync(id.Value.ToString());
                    if(role is null)
                    {
                        return NotFound();
                    }
                    return Ok(_mapper.Map<RolResponseDto>(role));
                }
                catch (Exception ex)
                {
                    return Conflict();
                }
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(RolRequestDto rolRequestDto)
        {
            if(ModelState.IsValid)
            {
                var userId = Guid.Parse(User.FindFirst("Id")?.Value);
                try
                {
                    var role = _mapper.Map<Role>(rolRequestDto);
                    role.Id = Guid.NewGuid();
                    var result = _roleManager.CreateAsync(role).Result;
                    if (result.Succeeded) 
                    {
                        return Ok(role.Id);
                    }
                    return Problem(detail: result.Errors.First().Description, 
                        instance: role.Name,
                        statusCode: StatusCodes.Status409Conflict);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                return BadRequest("The data is invalid");
            }
        }

        [HttpPut]
        [Route("Update")]
        public IActionResult Update([FromBody] RolRequestDto rolRequestDto, [FromQuery] Guid id)
        {
            if (ModelState.IsValid)
            {
                var userId = Guid.Parse(User.FindFirst("Id")?.Value);
                try
                {
                    var role = _mapper.Map<Role>(rolRequestDto);
                    role.Id = id;
                    var result = _roleManager.UpdateAsync(role).Result;
                    if (result.Succeeded)
                    {
                        return Ok(role.Id);
                    }
                    return Problem(detail: result.Errors.First().Description, instance: role.Name, statusCode: StatusCodes.Status409Conflict);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                return BadRequest("The data is invalid");
            }
        }
    }
}
