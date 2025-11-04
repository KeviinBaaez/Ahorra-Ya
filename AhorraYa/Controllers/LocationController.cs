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
            try
            {
                var locations = _mapper.Map<IList<LocationResponseDto>>(_location.GetAll());
                if(locations.Count > 0)
                {
                    return Ok(locations);
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
        public async Task<IActionResult> GetById(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            try
            {
                Location location = _location.GetById(id.Value);

                if (location is null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<LocationResponseDto>(location));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(LocationRequestDto locationRequestDto)
        {
            if (ModelState.IsValid)
            {                
                try
                {
                    var location = _mapper.Map<Location>(locationRequestDto);
                    _location.Save(location);
                    return Ok(location.Id);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return BadRequest();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(int? id, LocationRequestDto locationRequestDto)
        {
            if (ModelState.IsValid && id.HasValue)
            {
                try
                {
                    Location locationBack = _location.GetById(id.Value);
                    if (locationBack is null)
                    {
                        return NotFound();
                    }
                    locationBack = _mapper.Map<Location>(locationRequestDto);
                    _location.Save(locationBack);

                    var response = _mapper.Map<LocationResponseDto>(locationBack);
                    return Ok(response);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return BadRequest();
        }

        [HttpDelete("Remove")]
        public async Task<IActionResult> Remove(int? id)
        {
            if (ModelState.IsValid && id.HasValue)
            {
                try
                {
                    Location locationBack = _location.GetById(id.Value);
                    if (locationBack is null)
                    {
                        return NotFound();
                    }
                    _location.RemoveById(locationBack.Id);
                    return Ok();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return BadRequest();
        }
    }
}
