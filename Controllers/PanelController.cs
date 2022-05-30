using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopCRM.Database;
using ShopCRM.Models;
using ShopCRM.Utils;
using System.Security.Claims;

namespace ShopCRM.Controllers
{
    /// <summary>
    /// Нужно прокидывать метрики в каждый контроллер
    /// </summary>
    public class PanelController : Controller
    {
        public readonly ApplicationDbContext _db;
        private readonly Security _security;

        public PanelController(ApplicationDbContext db)
        {
            _db = db;
            _security = new Security(db);
        }

        [Route("")]
        [Route("Panel")]
        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            ViewBag.Metrics = new Metrics(_db);
            return View();
        }

        [HttpGet]
        [Route("Panel/Login")]
        public IActionResult Login()
        {
            ViewBag.Metrics = new Metrics(_db);
            return View();
        }

        [HttpPost]
        [Route("Panel/Login")]
        public async Task<IActionResult> Login(IFormCollection collection)
        {
            ViewBag.Metrics = new Metrics(_db);

            var login = collection.Where(x => x.Key == "login").First().Value;
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
                return Redirect("/Panel");
            }

            return Redirect("/Panel/Login");
        }

        [HttpGet]
        [Route("Panel/Logout")]
        public async Task<IActionResult> Logout()
        {
            ViewBag.Metrics = new Metrics(_db);
            await AuthenticationHttpContextExtensions.SignOutAsync(HttpContext);
            return Redirect("/Panel");
        }

        [HttpGet]
        [Route("Panel/Users/{page}")]
        [Authorize(Roles = "admin")]
        public IActionResult Users(int page)
        {
            if (page < 1)
            {
                return Redirect("/Panel/Users/1");
            }
            else
            {
                ViewBag.Metrics = new Metrics(_db);
                ViewBag.Page = page;

                var users = _db.Users.ToList();
                var selectedUsers = new List<User>();
                var current = (page - 1) * 10;
                var end = current + 10;

                while (current <= end && current < users.Count)
                {
                    selectedUsers.Add(users[current]);
                    current++;
                }

                return View(selectedUsers);
            }
        }

        [HttpGet]
        [Route("Panel/Customers/{page}")]
        [Authorize(Roles = "admin")]
        public IActionResult Customers(int page)
        {
            if (page < 1)
            {
                return Redirect("/Panel/Customers/1");
            }
            else
            {
                ViewBag.Metrics = new Metrics(_db);
                ViewBag.Page = page;

                var customers = _db.Customers.ToList();
                var selectedCustomers = new List<Customer>();
                var current = (page - 1) * 10;
                var end = current + 10;

                while (current <= end && current < customers.Count)
                {
                    selectedCustomers.Add(customers[current]);
                    current++;
                }

                return View(selectedCustomers);
            }
        }
    }
}
