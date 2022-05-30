using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopCRM.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int CustomerId { get; set; }

        List<Guid> ItemsId { get; set; } = new List<Guid>();

        public float ItemsPrice { get; set; }

        public float ItemsPaid { get; set; }

        public string DeliveryService { get; set; } = string.Empty;

        public string DeliveryType { get; set; } = string.Empty;

        public string TrackNumber { get; set; } = string.Empty;

        public float DeliveryPrice { get; set; }

        public float DeliveryPaid { get; set; }

    }
}
