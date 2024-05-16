using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StandardLibrary;
using WebAPIEcommerce.Data.DataContext;
using WebAPIEcommerce.Interfaces;
using WebAPIEcommerce.Models.Entities;

namespace WebAPIEcommerce.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            var categories = await _categoryRepository.GetCategories();
            return Ok(categories);
        }

        [HttpGet("subcategories/{parentId}")]
        public async Task<ActionResult<List<CategoryDto>>> GetSubCategories(int parentId)
        {
            var subCategories = await _categoryRepository.GetSubCategories(parentId);
            if (!subCategories.Any())
            {
                return NotFound();
            }
            var result = subCategories.Select(c => new CategoryDto
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            }).ToList();
            return Ok(result);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            var filteredCategories = categories.Where(c => c.SubCategories.Any()).ToList();
            return Ok(filteredCategories);
        }
    }
}
