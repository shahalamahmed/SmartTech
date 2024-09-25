using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SmartTech.Models
{
    public class Product // Changed from Products to Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        [Required]
        [MaxLength(500)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Price")]
        public decimal Price { get; set; }

        [Required]
        [DisplayName("Stock Quantity")]
        public int StockQuantity { get; set; }

        [DisplayName("Category")]
        public string Category { get; set; }

        [DisplayName("Image")]
        public string ImageFile { get; set; }
    }
}
