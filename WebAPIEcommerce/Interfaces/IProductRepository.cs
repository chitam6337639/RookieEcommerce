using StandardLibrary.Product;
using WebAPIEcommerce.Models.Entities;

namespace WebAPIEcommerce.Interfaces
{
    public interface IProductRepository
    {
        Task<ProductDto> CreateProduct(CreateProductDto createProductDto);
        Task<List<ProductDto>> GetProducts();
        Task<ProductDto> GetProductId(int id);
        Task<bool> UpdateProduct(int id, CreateProductDto createProductDto);
        Task<bool> DeleteProduct(int id);
    }
}
