using System.ComponentModel.DataAnnotations;

namespace MiniEcommerMVC.Models.Domain
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }


        [Required]
        public int StockQuantity { get; set;}


        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
