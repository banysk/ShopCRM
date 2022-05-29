namespace ShopCRM.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string CustomId { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string ImageLink { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public float Price { get; set; }

        public string Stock { get; set; } = string.Empty;

        public bool IsOrdered { get; set; }

        public int OrderId { get; set; }
    }
}
