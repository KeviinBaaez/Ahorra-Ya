using AhorraYa.Application.Dtos.Product;
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
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IApplication<Product> _product;
        private readonly IApplication<Brand> _brand;
        private readonly IApplication<MeasurementUnit> _measurement;
        private readonly IApplication<Category> _category;
        private readonly IMapper _mapper;

        public ProductsController(ILogger<ProductsController> logger,
            IApplication<Product> product, 
            IApplication<Brand> brand,
            IApplication<MeasurementUnit> measurement,
            IApplication<Category> category,
            IMapper mapper)
        {
            _logger = logger;
            _product = product;
            _brand = brand;
            _measurement = measurement;
            _category = category;
            _mapper = mapper;
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = _mapper.Map<IList<ProductResponseDto>>(_product.GetAll());
                if (products.Count > 0)
                {
                    return Ok(products);
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
                Product product = _product.GetById(id.Value);
                return Ok(_mapper.Map<ProductResponseDto>(product));
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
        public async Task<IActionResult> Create(ProductRequestDto productRequestDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if(productRequestDto.Id != 0)
                    {
                        throw new ExceptionIdNotZero(typeof(Product), productRequestDto.Id.ToString()); 
                    }


                    Brand brand = _brand.GetById(productRequestDto.BrandId);
                    MeasurementUnit unit = _measurement.GetById(productRequestDto.UnitId);
                    Category category = _category.GetById(productRequestDto.CategoryId);

                    var product = _mapper.Map<Product>(productRequestDto);
                    _product.Save(product);
                    return Ok(product.Id);
                }
                catch (AutoMapperMappingException)
                {
                    throw new ExceptionRequestMappingError();
                }
                catch (ExceptionRequestMappingError ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (ExceptionIdNotZero ex) //El Id es distinto a 0.
                {
                    return BadRequest(ex.Message);
                }
                catch(ExceptionIdNotFound ex)
                {
                    return StatusCode(500, ex.Message);
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
        public async Task<IActionResult> Update(int? id, ProductRequestDto productRequestDto)
        {
            if (ModelState.IsValid && id.HasValue)
            {
                try
                {
                    Brand brand = _brand.GetById(productRequestDto.BrandId);
                    MeasurementUnit unit = _measurement.GetById(productRequestDto.UnitId);
                    Category category = _category.GetById(productRequestDto.CategoryId);

                    Product productBack = _product.GetById(id.Value);

                    productBack = _mapper.Map<Product>(productRequestDto);
                    productBack.Brand = brand;
                    productBack.MeasurementUnit = unit;
                    productBack.Category = category;

                    _product.Save(productBack);

                    var response = _mapper.Map<ProductResponseDto>(productBack);
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
                    Product productBack = _product.GetById(id.Value);
                    _product.RemoveById(productBack.Id);
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
