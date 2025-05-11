using AgriEnergyConnect_st10255631_MVC.Models;
using AgriEnergyConnect_st10255631_MVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgriEnergyConnect_st10255631_MVC.Services
{
 
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task AddProductForFarmerAsync(Product product, int farmerId)
        {
            product.FarmerId = farmerId;
            product.AddedDate = DateTime.UtcNow;
            await _productRepository.AddProductAsync(product);
        }

        public async Task<IEnumerable<Product>> GetProductsForFarmerAsync(int farmerId)
        {
            return await _productRepository.GetProductsByFarmerIdAsync(farmerId);
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await _productRepository.GetProductByIdAsync(productId);
        }

        public async Task<IEnumerable<Product>> GetAllProductsForEmployeeAsync(
            string? productType, DateTime? startDate, DateTime? endDate)
        {
            var allProducts = await _productRepository.GetAllProductsAsync();

            if (!string.IsNullOrWhiteSpace(productType))
            {
                allProducts = allProducts.Where(p =>
                    p.Category.Equals(productType, StringComparison.OrdinalIgnoreCase));
            }
            if (startDate.HasValue)
            {
                allProducts = allProducts.Where(p => p.ProductionDate >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                allProducts = allProducts.Where(p => p.ProductionDate <= endDate.Value);
            }
            return allProducts;
        }

   
        public async Task UpdateProductAsync(Product product)
        {
           
            await _productRepository.UpdateProductAsync(product);
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }
        public async Task DeleteProductAsync(int productId)
        {
            await _productRepository.DeleteProductAsync(productId);
        }


    }
}
////////////////////////////////////////////////////////////END OF FILE////////////////////////////////////////////////////////////

