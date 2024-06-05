using Microsoft.EntityFrameworkCore;
using StandardLibrary.Category;
using StandardLibrary.Product;
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
		public async Task<List<Category>> GetAllCategoriesAsync()
		{
			var categories = await _context.Categories.Include(c => c.SubCategories).ToListAsync();
			return categories;
		}

		public List<CategoryDto> MapCategoriesToDTO(List<Category> categories)
		{
			return categories.Select(category => new CategoryDto
			{
				CategoryId = category.CategoryId,
				CategoryName = category.CategoryName,
                CategoryDescription = category.CategoryDescription,
                ParentId = category.ParentId,
				SubCategories = MapCategoriesToDTO(category.SubCategories)
			}).ToList();
		}


		public async Task<CategoryDto> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var category = new Category
            {
                CategoryName = createCategoryDto.CategoryName,
                CategoryDescription = createCategoryDto.CategoryDescription,
                ParentId = createCategoryDto.ParentId
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                CategoryDescription = category.CategoryDescription,
                ParentId = category.ParentId
            };
        }

        public async Task<CategoryDto> UpdateCategory(int categoryId, UpdateCategoryDto updateCategoryDto)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
            {
                return null;
            }
            category.CategoryName = updateCategoryDto.CategoryName;
            category.CategoryDescription = updateCategoryDto.CategoryDescription;
            

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                CategoryDescription = category.CategoryDescription,
                ParentId = category.ParentId
            };
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
                CategoryDescription = category.CategoryDescription,
                Products = category.Products.Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    ImageURL = p.ImageURL,
                    Price = p.Price
                }).ToList()
            };
        }
		public async Task<CategoryWithSubCategoriesDto> GetCategoryWithSubCategoriesAsync(int categoryId)
		{
			var category = await _context.Categories
										  .Include(c => c.SubCategories)
										  .ThenInclude(sc => sc.Products) // Include products of subcategories
										  .FirstOrDefaultAsync(c => c.CategoryId == categoryId);
			if (category == null)
			{
				return null;
			}

			return new CategoryWithSubCategoriesDto
			{
				CategoryId = category.CategoryId,
				CategoryName = category.CategoryName,
				CategoryDescription = category.CategoryDescription,
                ParentId = category.ParentId,
				SubCategories = MapCategoriesToDTO(category.SubCategories),
				Products = category.Products.Select(p => new ProductDto
				{
					ProductId = p.ProductId,
					ProductName = p.ProductName,
					ProductDescription = p.ProductDescription,
					Price = p.Price
				}).ToList()
			};
		}

		public async Task<bool> DeleteCategory(int categoryId)
		{
			var category = await _context.Categories
										 .Include(c => c.SubCategories)
										 .FirstOrDefaultAsync(c => c.CategoryId == categoryId);

			if (category == null)
			{
				return false;
			}

			DeleteCategoryRecursive(category);

			await _context.SaveChangesAsync();
			return true;
		}

		private void DeleteCategoryRecursive(Category category)
		{
			foreach (var subCategory in category.SubCategories.ToList())
			{
				DeleteCategoryRecursive(subCategory);
			}
			_context.Categories.Remove(category);
		}


	}

}
