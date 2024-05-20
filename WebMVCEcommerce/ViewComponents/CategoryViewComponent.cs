using Microsoft.AspNetCore.Mvc;
using WebMVCEcommerce.Services.Category;

namespace WebMVCEcommerce.ViewComponents
{
	public class CategoryViewComponent : ViewComponent
	{
		private readonly ICategoryApiClient _categoryApiClient;

		public CategoryViewComponent(ICategoryApiClient categoryApiClient)
		{
			_categoryApiClient = categoryApiClient;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var categories = await _categoryApiClient.GetCategories();
			return View(categories);
		}
	}
}
