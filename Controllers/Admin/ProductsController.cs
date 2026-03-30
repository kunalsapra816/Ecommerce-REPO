using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniEcommerMVC.Data;
using MiniEcommerMVC.Interfaces;

namespace MiniEcommerMVC.Controllers
{
    public class ProductsController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;


        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: /Products
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }
    }
}