using AhorraYa.Application.Dtos.Location;
using AhorraYa.Application.Interfaces;
using AhorraYa.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AhorraYa.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : Controller
    {
        private readonly ILogger<LocationController> _logger;
        private readonly IApplication<Location> _location;
        private readonly IMapper _mapper;

        public LocationController(ILogger<LocationController> logger,
            IApplication<Location> location,
            IMapper mapper)
        {
            _logger = logger;
            _location = location;
            _mapper = mapper;
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IList<LocationResponseDto>>(_location.GetAll()));
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            Location location = _location.GetById(id.Value);
            if (location is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<LocationResponseDto>(location));
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(LocationRequestDto locationRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var location = _mapper.Map<Location>(locationRequestDto);
            _location.Save(location);
            return Ok(location.Id);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(int? id, LocationRequestDto locationRequestDto)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Location locationBack = _location.GetById(id.Value);
            if (locationBack is null)
            {
                return NotFound();
            }
            locationBack = _mapper.Map<Location>(locationRequestDto);
            _location.Save(locationBack);
            return Ok();
        }

        [HttpDelete("Remove")]
        public async Task<IActionResult> Remove(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Location locationBack = _location.GetById(id.Value);
            if (locationBack is null)
            {
                return NotFound();
            }
            _location.RemoveById(locationBack.Id);
            return Ok();
        }
    }
}
