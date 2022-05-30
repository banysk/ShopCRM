using ShopCRM.Models;

namespace ShopCRM.Utils
{
    public class Tabler
    {
        private static string CreateTable(string innerHTML)
        {
            return $"<table class=\"generated__table\" cellspacing=\"0\">\n{innerHTML}</table>\n";
        }

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

        public static string FromCustomers(List<Customer> customers)
        {
            List<string> heads = new List<string>()
            {
                "Guid",
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
                    "<tr>" +
                    $"<td>{customer.Id}</td>\n" +
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
    }
}
