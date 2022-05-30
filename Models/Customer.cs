using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopCRM.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Surname { get; set; } = string.Empty;

        public string Patronymic { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Street { get; set; } = string.Empty;

        public string Building { get; set; } = string.Empty;

        public string Flat { get; set; } = string.Empty;

        public bool IsConfirmed { get; set; }
    }
}
