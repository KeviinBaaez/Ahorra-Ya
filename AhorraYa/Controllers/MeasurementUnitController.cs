using AhorraYa.Application.Dtos.MeasurementUnit;
using AhorraYa.Application.Interfaces;
using AhorraYa.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AhorraYa.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementUnitController : Controller
    {
        private readonly ILogger<MeasurementUnitController> _logger;
        private readonly IApplication<MeasurementUnit> _measurementunit;
        private readonly IMapper _mapper;

        public MeasurementUnitController(ILogger<MeasurementUnitController> logger,
            IApplication<MeasurementUnit> measurementunit,
            IMapper mapper)
        {
            _logger = logger;
            _measurementunit = measurementunit;
            _mapper = mapper;
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IList<MeasurementUnitResponseDto>>(_measurementunit.GetAll()));
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            MeasurementUnit measurementunit = _measurementunit.GetById(id.Value);
            if (measurementunit is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<MeasurementUnitResponseDto>(measurementunit));
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(MeasurementUnitRequestDto measurementunitRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var measurementunit = _mapper.Map<MeasurementUnit>(measurementunitRequestDto);
            _measurementunit.Save(measurementunit);
            return Ok(measurementunit.Id);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(int? id, MeasurementUnitRequestDto measurementunitRequestDto)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            MeasurementUnit measurementunitBack = _measurementunit.GetById(id.Value);
            if (measurementunitBack is null)
            {
                return NotFound();
            }
            measurementunitBack = _mapper.Map<MeasurementUnit>(measurementunitRequestDto);
            _measurementunit.Save(measurementunitBack);
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
            MeasurementUnit measurementunitBack = _measurementunit.GetById(id.Value);
            if (measurementunitBack is null)
            {
                return NotFound();
            }
            _measurementunit.RemoveById(measurementunitBack.Id);
            return Ok();
        }
    }
}
