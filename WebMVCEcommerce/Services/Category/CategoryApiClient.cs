using Newtonsoft.Json;
using StandardLibrary.Category;

namespace WebMVCEcommerce.Services.Category
{
    public class CategoryApiClient : ICategoryApiClient
	{
		private readonly HttpClient _httpClient;

		public CategoryApiClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task<List<CategoryDto>> GetCategories()
		{
			var response = await _httpClient.GetAsync("/api/category/all");
			response.EnsureSuccessStatusCode();

			var content = response.Content.ReadAsStringAsync().Result;
			var categorieslist = JsonConvert.DeserializeObject<List<CategoryDto>>(content)!;
			return categorieslist;
		}
		public async Task<CategoryDetailDto> GetCategoryDetail(int categoryId)
		{
			var response = await _httpClient.GetAsync($"/api/category/{categoryId}/details");
			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();
			var categoryDetail = JsonConvert.DeserializeObject<CategoryDetailDto>(content)!;
			return categoryDetail;
		}
	}
}
