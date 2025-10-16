using AhorraYa.Application.Dtos.Category;
using AhorraYa.Application.Interfaces;
using AhorraYa.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AhorraYa.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {

        private readonly ILogger<CategoryController> _logger;
        private readonly IApplication<Category> _category;
        private readonly IMapper _mapper;

        public CategoryController(ILogger<CategoryController> logger,
            IApplication<Category> category,
            IMapper mapper)
        {
            _logger = logger;
            _category = category;
            _mapper = mapper;
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_mapper.Map<IList<CategoryResponseDto>>(_category.GetAll()));
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            Category category = _category.GetById(id.Value);
            if (category is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CategoryResponseDto>(category));
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CategoryRequestDto categoryRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var category = _mapper.Map<Category>(categoryRequestDto);
            _category.Save(category);
            return Ok(category.Id);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(int? id, CategoryRequestDto categoryRequestDto)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Category categoryBack = _category.GetById(id.Value);
            if (categoryBack is null)
            {
                return NotFound();
            }
            categoryBack = _mapper.Map<Category>(categoryRequestDto);
            _category.Save(categoryBack);
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
            Category categoryBack = _category.GetById(id.Value);
            if (categoryBack is null)
            {
                return NotFound();
            }
            _category.RemoveById(categoryBack.Id);
            return Ok();
        }

    }
}
