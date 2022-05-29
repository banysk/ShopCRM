using Microsoft.AspNetCore.Mvc;

namespace ShopCRM.Controllers
{
    public class PanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
