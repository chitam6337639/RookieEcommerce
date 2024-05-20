using StandardLibrary;
using WebAPIEcommerce.Models.Entities;

namespace WebAPIEcommerce.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductDto> CreateProduct(ProductDto productDto);
        Task<List<ProductDto>> GetProducts();
        Task<ProductDto> GetProductId(int id);
        Task<bool> UpdateProduct(int id,ProductDto productDto);
        Task<bool> DeleteProduct(int id);
    }
}
