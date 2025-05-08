using AgriEnergyConnect_st10255631_MVC.Models;
using AgriEnergyConnect_st10255631_MVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergyConnect_st10255631_MVC.Controllers
{
    [Authorize(Roles = "Farmer")] 
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IFarmerService _farmerService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(
            IProductService productService,
            IFarmerService farmerService,
            ILogger<ProductController> logger)
        {
            _productService = productService;
            _farmerService = farmerService;
            _logger = logger;
        }

        // Helper to get the current logged-in Farmer entity
        private async Task<Farmer?> GetCurrentFarmerAsync()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                _logger.LogWarning("User ID claim not found or invalid while trying to get current farmer.");
                return null;
            }
            // This assumes your FarmerService can fetch a Farmer by their associated User ID
            return await _farmerService.GetFarmerByUserIdAsync(userId);
        }

        // GET: /Product/FarmerProducts (This will be the Farmer's Dashboard)
        public async Task<IActionResult> FarmerProducts()
        {
            var farmer = await GetCurrentFarmerAsync();
            if (farmer == null)
            {
                _logger.LogWarning("Farmer profile not found for logged-in user. Redirecting to login.");
                // This might happen if a user with role "Farmer" doesn't have a corresponding Farmer record.
                // Or if GetFarmerByUserIdAsync returns null.
                return RedirectToAction("Login", "Account");
            }

            var products = await _productService.GetProductsForFarmerAsync(farmer.Id);
            ViewBag.FarmerName = farmer.Name; // the actual farmer's name

         
            var viewModel = new FarmerDashboardViewModel
            {
                NewProduct = new Product { ProductionDate = DateTime.Today }, 
                MyProducts = products
            };

            return View("~/Views/Home/FarmerDashboard.cshtml", viewModel);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(FarmerDashboardViewModel viewModel) // Bind to the NewProduct part of the ViewModel
        {
            var farmer = await GetCurrentFarmerAsync();
            if (farmer == null)
            {
              
                return RedirectToAction("Login", "Account");
            }

         
            var productToAdd = viewModel.NewProduct;

          
            ModelState.Remove("NewProduct.Id");
            ModelState.Remove("NewProduct.FarmerId");
            ModelState.Remove("NewProduct.Farmer");
            ModelState.Remove("NewProduct.AddedDate");
            ModelState.Remove("MyProducts"); 

            if (ModelState.IsValid && productToAdd != null) // Check if productToAdd is not null
            {
                try
                {
                    await _productService.AddProductForFarmerAsync(productToAdd, farmer.Id);
                    _logger.LogInformation("Product '{ProductName}' added for farmer {FarmerId}.", productToAdd.Name, farmer.Id);
                    TempData["SuccessMessage"] = "Product added successfully!"; // For user feedback
                    return RedirectToAction(nameof(FarmerProducts));
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex, "Error adding product for farmer {FarmerId}", farmer.Id);
                    ModelState.AddModelError("", "An error occurred while adding the product. Please try again.");
                }
            }
            else
            {
                 _logger.LogWarning("AddProduct model state invalid for farmer {FarmerId}. Errors: {Errors}",
                    farmer.Id,
                    string.Join("; ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
                 if (productToAdd == null)
                 {
                     ModelState.AddModelError("", "Product data was not received.");
                 }
            }

            // If ModelState is invalid or an error occurred, re-display the dashboard
     
            var existingProducts = await _productService.GetProductsForFarmerAsync(farmer.Id);
            ViewBag.FarmerName = farmer.Name;

            var newViewModel = new FarmerDashboardViewModel
            {
                NewProduct = productToAdd ?? new Product { ProductionDate = DateTime.Today }, // submitted or new
                MyProducts = existingProducts
            };
            return View("~/Views/Home/FarmerDashboard.cshtml", newViewModel);
        }

        // TODO: Implement Edit (GET and POST) and Delete (GET and POST) actions
        // Example for Edit (GET)
        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmer = await GetCurrentFarmerAsync();
            if (farmer == null) return RedirectToAction("Login", "Account");

            var product = await _productService.GetProductByIdAsync(id.Value);
            if (product == null || product.FarmerId != farmer.Id) // Ensure farmer owns this product
            {
                return NotFound(); // Or Access Denied
            }
           
            return View("EditProduct", product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product) // Product comes from the form
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            var farmer = await GetCurrentFarmerAsync();
            if (farmer == null) return RedirectToAction("Login", "Account");

         
            var originalProduct = await _productService.GetProductByIdAsync(id);
            if (originalProduct == null || originalProduct.FarmerId != farmer.Id)
            {
                return NotFound(); 
            }

        
            product.FarmerId = originalProduct.FarmerId;
            product.AddedDate = originalProduct.AddedDate;
            product.Farmer = null;

            ModelState.Remove("Farmer");

            if (ModelState.IsValid)
            {
                try
                {
                    await _productService.UpdateProductAsync(product); 
                    TempData["SuccessMessage"] = "Product updated successfully!";
                    return RedirectToAction(nameof(FarmerProducts));
                }
                catch (DbUpdateConcurrencyException)
                {
                 
                    ModelState.AddModelError("", "The product was modified by another user. Please try again.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating product {ProductId}", product.Id);
                    ModelState.AddModelError("", "An error occurred while updating the product.");
                }
            }
            return View("EditProduct", product); 
        }
    }
}
