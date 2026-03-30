using MiniEcommerMVC.Interfaces;
using MiniEcommerMVC.Models.Domain;
using MiniEcommerMVC.Models.ViewModels;



namespace MiniEcommerMVC.Services
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _repo;
        
        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task AddAsync(CreateProductVM model)
        {
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                ImageUrl = model.ImageUrl,
                StockQuantity = model.StockQuantity
            };

            await _repo.AddAsync(product);
        }

        public async Task<bool> UpdateAsync(CreateProductVM model)
        {
            var product = await _repo.GetByIdAsync(model.Id);

            if(product == null)
            {
                return false;
            }

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.ImageUrl = model.ImageUrl;
            product.StockQuantity = model.StockQuantity;

            await _repo.UpdateAsync(product);
            return true;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            if(product == null)
            {
                return false;
            }


            await _repo.DeleteAsync(product);
            return true;
        }


    }
}
