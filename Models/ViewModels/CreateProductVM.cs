using System.ComponentModel.DataAnnotations;


namespace MiniEcommerMVC.Models.ViewModels
{
    public class CreateProductVM
    {

        public int Id { get; set; }

        [Required]
        public string Name {  get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set;}

        public string ImageUrl { get; set; }

        [Required]
        public int StockQuantity { get; set;}


    }
}
