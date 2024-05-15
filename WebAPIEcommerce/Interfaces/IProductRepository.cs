using StandardLibrary;
using WebAPIEcommerce.Models.Entities;

namespace WebAPIEcommerce.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> CreateProduct(Product product);
        Task<List<ProductDto>> GetProducts();
    }
}
