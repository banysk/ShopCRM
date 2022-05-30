using ShopCRM.Database;
using ShopCRM.Models;

namespace ShopCRM.Utils
{
    public class Security
    {
        private readonly ApplicationDbContext _db;

        public Security(ApplicationDbContext db)
        {
            _db = db;
        }

        public string Login(IFormCollection collection)
        {
            var login = collection.Where(x => x.Key == "login").First().Value.ToString();
            var password = collection.Where(x => x.Key == "password").First().Value.ToString();

            List<User> users = _db.Users.Where(x => x.Login == login && x.Password == password).ToList();

            if (users.Count == 1)
            {
                return users[0].Role;
            }

            return "guest";
        }
    }
}
