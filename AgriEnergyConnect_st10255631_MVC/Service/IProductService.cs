using AgriEnergyConnect_st10255631_MVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgriEnergyConnect_st10255631_MVC.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsForFarmerAsync(int farmerId);
        Task AddProductForFarmerAsync(Product product, int farmerId);
        Task<Product?> GetProductByIdAsync(int productId);
        Task<IEnumerable<Product>> GetAllProductsForEmployeeAsync(
            string? productType, DateTime? startDate, DateTime? endDate);

      
        Task UpdateProductAsync(Product product);
    }
}