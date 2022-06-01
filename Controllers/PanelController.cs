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
        private readonly DataHandler _dataHandler;

        public PanelController(ApplicationDbContext db)
        {
            _db = db;
            _security = new Security(db);
            _dataHandler = new DataHandler(db);
        }
        
        #region index DONE
        [Route("")]
        [Route("Panel")]
        [Authorize(Roles = "admin, creator")]
        public IActionResult Index()
        {
            ViewBag.Metrics = new Metrics(_db);
            return View();
        }
        #endregion

        #region authorization DONE
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
        [Authorize(Roles = "admin, creator")]
        public async Task<IActionResult> Logout()
        {
            ViewBag.Metrics = new Metrics(_db);
            await AuthenticationHttpContextExtensions.SignOutAsync(HttpContext);
            return Redirect("/Panel");
        }
        #endregion

        #region user DONE
        [HttpGet]
        [Route("Panel/Users/{page}")]
        [Authorize(Roles = "creator")]
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

                var users = _db.Users.OrderBy(x => x.Login).ToList();
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
        [Route("Panel/User/Create")]
        [Authorize(Roles = "creator")]
        public IActionResult CreateUser()
        {
            ViewBag.Metrics = new Metrics(_db);
            return View(new User());
        }

        [HttpPost]
        [Route("Panel/User/Create")]
        [Authorize(Roles = "creator")]
        public IActionResult CreateUser(IFormCollection collection)
        {
            var login = collection.Where(x => x.Key == "login").First().Value;
            var password = collection.Where(x => x.Key == "password").First().Value;
            var role = collection.Where(x => x.Key == "role").First().Value;

            User user = new User()
            {
                Id = Guid.Empty,
                Login = login,
                Password = password,
                Role = role
            };

            _dataHandler.Create(user);

            return Redirect("/Panel/Users/1");
        }
        #endregion

        #region customer DONE
        /// <summary>
        /// Страница с клиентами
        /// </summary>
        [HttpGet]
        [Route("Panel/Customers/{page}")]
        [Authorize(Roles = "admin, creator")]
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

                var customers = _db.Customers.OrderBy(x => x.Surname).ToList();
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

        /// <summary>
        /// Страница клиента
        /// </summary>
        [HttpGet]
        [Route("Panel/Customer/{guid}")]
        [Authorize(Roles = "admin, creator")]
        public IActionResult Customer(Guid guid)
        {
            ViewBag.Metrics = new Metrics(_db);
            Customer customer = _db.Customers.Where(x => x.Id == guid).First();
            return View("Customer", customer);
        }

        /// <summary>
        /// Страница создания/редактирования
        /// </summary>
        [HttpGet]
        [Route("Panel/Customer/Create")]
        [Authorize(Roles = "admin, creator")]
        public IActionResult CreateCustomer()
        {
            ViewBag.Metrics = new Metrics(_db);
            return View("Customer", new Customer());
        }

        /// <summary>
        /// Метод создания/редактирования
        /// </summary>
        [HttpPost]
        [Route("Panel/Customer/CreateOrUpdate")]
        [Authorize(Roles = "admin, creator")]
        public IActionResult CreateOrUpdateCustomer(IFormCollection collection)
        {
            _dataHandler.CreateOrUpdateCustomer(collection);
            return Redirect("/Panel/Customers/1");
        }

        /// <summary>
        /// Удаление клиента
        /// </summary>
        [HttpGet]
        [Route("Panel/Customer/{guid}/Remove")]
        [Authorize(Roles = "admin, creator")]
        public IActionResult RemoveCustomer(Guid guid)
        {
            _dataHandler.RemoveCustomer(guid);
            return Redirect("/Panel/Customers/1");
        }

        /// <summary>
        /// Подтверждение клиента
        /// </summary>
        [HttpGet]
        [Route("Panel/Customer/{guid}/Confirm")]
        [Authorize(Roles = "admin, creator")]
        public IActionResult ConfirmCustomer(Guid guid)
        {
            _dataHandler.ConfirmCustomer(guid);
            return Redirect("/Panel/Customers/1");
        }
        #endregion

        #region item DONE
        /// <summary>
        /// Страница с товарами
        /// </summary>
        [HttpGet]
        [Route("Panel/Items/{page}")]
        [Authorize(Roles = "admin, creator")]
        public IActionResult Items(int page)
        {
            if (page < 1)
            {
                return Redirect("/Panel/Items/1");
            }
            else
            {
                ViewBag.Metrics = new Metrics(_db);
                ViewBag.Page = page;

                var items = _db.Items.OrderBy(x => x.CustomId).ToList();
                var selectedItems = new List<Item>();
                var current = (page - 1) * 6;
                var end = current + 6;

                while (current <= end && current < items.Count)
                {
                    selectedItems.Add(items[current]);
                    current++;
                }

                return View(selectedItems);
            }
        }

        /// <summary>
        /// Страница товара
        /// </summary>
        [HttpGet]
        [Route("Panel/Item/{guid}")]
        [Authorize(Roles = "admin, creator")]
        public IActionResult Item(Guid guid)
        {
            ViewBag.Metrics = new Metrics(_db);
            Item item = _db.Items.Where(x => x.Id == guid).First();
            return View("Item", item);
        }

        /// <summary>
        /// Страница создания/редактирования товара
        /// </summary>
        [HttpGet]
        [Route("Panel/Item/Create")]
        [Authorize(Roles = "admin, creator")]
        public IActionResult CreateItem()
        {
            ViewBag.Metrics = new Metrics(_db);
            return View("Item", new Item());
        }

        /// <summary>
        /// Метод создания/редактирования
        /// </summary>
        [HttpPost]
        [Route("Panel/Item/Create")]
        [Authorize(Roles = "admin, creator")]
        public IActionResult CreateOrUpdateItem(IFormCollection collection)
        {
            _dataHandler.CreateOrUpdateItem(collection);
            return Redirect("/Panel/Items/1");
        }

        /// <summary>
        /// Удаление товара
        /// </summary>
        [HttpGet]
        [Route("Panel/Item/{guid}/Remove")]
        [Authorize(Roles = "admin, creator")]
        public IActionResult RemoveItem(Guid guid)
        {
            _dataHandler.RemoveItem(guid);
            return Redirect("/Panel/Items/1");
        }

        /// <summary>
        /// Заказ товара
        /// </summary>
        [HttpGet]
        [Route("Panel/Item/{guid}/Order")]
        [Authorize(Roles = "admin, creator")]
        public IActionResult OrderItem(Guid guid)
        {
            _dataHandler.OrderItem(guid);
            return Redirect("/Panel/Items/1");
        }
        #endregion

        #region order
        /// <summary>
        /// Страница заказов
        /// </summary>
        [HttpGet]
        [Route("Panel/Orders/{page}")]
        [Authorize(Roles = "admin, creator")]
        public IActionResult Orders(int page)
        {
            if (page < 1)
            {
                return Redirect("/Panel/Orders/1");
            }
            else
            {
                ViewBag.Metrics = new Metrics(_db);
                ViewBag.Page = page;

                var orders = _db.Orders.ToList();
                var selectedOrders = new List<Order>();
                var current = (page - 1) * 10;
                var end = current + 10;

                while (current <= end && current < orders.Count)
                {
                    selectedOrders.Add(orders[current]);
                    current++;
                }

                return View(selectedOrders);
            }
        }

        /// <summary>
        /// Страница заказа
        /// </summary>
        [HttpGet]
        [Route("Panel/Item/{guid}")]
        [Authorize(Roles = "admin, creator")]
        public IActionResult Order(Guid guid)
        {
            ViewBag.Metrics = new Metrics(_db);
            Order order = _db.Orders.Where(x => x.Id == guid).First();
            return View("Order", order);
        }

        /// <summary>
        /// Страница создания/редактирования товара
        /// </summary>
        [HttpGet]
        [Route("Panel/Order/Create")]
        [Authorize(Roles = "admin, creator")]
        public IActionResult CreateOrder()
        {
            ViewBag.Metrics = new Metrics(_db);
            return View("Order", new Order());
        }

        /// <summary>
        /// Метод создания/редактирования
        /// </summary>
        [HttpPost]
        [Route("Panel/Item/Create")]
        [Authorize(Roles = "admin, creator")]
        public IActionResult CreateOrUpdateOrder(IFormCollection collection)
        {
            //_dataHandler.CreateOrUpdateOrder(collection);
            return Redirect("/Panel/Orders/1");
        }
        #endregion
    }
}
