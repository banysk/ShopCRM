namespace ShopCRM.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int CustomerId { get; set; }

        List<string> ItemsId { get; set; }

        public float ItemsPrice { get; set; }

        public float ItemsPaid { get; set; }

        public string DeliveryService { get; set; }

        public string DeliveryType { get; set; }

        public string TrackNumber { get; set; }

        public float DeliveryPrice { get; set; }

        public float DeliveryPaid { get; set; }

    }
}
