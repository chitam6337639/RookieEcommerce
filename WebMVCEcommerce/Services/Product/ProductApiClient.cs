using Newtonsoft.Json;
using StandardLibrary.Comment;
using StandardLibrary.Product;
using System.Text.Json;

namespace WebMVCEcommerce.Services.Product
{
    public class ProductApiClient : IProductApiClient
    {
        private readonly HttpClient _httpClient;

        public ProductApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7245");
        }
        public async Task<List<ProductDto>> GetProducts()
        {
            var response = await _httpClient.GetAsync("/api/product");
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            var productList = JsonConvert.DeserializeObject<List<ProductDto>>(content)!;
            return productList;
        }
		public async Task<ProductDto> GetProductById(int id)
		{
			var response = await _httpClient.GetAsync($"/api/product/{id}");
			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();
			var product = JsonConvert.DeserializeObject<ProductDto>(content);
			return product;
		}
		public async Task<List<CommentDto>> GetProductComments(int productId)
		{
			var response = await _httpClient.GetAsync($"/api/comment/{productId}");
			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();
			var comments = JsonConvert.DeserializeObject<List<CommentDto>>(content);
			return comments!;
		}

	}
}
