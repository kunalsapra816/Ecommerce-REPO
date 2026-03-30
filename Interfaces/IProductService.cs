using MiniEcommerMVC.Models.Domain;
using MiniEcommerMVC.Models.ViewModels;



namespace MiniEcommerMVC.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task AddAsync(CreateProductVM model);
        Task<bool> UpdateAsync(CreateProductVM model);
        Task<bool> DeleteAsync(int id);

    }
}
