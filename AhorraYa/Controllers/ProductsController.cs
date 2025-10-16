using AhorraYa.Application.Dtos.Product;
using AhorraYa.Application.Interfaces;
using AhorraYa.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
namespace AhorraYa.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
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
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IList<ProductResponseDto>>(_product.GetAll()));   
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            Product product = _product.GetById(id.Value);
            if(product is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ProductResponseDto>(product));
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(ProductRequestDto productRequestDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var product = _mapper.Map<Product>(productRequestDto);
            _product.Save(product);
            return Ok(product.Id);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(int? id, ProductRequestDto productRequestDto)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Product productBack = _product.GetById(id.Value);
            if(productBack is null)
            {
                return NotFound();
            }
            productBack = _mapper.Map<Product>(productRequestDto);
            _product.Save(productBack);
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
            Product productBack = _product.GetById(id.Value);
            if (productBack is null)
            {
                return NotFound();
            }
            _product.RemoveById(productBack.Id);
            return Ok();
        }

    }
}
