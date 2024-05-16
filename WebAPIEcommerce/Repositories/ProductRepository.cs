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
    }
}
