using System.Collections.Generic; // Added for ICollection
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AgriEnergyConnect_st10255631_MVC.Models
{
    public class Farmer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(500)]
        public string? ContactDetails { get; set; }

        // Foreign Key to User
        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; } 

        // Navigation property: A farmer can have many products
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}

////////////////////////////////////////////////////////////END OF FILE////////////////////////////////////////////////////////////