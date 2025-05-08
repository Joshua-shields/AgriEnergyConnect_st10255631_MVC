using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect_st10255631_MVC.Models
{
    public class FarmerDashboardViewModel
    {

        public Product NewProduct { get; set; } = new Product();

       
        public IEnumerable<Product> MyProducts { get; set; } = new List<Product>();
    }
}