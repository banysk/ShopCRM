﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopCRM.Models
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.Empty;

        public string CustomId { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string ImageLink { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public float Price { get; set; }

        public string Stock { get; set; } = string.Empty;

        public bool IsOrdered { get; set; } = false;

        public Guid OrderGuid { get; set; } = Guid.Empty;
    }
}
