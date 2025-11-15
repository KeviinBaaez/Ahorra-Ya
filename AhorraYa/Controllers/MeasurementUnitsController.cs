using AhorraYa.Application.Dtos.MeasurementUnit;
using AhorraYa.Application.Interfaces;
using AhorraYa.Entities;
using AhorraYa.Exceptions;
using AhorraYa.Exceptions.ExceptionsForId;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AhorraYa.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
            catch (ExceptionByServiceConnection ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (AutoMapperMappingException)
            {
                throw new ExceptionMappingError();
            }
            catch (ExceptionMappingError ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred");
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
                return Ok(_mapper.Map<MeasurementUnitResponseDto>(measurementunit));
            }
            catch (AutoMapperMappingException)
            {
                throw new ExceptionMappingError();
            }
            catch (ExceptionMappingError ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (ExceptionIdNotFound ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (ExceptionIdNotZero ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(MeasurementUnitRequestDto measurementunitRequestDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (measurementunitRequestDto.Id != 0)
                    {
                        throw new ExceptionIdNotZero(typeof(MeasurementUnit), measurementunitRequestDto.Id.ToString());
                    }

                    var measurement = _mapper.Map<MeasurementUnit>(measurementunitRequestDto);
                    _measurementunit.Save(measurement);
                    return Ok(measurement.Id);
                }
                catch (AutoMapperMappingException)
                {
                    throw new ExceptionRequestMappingError(); //No pudo mapear del Request al objeto local.
                }
                catch (ExceptionRequestMappingError ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (ExceptionIdNotZero ex) //El Id es distinto a 0.
                {
                    return BadRequest(ex.Message);
                }
                catch (ExceptionAlreadyExist ex) //Ya existe una category con el mismo nombre.
                {
                    return StatusCode(500, ex.Message);
                }
                catch (Exception)
                {
                    return StatusCode(500, "An unexpected error occurred");
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

                    measurementunitBack = _mapper.Map<MeasurementUnit>(measurementunitRequestDto);
                    _measurementunit.Save(measurementunitBack);

                    var response = _mapper.Map<MeasurementUnitResponseDto>(measurementunitBack);
                    return Ok(response);
                }
                catch (AutoMapperMappingException)
                {
                    throw new ExceptionRequestMappingError();
                }
                catch (ExceptionRequestMappingError ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (ExceptionIdNotFound ex)
                {
                    return StatusCode(500, ex.Message);
                }
                catch (ExceptionIdNotZero ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (ExceptionAlreadyExist ex)
                {
                    return StatusCode(500, ex.Message);
                }
                catch (Exception)
                {
                    return StatusCode(500, "An unexpected error occurred");
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

                    _measurementunit.RemoveById(measurementunitBack.Id);
                    return Ok();
                }
                catch (ExceptionIdNotZero ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (ExceptionIdNotFound ex)
                {
                    return StatusCode(500, ex.Message);
                }
                catch (Exception)
                {
                    return StatusCode(500, "An unexpected error occurred");
                }
            }
            return BadRequest();
        }
    }
}
