using Microsoft.AspNetCore.Mvc.Rendering; 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect_st10255631_MVC.Models
{
    public class EmployeeDashboardViewModel
    {
        [Display(Name = "Select Farmer")]
        public int? SelectedFarmerId { get; set; } 
        public IEnumerable<SelectListItem> AvailableFarmers { get; set; } = new List<SelectListItem>();

        [Display(Name = "Product Type/Category")]
        public string? FilterProductType { get; set; } // Product type filter

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime? FilterStartDate { get; set; } // Date range start

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? FilterEndDate { get; set; } // Date range end

        // --- Data to Display ---
        public IEnumerable<Product> Products { get; set; } = new List<Product>(); // List of products to show
    }
}