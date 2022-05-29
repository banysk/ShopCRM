using Microsoft.AspNetCore.Mvc;

namespace ShopCRM.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
