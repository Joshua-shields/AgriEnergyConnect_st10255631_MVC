using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect_st10255631_MVC.Models
{
    public class AddFarmerViewModel
    {
        [Required]
        [Display(Name = "Farmer Name")]
        public string FarmerName { get; set; }

        [Display(Name = "Contact Details")]
        public string? ContactDetails { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}