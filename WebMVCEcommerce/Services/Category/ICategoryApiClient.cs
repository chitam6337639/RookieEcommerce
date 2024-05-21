using StandardLibrary;
using StandardLibrary.Category;

namespace WebMVCEcommerce.Services.Category
{
	public interface ICategoryApiClient
	{
		Task<List<CategoryDto>> GetCategories();
		Task<CategoryDetailDto> GetCategoryDetail(int categoryId);

	}
}
