using Microsoft.EntityFrameworkCore;
using StandardLibrary;
using WebAPIEcommerce.Data.DataContext;
using WebAPIEcommerce.Interfaces;
using WebAPIEcommerce.Models.Entities;

namespace WebAPIEcommerce.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDto>> GetCategories()
        {
            return await _context.Categories
                .Select(c => new CategoryDto
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    ParentId = c.ParentId,
                }).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetSubCategories(int parentId)
        {
            return await _context.Categories.Where(c => c.ParentId == parentId).ToListAsync();
        }

        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories.Include(c => c.SubCategories).ToListAsync();
            return MapCategoriesToDTO(categories);
        }

        private List<CategoryDto> MapCategoriesToDTO(List<Category> categories)
        {
            return categories.Select(category => new CategoryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                SubCategories = MapCategoriesToDTO(category.SubCategories)
            }).ToList();
        }

    }
    
}
