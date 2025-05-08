using AgriEnergyConnect_st10255631_MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgriEnergyConnect_st10255631_MVC.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetProductByIdAsync(int productId);
        Task<IEnumerable<Product>> GetProductsByFarmerIdAsync(int farmerId);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int productId);
        Task<IEnumerable<Product>> GetAllProductsAsync(); // For employee view
    }
}