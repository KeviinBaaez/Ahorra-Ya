using AhorraYa.Application.Dtos.MeasurementUnit;
using AhorraYa.Application.Interfaces;
using AhorraYa.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AhorraYa.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementUnitsController : ControllerBase
    {
        private readonly ILogger<MeasurementUnitsController> _logger;
        private readonly IApplication<MeasurementUnit> _measurementunit;
        private readonly IMapper _mapper;

        public MeasurementUnitsController(ILogger<MeasurementUnitsController> logger,
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
            try
            {
                var list = _mapper.Map<IList<MeasurementUnitResponseDto>>(_measurementunit.GetAll());
                if (list.Count > 0)
                {
                    return Ok(list);
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
                MeasurementUnit measurementunit = _measurementunit.GetById(id.Value);
                if (measurementunit is null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<MeasurementUnitResponseDto>(measurementunit));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(MeasurementUnitRequestDto measurementunitRequestDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var measurementunit = _mapper.Map<MeasurementUnit>(measurementunitRequestDto);
                    _measurementunit.Save(measurementunit);
                    return Ok(measurementunit.Id);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return BadRequest();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(int? id, MeasurementUnitRequestDto measurementunitRequestDto)
        {
            if (ModelState.IsValid && id.HasValue)
            {
                try
                {
                    MeasurementUnit measurementunitBack = _measurementunit.GetById(id.Value);
                    if (measurementunitBack is null)
                    {
                        return NotFound();
                    }
                    measurementunitBack = _mapper.Map<MeasurementUnit>(measurementunitRequestDto);
                    _measurementunit.Save(measurementunitBack);

                    var response = _mapper.Map<MeasurementUnitResponseDto>(measurementunitBack);
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
                    MeasurementUnit measurementunitBack = _measurementunit.GetById(id.Value);
                    if (measurementunitBack is null)
                    {
                        return NotFound();
                    }
                    _measurementunit.RemoveById(measurementunitBack.Id);
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
