using AgriEnergyConnect_st10255631_MVC.Models;
using AgriEnergyConnect_st10255631_MVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; 
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks;

namespace AgriEnergyConnect_st10255631_MVC.Controllers
{
    [Authorize(Roles = "Employee")] // Only Employees can access this
    public class EmployeeController : Controller
    {
        private readonly IFarmerService _farmerService;
        private readonly IProductService _productService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(
            IFarmerService farmerService,
            IProductService productService,
            ILogger<EmployeeController> logger)
        {
            _farmerService = farmerService;
            _productService = productService;
            _logger = logger;
        }

       
        public async Task<IActionResult> Dashboard(EmployeeDashboardViewModel filterModel)
        {
            // Gets data needed for filtering options
            var farmers = await _farmerService.GetAllFarmersAsync();
            var farmerListItems = farmers
                .Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.Name })
                .ToList();
        
            farmerListItems.Insert(0, new SelectListItem { Value = "", Text = "--- All Farmers ---" });

            // Get products based on filters
           
            var productsQuery = await _productService.GetAllProductsAsync();

            // Apply filters based on the submitted filterModel
            if (filterModel.SelectedFarmerId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.FarmerId == filterModel.SelectedFarmerId.Value);
            }
            if (!string.IsNullOrWhiteSpace(filterModel.FilterProductType))
            {
                productsQuery = productsQuery.Where(p => p.Category.Equals(filterModel.FilterProductType, StringComparison.OrdinalIgnoreCase));
            }
            if (filterModel.FilterStartDate.HasValue)
            {
                //  comparison ignores time part if only date is relevant
                productsQuery = productsQuery.Where(p => p.ProductionDate.Date >= filterModel.FilterStartDate.Value.Date);
            }
            if (filterModel.FilterEndDate.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.ProductionDate.Date <= filterModel.FilterEndDate.Value.Date);
            }

            //Prepare the ViewModel for the View
            var viewModel = new EmployeeDashboardViewModel
            {
                // Populate filter options for redisplay
                AvailableFarmers = farmerListItems,
                SelectedFarmerId = filterModel.SelectedFarmerId,
                FilterProductType = filterModel.FilterProductType,
                FilterStartDate = filterModel.FilterStartDate,
                FilterEndDate = filterModel.FilterEndDate,

                // Assign the filtered products
                Products = productsQuery.ToList() // Execute the query
            };

            return View(viewModel); 
        }
        
     
        [HttpGet]
        public IActionResult AddFarmer()
        {
           
            return View(new AddFarmerViewModel()); 
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFarmer(AddFarmerViewModel model)
        {
           
            if (!ModelState.IsValid)
            {
                return View(model); // Return view with validation errors
            }

          
            var (success, errorMessage) = await _farmerService.CreateFarmerWithUserAsync(model);

            if (success)
            {
                _logger.LogInformation("New farmer {FarmerName} and user {Username} created successfully.", model.FarmerName, model.Username);
                TempData["SuccessMessage"] = $"Farmer '{model.FarmerName}' created successfully.";
                return RedirectToAction(nameof(Dashboard));
            }
            else
            {
                ModelState.AddModelError(string.Empty, errorMessage ?? "Failed to create farmer account.");
                return View("AddFarmer", model);


            }
        }
    }
}
////////////////////////////////////////////////////////////END OF FILE////////////////////////////////////////////////////////////