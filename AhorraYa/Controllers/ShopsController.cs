using AhorraYa.Application.Dtos.Shop;
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
    public class ShopsController : ControllerBase
    {
        private readonly ILogger<ShopsController> _logger;
        private readonly IApplication<Shop> _shop;
        private readonly IMapper _mapper;

        public ShopsController(ILogger<ShopsController> logger,
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
            try
            {
                var shops = _mapper.Map<IList<ShopResponseDto>>(_shop.GetAll());
                if (shops.Count > 0)
                {
                    return Ok(shops);
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
                Shop shop = _shop.GetById(id.Value);
                if (shop is null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<ShopResponseDto>(shop));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(ShopRequestDto shopRequestDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var shop = _mapper.Map<Shop>(shopRequestDto);
                    _shop.Save(shop);
                    return Ok(shop.Id);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return BadRequest();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(int? id, ShopRequestDto shopRequestDto)
        {
            if (ModelState.IsValid && id.HasValue)
            {
                try
                {
                    Shop shopBack = _shop.GetById(id.Value);
                    if (shopBack is null)
                    {
                        return NotFound();
                    }
                    shopBack = _mapper.Map<Shop>(shopRequestDto);
                    _shop.Save(shopBack);

                    var response = _mapper.Map<ShopResponseDto>(shopBack);
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
                    Shop shopBack = _shop.GetById(id.Value);
                    if (shopBack is null)
                    {
                        return NotFound();
                    }
                    _shop.RemoveById(shopBack.Id);
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
