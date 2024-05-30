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
			var filteredCategories = categories.Where(c => c.ParentId == null).ToList(); 
			var mappedCategories = _categoryRepository.MapCategoriesToDTO(filteredCategories);
			return Ok(mappedCategories);
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
		public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDto updateCategoryDto)
		{
			if (updateCategoryDto == null)
			{
				return BadRequest();
			}

			var categoryToUpdate = await _categoryRepository.GetAllCategoriesAsync();
			var existingCategory = categoryToUpdate.FirstOrDefault(c => c.CategoryId == id);
			if (existingCategory == null)
			{
				return NotFound();
			}

			existingCategory.CategoryName = updateCategoryDto.CategoryName;
			existingCategory.CategoryDescription = updateCategoryDto.CategoryDescription;

			var updatedCategory = await _categoryRepository.UpdateCategory(id, updateCategoryDto);
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

		[HttpGet("{id}/details")]
		public async Task<IActionResult> GetCategoryDetail(int id)
		{
			var categoryDetail = await _categoryRepository.GetCategoryDetailAsync(id);
			if (categoryDetail == null)
			{
				return NotFound();
			}
			return Ok(categoryDetail);
		}
	}
}
