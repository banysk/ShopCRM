using ShopCRM.Models;

namespace ShopCRM.Database
{
    public class DataHandler
    {
        public readonly ApplicationDbContext _db;

        public DataHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        #region User DONE
        public void Create(User user)
        {
            if (user.Id != Guid.Empty)
                return;

            _db.Users.Add(user);
            _db.SaveChanges();
        }
        #endregion

        #region Customer DONE
        public void CreateOrUpdateCustomer(IFormCollection collection)
        {
            var guid = Guid.Parse(collection.Where(x => x.Key == "guid").First().Value);
            var name = collection.Where(x => x.Key == "name").First().Value;
            var surname = collection.Where(x => x.Key == "surname").First().Value;
            var patronymic = collection.Where(x => x.Key == "patronymic").First().Value;
            var phonenumber = collection.Where(x => x.Key == "phonenumber").First().Value;
            var index = collection.Where(x => x.Key == "index").First().Value;
            var country = collection.Where(x => x.Key == "country").First().Value;
            var city = collection.Where(x => x.Key == "city").First().Value;
            var street = collection.Where(x => x.Key == "street").First().Value;
            var building = collection.Where(x => x.Key == "building").First().Value;
            var flat = collection.Where(x => x.Key == "flat").First().Value;


            Customer customer = new Customer()
            {
                Id = guid,
                Name = name,
                Surname = surname,
                Patronymic = patronymic,
                PhoneNumber = phonenumber,
                Index = index,
                Country = country,
                City = city,
                Street = street,
                Building = building,
                Flat = flat,
                IsConfirmed = false
            };

            if (customer.Id == Guid.Empty)
            {
                _db.Customers.Add(customer);
                _db.SaveChanges();
            }
            else
            {
                Customer target = _db.Customers.First(x => x.Id == customer.Id);

                target.Name = customer.Name;
                target.Surname = customer.Surname;
                target.Patronymic = customer.Patronymic;
                target.PhoneNumber = customer.PhoneNumber;
                target.Index = customer.Index;
                target.Country = customer.Country;
                target.City = customer.City;
                target.Street = customer.Street;
                target.Building = customer.Building;
                target.Flat = customer.Flat;

                _db.Update(target);
                _db.SaveChanges();
            }
        }

        public void RemoveCustomer(Guid guid)
        {
            Customer customer = _db.Customers.First(x => x.Id == guid);
            _db.Customers.Remove(customer);
            _db.SaveChanges();
        }

        public void ConfirmCustomer(Guid guid)
        {
            Customer customer = _db.Customers.First(x => x.Id == guid);
            customer.IsConfirmed = true;
            _db.Customers.Update(customer);
            _db.SaveChanges();
        }
        #endregion

        #region item
        public void CreateOrUpdateItem(IFormCollection collection)
        {
            var guid = Guid.Parse(collection.Where(x => x.Key == "guid").First().Value);
            var customid = collection.Where(x => x.Key == "customid").First().Value;
            var name = collection.Where(x => x.Key == "name").First().Value;
            var imagelink = collection.Where(x => x.Key == "imagelink").First().Value;
            var description = collection.Where(x => x.Key == "description").First().Value;
            var price = float.Parse(collection.Where(x => x.Key == "price").First().Value);
            var stock = collection.Where(x => x.Key == "stock").First().Value;
            Guid orderguid = Guid.Parse(collection.Where(x => x.Key == "orderguid").First().Value);

            Item item = new Item()
            {
                Id = guid,
                CustomId = customid,
                Name = name,
                ImageLink = imagelink,
                Description = description,
                Price = price,
                Stock = stock,
                OrderGuid = orderguid
            };

            if (item.Id == Guid.Empty)
            {
                _db.Items.Add(item);
                _db.SaveChanges();
            }
            else
            {
                Item target = _db.Items.First(x => x.Id == item.Id);

                target.CustomId = customid;
                target.Name = name; 
                target.ImageLink = imagelink;
                target.Description = description;
                target.Price = price;
                target.Stock = stock;
                target.OrderGuid = orderguid;

                _db.Update(target);
                _db.SaveChanges();
            }
        }

        public void RemoveItem(Guid guid)
        {
            Item item = _db.Items.First(x => x.Id == guid);
            _db.Items.Remove(item);
            _db.SaveChanges();
        }

        public void OrderItem(Guid guid)
        {
            Item item = _db.Items.First(x => x.Id == guid);
            item.IsOrdered = true;
            _db.Items.Update(item);
            _db.SaveChanges();
        }
        #endregion
    }
}
