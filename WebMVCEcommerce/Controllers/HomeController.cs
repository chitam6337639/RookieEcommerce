using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebMVCEcommerce.Models;
using WebMVCEcommerce.Services.Product;

namespace WebMVCEcommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductApiClient _productApiClient;

        public HomeController(ILogger<HomeController> logger, IProductApiClient productApiClient)
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
            //return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
