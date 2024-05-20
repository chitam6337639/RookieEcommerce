using Microsoft.AspNetCore.Mvc;
using WebMVCEcommerce.Services.Category;
using WebMVCEcommerce.Services.Product;

namespace WebMVCEcommerce.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ILogger<CategoryController> _logger;
		private readonly ICategoryApiClient _categoryApiClient;
		public CategoryController(ILogger<CategoryController> logger, ICategoryApiClient categoryApiClient)
		{
			_logger = logger;
			_categoryApiClient = categoryApiClient;
		}
		public async Task<IActionResult> CategoryDetail()
		{
			var categories = await _categoryApiClient.GetCategories();
			return View(categories);
		}

	}
}
