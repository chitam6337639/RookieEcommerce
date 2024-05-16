using Microsoft.AspNetCore.Mvc;
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
    }
}
