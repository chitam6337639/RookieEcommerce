using Microsoft.EntityFrameworkCore;
using StandardLibrary;
using StandardLibrary.Category;
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
        public async Task<CategoryDto> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var category = new Category
            {
                CategoryName = createCategoryDto.CategoryName,
                ParentId = createCategoryDto.ParentId
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                ParentId = category.ParentId
            };
        }

        public async Task<CategoryDto> UpdateCategory(int categoryId, CreateCategoryDto createCategoryDto)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
            {
                return null;
            }
            category.CategoryName = createCategoryDto.CategoryName;
            category.ParentId = createCategoryDto.ParentId;

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                ParentId = category.ParentId
            };
        }

        public async Task<bool> DeleteCategory(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
            {
                return false;
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CategoryDetailDto> GetCategoryDetailAsync(int categoryId)
        {
            var category = await _context.Categories
                                          .Include(c => c.Products)
                                          .FirstOrDefaultAsync(c => c.CategoryId == categoryId);
            if (category == null)
            {
                return null;
            }

            return new CategoryDetailDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Products = category.Products.Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    Price = p.Price
                }).ToList()
            };
        }

	}
    
}
