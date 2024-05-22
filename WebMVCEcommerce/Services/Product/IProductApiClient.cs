using StandardLibrary.Comment;
using StandardLibrary.Product;

namespace WebMVCEcommerce.Services.Product
{
    public interface IProductApiClient
    {
        Task<List<ProductDto>> GetProducts();
		Task<ProductDto> GetProductById(int id);
		Task<List<CommentDto>> GetProductComments(int productId);

	}
}
