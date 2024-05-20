using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StandardLibrary.Category;
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



		[HttpGet("all")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            var filteredCategories = categories.Where(c => c.SubCategories.Any()).ToList();
            return Ok(filteredCategories);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest();
            }
            var createdCategory = await _categoryRepository.CreateCategory(categoryDto);
            return CreatedAtAction(nameof(GetAllCategories), new { id = createdCategory.CategoryId }, createdCategory);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CreateCategoryDto createCategoryDto)
        {
            if (createCategoryDto == null || id != createCategoryDto.CategoryId)
            {
                return BadRequest();
            }

            var existingCategory = await _categoryRepository.GetAllCategoriesAsync();
            var categoryToUpdate = existingCategory.FirstOrDefault(c => c.CategoryId == id);
            if (categoryToUpdate == null)
            {
                return NotFound();
            }
            categoryToUpdate.CategoryName = createCategoryDto.CategoryName;
            categoryToUpdate.ParentId = createCategoryDto.ParentId;

            var updatedCategory = await _categoryRepository.UpdateCategory(id, createCategoryDto);
            if (updatedCategory == null)
            {
                return NotFound();
            }
            return Ok(updatedCategory);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryRepository.DeleteCategory(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
