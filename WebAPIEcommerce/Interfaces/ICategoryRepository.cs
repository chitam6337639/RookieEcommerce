using StandardLibrary;
using WebAPIEcommerce.Models.Entities;

namespace WebAPIEcommerce.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<CategoryDto>> GetCategories();
        Task<IEnumerable<Category>> GetSubCategories(int parentId);
        Task<List<CategoryDto>> GetAllCategoriesAsync();


    }
}
