using Microsoft.AspNetCore.Mvc;

namespace ShopCRM.Controllers
{
    public class PanelController : Controller
    {
        [Route("")]
        [Route("Panel")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Panel/Users")]
        public IActionResult Users()
        {
            return View();
        }
    }
}
