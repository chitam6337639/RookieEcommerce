using Newtonsoft.Json;
using StandardLibrary;
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

    }
}
