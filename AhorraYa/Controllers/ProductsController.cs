using AhorraYa.Application.Dtos.Product;
using AhorraYa.Application.Interfaces;
using AhorraYa.Entities;
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
        private readonly IMapper _mapper;

        public ProductsController(ILogger<ProductsController> logger,
            IApplication<Product> product,
            IMapper mapper)
        {
            _logger = logger;
            _product = product;
            _mapper = mapper;
        }

        [HttpGet("All")]
        [Authorize(Roles = "Viewer")]
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
                Product product = _product.GetById(id.Value);
                if (product is null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<ProductResponseDto>(product));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("Create")]
        [Authorize(Roles = "")]
        public async Task<IActionResult> Create(ProductRequestDto productRequestDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var product = _mapper.Map<Product>(productRequestDto);
                    _product.Save(product);
                    return Ok(product.Id);
                }
                catch (Exception)
                {

                    throw;
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
                    Product productBack = _product.GetById(id.Value);
                    if (productBack is null)
                    {
                        return NotFound();
                    }
                    productBack = _mapper.Map<Product>(productRequestDto);
                    _product.Save(productBack);

                    var response = _mapper.Map<ProductResponseDto>(productBack);
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
                    Product productBack = _product.GetById(id.Value);
                    if (productBack is null)
                    {
                        return NotFound();
                    }
                    _product.RemoveById(productBack.Id);
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
