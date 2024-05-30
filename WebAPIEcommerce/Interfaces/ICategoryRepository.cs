using StandardLibrary.Category;
using WebAPIEcommerce.Models.Entities;

namespace WebAPIEcommerce.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoriesAsync();
		List<CategoryDto> MapCategoriesToDTO(List<Category> categories);
		Task<CategoryDto> CreateCategory(CreateCategoryDto createCategoryDto);
        Task<CategoryDto> UpdateCategory(int categoryId, UpdateCategoryDto updateCategoryDto);
        Task<bool> DeleteCategory(int categoryId);
		Task<CategoryDetailDto> GetCategoryDetailAsync(int categoryId);
		Task<CategoryWithSubCategoriesDto> GetCategoryWithSubCategoriesAsync(int categoryId);

	}
}
