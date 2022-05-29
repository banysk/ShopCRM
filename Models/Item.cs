namespace ShopCRM.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string CustomId { get; set; }

        public string Name { get; set; }

        public string ImageLink { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public string Stock { get; set; }

        public bool IsOrdered { get; set; }

        public int OrderId { get; set; }
    }
}
