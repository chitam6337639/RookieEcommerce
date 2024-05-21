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
		public async Task<IActionResult> Index()
		{
			var categories = await _categoryApiClient.GetCategories();
			return View(categories);
		}
		public async Task<IActionResult> CategoryDetail(int id) 
		{
			var categoryDetail = await _categoryApiClient.GetCategoryDetail(id);
			if (categoryDetail == null)
			{
				return NotFound();
			}
			return View(categoryDetail);
		}

	}
}
