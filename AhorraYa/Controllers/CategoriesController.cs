using AhorraYa.Application.Dtos.Category;
using AhorraYa.Application.Interfaces;
using AhorraYa.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AhorraYa.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly ILogger<CategoriesController> _logger;
        private readonly IApplication<Category> _category;
        private readonly IMapper _mapper;

        public CategoriesController(ILogger<CategoriesController> logger,
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
            try
            {
                var categories = _mapper.Map<IList<CategoryResponseDto>>(_category.GetAll());
                if (categories.Count > 0)
                {
                    return Ok(categories);
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
                Category category = _category.GetById(id.Value);
                if (category is null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<CategoryResponseDto>(category));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CategoryRequestDto categoryRequestDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var category = _mapper.Map<Category>(categoryRequestDto);
                    _category.Save(category);
                    return Ok(category.Id);
                }
                catch (Exception)
                {

                    throw;
                }

            }
            return BadRequest();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(int? id, CategoryRequestDto categoryRequestDto)
        {
            if (ModelState.IsValid && id.HasValue)
            {
                try
                {
                    Category categoryBack = _category.GetById(id.Value);
                    if (categoryBack is null)
                    {
                        return NotFound();
                    }
                    categoryBack = _mapper.Map<Category>(categoryRequestDto);
                    _category.Save(categoryBack);

                    var response = _mapper.Map<CategoryResponseDto>(categoryBack);
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
                    Category categoryBack = _category.GetById(id.Value);
                    if (categoryBack is null)
                    {
                        return NotFound();
                    }
                    _category.RemoveById(categoryBack.Id);
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
