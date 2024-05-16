using Microsoft.AspNetCore.Mvc;

namespace WebMVCEcommerce.Controllers
{
    public class ProductController1 : Controller
    {
        public IActionResult Detail()
        {
            return View();
        }
    }
}
