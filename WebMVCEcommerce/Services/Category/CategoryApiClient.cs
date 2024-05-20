using Newtonsoft.Json;
using StandardLibrary;
using StandardLibrary.Category;

namespace WebMVCEcommerce.Services.Category
{
	public class CategoryApiClient : ICategoryApiClient
	{
		private readonly HttpClient _httpClient;

		public CategoryApiClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri("https://localhost:7245");
		}
		public async Task<List<CategoryDto>> GetCategories()
		{
			var response = await _httpClient.GetAsync("/api/category/all");
			response.EnsureSuccessStatusCode();

			var content = response.Content.ReadAsStringAsync().Result;
			var categorieslist = JsonConvert.DeserializeObject<List<CategoryDto>>(content)!;
			return categorieslist;
		}
	}
}
