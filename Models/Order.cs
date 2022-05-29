namespace ShopCRM.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int CustomerId { get; set; }

        List<string> ItemsId { get; set; } = new List<string>();

        public float ItemsPrice { get; set; }

        public float ItemsPaid { get; set; }

        public string DeliveryService { get; set; } = string.Empty;

        public string DeliveryType { get; set; } = string.Empty;

        public string TrackNumber { get; set; } = string.Empty;

        public float DeliveryPrice { get; set; }

        public float DeliveryPaid { get; set; }

    }
}
