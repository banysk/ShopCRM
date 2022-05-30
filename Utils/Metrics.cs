using ShopCRM.Database;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopCRM.Utils
{
    [NotMapped]
    public class Metrics
    {
        public int Users { get; set; }

        public int Customers { get; set; }

        public int TemporaryCustomers { get; set; }

        public int Items { get; set; }

        public int Orders { get; set; }

        public Metrics(ApplicationDbContext db)
        {
            Users = db.Users.Count();
            Customers = db.Customers.Count();
            TemporaryCustomers = db.TemporaryCustomers.Count();
            Items = db.Items.Count();
            Orders = db.Orders.Count();
        }
    }
}
