using StandardLibrary;
using StandardLibrary.Category;
using WebAPIEcommerce.Models.Entities;

namespace WebAPIEcommerce.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> CreateCategory(CreateCategoryDto createCategoryDto);
        Task<CategoryDto> UpdateCategory(int categoryId, CreateCategoryDto createCategoryDto);
        Task<bool> DeleteCategory(int categoryId);
		Task<CategoryDetailDto> GetCategoryDetailAsync(int categoryId);

	}
}
