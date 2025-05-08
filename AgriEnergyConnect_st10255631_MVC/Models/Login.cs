using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect_st10255631_MVC.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
