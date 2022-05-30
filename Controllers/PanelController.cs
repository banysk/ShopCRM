using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ShopCRM.Database;
using ShopCRM.Utils;
using System.Security.Claims;

namespace ShopCRM.Controllers
{
    public class PanelController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly Security _security;

        public PanelController(ApplicationDbContext db)
        {
            _db = db;
            _security = new Security(db);
        }

        [Route("")]
        [Route("Panel")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Panel/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Panel/Login")]
        public async Task<IActionResult> Login(IFormCollection collection)
        {
            var login = collection.Where(x => x.Key == "login").First().ToString();
            var role = _security.Login(collection);

            if (role != "guest")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
                };

                ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

                await AuthenticationHttpContextExtensions.SignInAsync(HttpContext, new ClaimsPrincipal(id));
                return Redirect("Panel");
            }

            return Redirect("Panel/Login");
        }

        [HttpGet]
        [Route("Panel/Logout")]
        public async Task<IActionResult> Logout()
        {
            await AuthenticationHttpContextExtensions.SignOutAsync(HttpContext);
            return Redirect("Panel");
        }

        [HttpGet]
        [Route("Panel/Users")]
        public IActionResult Users()
        {
            return View();
        }
    }
}
