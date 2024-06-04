using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StandardLibrary.Comment;
using StandardLibrary.Product;
using WebMVCEcommerce.Services.Product;

namespace WebMVCEcommerce.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductApiClient _productApiClient;
		public ProductController(ILogger<ProductController> logger, IProductApiClient productApiClient)
        {
            _logger = logger;
            _productApiClient = productApiClient;
        }
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Get Products from API");
            var products = await _productApiClient.GetProducts();
            //ViewData["Products"] = products;
            return View(products);
        }

		
		public async Task<IActionResult> Details(int id)
		{
			_logger.LogInformation($"Getting product details for ID: {id}");
			var product = await _productApiClient.GetProductById(id);
			if (product == null)
			{
				return NotFound();
			}

			var comments = await _productApiClient.GetProductComments(id);
			var productComment = new ProductCommentDto
			{
				Product = product,
				Comments = comments
			};

			return View(productComment);
		}

	}
}
