using StandardLibrary;

namespace WebMVCEcommerce.Services.Product
{
    public interface IProductApiClient
    {
        Task<List<ProductDto>> GetProducts();
    }
}
