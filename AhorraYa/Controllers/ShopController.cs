using AhorraYa.Application.Dtos.Shop;
using AhorraYa.Application.Interfaces;
using AhorraYa.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AhorraYa.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : Controller
    {
        private readonly ILogger<ShopController> _logger;
        private readonly IApplication<Shop> _shop;
        private readonly IMapper _mapper;

        public ShopController(ILogger<ShopController> logger,
            IApplication<Shop> shop,
            IMapper mapper)
        {
            _logger = logger;
            _shop = shop;
            _mapper = mapper;
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IList<ShopResponseDto>>(_shop.GetAll()));
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            Shop shop = _shop.GetById(id.Value);
            if (shop is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ShopResponseDto>(shop));
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(ShopRequestDto shopRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var shop = _mapper.Map<Shop>(shopRequestDto);
            _shop.Save(shop);
            return Ok(shop.Id);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(int? id, ShopRequestDto shopRequestDto)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Shop shopBack = _shop.GetById(id.Value);
            if (shopBack is null)
            {
                return NotFound();
            }
            shopBack = _mapper.Map<Shop>(shopRequestDto);
            _shop.Save(shopBack);
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
            Shop shopBack = _shop.GetById(id.Value);
            if (shopBack is null)
            {
                return NotFound();
            }
            _shop.RemoveById(shopBack.Id);
            return Ok();
        }
    }
}
