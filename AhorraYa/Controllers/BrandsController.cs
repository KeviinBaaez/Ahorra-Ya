using AhorraYa.Application.Dtos.Brand;
using AhorraYa.Application.Interfaces;
using AhorraYa.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AhorraYa.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : Controller
    {

        private readonly ILogger<BrandsController> _logger;
        private readonly IApplication<Brand> _brand;
        private readonly IMapper _mapper;

        public BrandsController(ILogger<BrandsController> logger,
            IApplication<Brand> brand,
            IMapper mapper)
        {
            _logger = logger;
            _brand = brand;
            _mapper = mapper;
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IList<BrandResponseDto>>(_brand.GetAll()));
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            Brand brand = _brand.GetById(id.Value);
            if (brand is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<BrandResponseDto>(brand));
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(BrandRequestDto brandRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var brand = _mapper.Map<Brand>(brandRequestDto);
            _brand.Save(brand);
            return Ok(brand.Id);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(int? id, BrandRequestDto brandRequestDto)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Brand brandBack = _brand.GetById(id.Value);
            if (brandBack is null)
            {
                return NotFound();
            }
            brandBack = _mapper.Map<Brand>(brandRequestDto);
            _brand.Save(brandBack);
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
            Brand brandBack = _brand.GetById(id.Value);
            if (brandBack is null)
            {
                return NotFound();
            }
            _brand.RemoveById(brandBack.Id);
            return Ok();
        }

    }
}
