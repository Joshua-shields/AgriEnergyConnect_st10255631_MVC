using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AgriEnergyConnect_st10255631_MVC.Models;

public class Product
{
    [Key]
    public int Id { get; set; } // Primary Key

    [Required]
    [StringLength(150)]
    public string Name { get; set; }

    [Required]
    [StringLength(100)]
    public string Category { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime ProductionDate { get; set; }

    public DateTime AddedDate { get; set; } = DateTime.UtcNow;

    [Required]
    public int FarmerId { get; set; } // Foreign Key
    [ForeignKey("FarmerId")]
    public virtual Farmer Farmer { get; set; }
}