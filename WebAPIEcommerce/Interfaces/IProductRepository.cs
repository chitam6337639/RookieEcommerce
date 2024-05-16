using StandardLibrary;
using WebAPIEcommerce.Models.Entities;

namespace WebAPIEcommerce.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductDto> CreateProduct(ProductDto productDto);
        Task<List<ProductDto>> GetProducts();
    }
}
