

using Microsoft.AspNetCore.Mvc;
using MiniEcommerMVC.Models.ViewModels;
using MiniEcommerMVC.Models.Domain;
using MiniEcommerMVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace MiniEcommerMVC.Controllers.Admin
{
    //[Route("ProductHistory")]
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }


        //[Route("CreateProduct")]
        public IActionResult Create()
        {
            return View();
        }


        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }


        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            // find a the product
            var product = await _context.Products.FindAsync(id);

            // handle null
            if(product == null)
            {
                return NotFound();
            }

            // convert to view model
            var model = new CreateProductVM
            {
                Id  = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                StockQuantity = product.StockQuantity,
            };

            // send to view
            return View(model);

        }



        // Create GET Functionality
        public async Task<IActionResult> Delete(CreateProductVM model)
        {
            var product = await _context.Products.FindAsync(model.Id);

            if(product == null)
            {
                return NotFound();
            }

            return View(product);


        }

        //[Route("CreateProduct")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductVM model)
        {

            Console.WriteLine("POST HIT"); 
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Model Invalid");
                return View(model);
            }

            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                ImageUrl = model.ImageUrl,
                StockQuantity = model.StockQuantity
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }



        // Edit products detailed option
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateProductVM model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            // fetch the product with that id
            var product = await _context.Products.FindAsync(model.Id);

            // check weather this product is null or not
            if(product == null)
            {
                return NotFound();
            }


            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.ImageUrl = model.ImageUrl;
            product.StockQuantity = model.StockQuantity;

            // save changes
            await _context.SaveChangesAsync();


            // return as view
            return RedirectToAction("Index");

        }


        // post delete step
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            // find the product
            var product = await _context.Products.FindAsync(id);

            //check it exits
            if(product == null)
            {
                return NotFound();
            }

            // remove that product
            _context.Products.Remove(product);

            // save that changes
            await _context.SaveChangesAsync();

            //redirect
            return RedirectToAction("Index");

        }
    }
}
