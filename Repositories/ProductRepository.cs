using Microsoft.EntityFrameworkCore;
using MiniEcommerMVC.Data;
using MiniEcommerMVC.Models.Domain;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    // get all products list
    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }


    // get a product by Id
    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    // add the product in DB
    public async Task AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    // update the product in DB
    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    //Delete the product from DB
    public async Task DeleteAsync(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }




}
