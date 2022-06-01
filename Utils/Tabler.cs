using ShopCRM.Models;

namespace ShopCRM.Utils
{
    public class Tabler
    {
        private static string CreateTable(string innerHTML)
        {
            return $"<table class=\"generated__table\" cellspacing=\"0\">\n{innerHTML}</table>\n";
        }
        #region user
        public static string FromUsers(List<User> users)
        {
            List<string> heads = new List<string>()
            {
                "Guid",
                "Логин",
                "Пароль",
                "Роль"
            };
            // thead
            string thead = "";
            foreach (string head in heads)
            {
                thead += $"<th>{head}</th>\n";
            }
            thead = $"<tr>\n{thead}</tr\n>";
            // tbody
            string tbody = "";
            foreach (User user in users)
            {
                tbody +=
                    "<tr>" +
                    $"<td>{user.Id}</td>\n" +
                    $"<td>{user.Login}</td>\n" +
                    $"<td>{user.Password}</td>\n" +
                    $"<td>{user.Role}</td>\n" +
                    "</tr>\n";
            }

            return CreateTable(thead + tbody);
        }
        #endregion

        #region customers
        public static string FromCustomers(List<Customer> customers)
        {
            List<string> heads = new List<string>()
            {
                "Имя",
                "Фамилия",
                "Отчество",
                "Номер",
                "Индекс",
                "Страна",
                "Город",
                "Улица",
                "№",
                "Кв",
                ""
            };
            // thead
            string thead = "";
            foreach (string head in heads)
            {
                thead += $"<th>{head}</th>\n";
            }
            thead = $"<tr>\n{thead}</tr\n>";
            // tbody
            string tbody = "";
            foreach (Customer customer in customers)
            {
                tbody +=
                    $"<tr onclick=\"location.href='/Panel/Customer/{customer.Id}'\">" +
                    $"<td>{customer.Name}</td>\n" +
                    $"<td>{customer.Surname}</td>\n" +
                    $"<td>{customer.Patronymic}</td>\n" +
                    $"<td>{customer.PhoneNumber}</td>\n" +
                    $"<td>{customer.Index}</td>\n" +
                    $"<td>{customer.Country}</td>\n" +
                    $"<td>{customer.City}</td>\n" +
                    $"<td>{customer.Street}</td>\n" +
                    $"<td>{customer.Building}</td>\n" +
                    $"<td>{customer.Flat}</td>\n" +
                    $"<td>{customer.IsConfirmed}</td>\n" +
                    "</tr>\n";
            }

            return CreateTable(thead + tbody);
        }
        #endregion

        #region item
        public static string FromItems(List<Item> items)
        {
            List<string> heads = new List<string>()
            {
                "Id",
                "Наименование",
                "",
                "Цена",
                "Склад",
                "Заказано"
            };
            // thead
            string thead = "";
            foreach (string head in heads)
            {
                thead += $"<th>{head}</th>\n";
            }
            thead = $"<tr>\n{thead}</tr\n>";
            // tbody
            string tbody = "";
            foreach (Item item in items)
            {
                tbody +=
                    $"<tr onclick=\"location.href='/Panel/Item/{item.Id}'\">" +
                    $"<td>{item.CustomId}</td>\n" +
                    $"<td>{item.Name}</td>\n" +
                    $"<td class=\"img\"><img src=\"{item.ImageLink}\"/></td>\n" +
                    $"<td>{item.Price}</td>\n" +
                    $"<td>{item.Stock}</td>\n" +
                    $"<td>{item.IsOrdered}</td>\n" +
                    "</tr>\n";
            }

            return CreateTable(thead + tbody);
        }
        #endregion

        #region order
        public static string FromOrders(List<Order> orders)
        {
            List<string> heads = new List<string>()
            {
                "Статус",
                "Создан",
                "Обновлен",
                "Стоимость",
                "Получено",
                "Служба",
                "Тип",
                "№",
                "Стоимость",
                "Получено"

            };
            // thead
            string thead = "";
            foreach (string head in heads)
            {
                thead += $"<th>{head}</th>\n";
            }
            thead = $"<tr>\n{thead}</tr\n>";
            // tbody
            string tbody = "";
            foreach (Order order in orders)
            {
                tbody +=
                    $"<tr onclick=\"location.href='/Panel/Order/{order.Id}'\">" +
                    $"<td>{order.Id}</td>\n" +
                    $"<td>{order.Status}</td>\n" +
                    $"<td>{order.CreatedDate}</td>\n" +
                    $"<td>{order.UpdatedDate}</td>\n" +
                    $"<td>{order.ItemsPrice}</td>\n" +
                    $"<td>{order.ItemsPaid}</td>\n" +
                    $"<td>{order.DeliveryService}</td>\n" +
                    $"<td>{order.DeliveryType}</td>\n" +
                    $"<td>{order.TrackNumber}</td>\n" +
                    $"<td>{order.DeliveryPrice}</td>\n" +
                    $"<td>{order.DeliveryPaid}</td>\n" +
                    "</tr>\n";
            }

            return CreateTable(thead + tbody);
        }
        #endregion
    }
}
