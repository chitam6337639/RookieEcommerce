using Microsoft.EntityFrameworkCore;
using StandardLibrary;
using WebAPIEcommerce.Data.DataContext;
using WebAPIEcommerce.Interfaces;
using WebAPIEcommerce.Models.Entities;

namespace WebAPIEcommerce.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ProductDto> CreateProduct(ProductDto productDto)
        {
            
            var product = new Product
            {
                ProductName = productDto.ProductName,
                ProductDescription = productDto.ProductDescription,
                Price = productDto.Price,
                ImageURL = productDto.ImageURL,
                CategoryId = productDto.CategoryId,
                
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return new ProductDto
            {
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                Price = product.Price,
                ImageURL = product.ImageURL,
                CategoryId = product.CategoryId,
                
            };
        }


        public async Task<List<ProductDto>> GetProducts()
        {
            return await _context.Products
                .Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    Price = p.Price,
                    ImageURL = p.ImageURL,
                    CategoryId = p.Category.CategoryId
                })
                .ToListAsync();
        }

        public async Task<ProductDto> GetProductId(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return null;
            }

            return new ProductDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ProductDescription = product.ProductDescription,
                Price = product.Price,
                ImageURL = product.ImageURL,
                CategoryId = product.Category.CategoryId
            };
        }


        public async Task<bool> UpdateProduct(int id, ProductDto productDto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            product.ProductName = productDto.ProductName;
            product.ProductDescription = productDto.ProductDescription;
            product.Price = productDto.Price;
            product.ImageURL = productDto.ImageURL;
            product.CategoryId = productDto.CategoryId;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> DeleteProduct(int id) // Thêm phương thức này
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
