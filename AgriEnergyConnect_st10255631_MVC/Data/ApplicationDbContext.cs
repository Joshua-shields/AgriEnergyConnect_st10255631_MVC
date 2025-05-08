

using AgriEnergyConnect_st10255631_MVC.Models; // Or your correct models namespace
// using AgriEnergyConnect.Models; // Remove this if the one above is correct
using Microsoft.EntityFrameworkCore;

namespace AgriEnergyConnect_st10255631_MVC.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Farmer> Farmers { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasOne(u => u.FarmerProfile)
            .WithOne(f => f.User)
            .HasForeignKey<Farmer>(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Farmer>()
            .HasMany(f => f.Products)
            .WithOne(p => p.Farmer)
            .HasForeignKey(p => p.FarmerId)
            .OnDelete(DeleteBehavior.Cascade);

        SeedInitialData(modelBuilder);
    }

    private void SeedInitialData(ModelBuilder modelBuilder)
    {
   
        var employeeUser = new User
        {
            Id = 1,
            Username = "employee01",
            PasswordHash = "EmpP@ss123", // Storing plain text
            Role = "Employee"
        };
        modelBuilder.Entity<User>().HasData(employeeUser);

        var farmerUser1 = new User
        {
            Id = 2,
            Username = "farmerJohn",
            PasswordHash = "FarmP@ss123",
            Role = "Farmer"
        };
        modelBuilder.Entity<User>().HasData(farmerUser1);

        var farmerUser2 = new User
        {
            Id = 3,
            Username = "farmerJane",
            PasswordHash = "FarmP@ss456", 
            Role = "Farmer"
        };
        modelBuilder.Entity<User>().HasData(farmerUser2);
        
        modelBuilder.Entity<Farmer>().HasData(
            new Farmer
            {
                Id = 1,
                Name = "John's Sunny Acres",
                ContactDetails = "john@sunnyacres.com, 555-0101",
                UserId = farmerUser1.Id
            },
            new Farmer
            {
                Id = 2,
                Name = "Jane's Green Fields",
                ContactDetails = "jane@greenfields.org, 555-0202",
                UserId = farmerUser2.Id
            }
        );

   
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Name = "Solar Panel",
                Category = "Outdoor",
                ProductionDate = new DateTime(2025, 4, 15),
                FarmerId = 1,
                AddedDate = DateTime.UtcNow.AddDays(-10)
            },
            new Product
            {
                Id = 2,
                Name = "Free-Range Eggs",
                Category = "Poultry",
                ProductionDate = new DateTime(2025, 5, 1),
                FarmerId = 1,
                AddedDate = DateTime.UtcNow.AddDays(-5)
            },
            new Product
            {
                Id = 3,
                Name = "Artisan Bread",
                Category = "Bakery",
                ProductionDate = new DateTime(2025, 5, 5),
                FarmerId = 2,
                AddedDate = DateTime.UtcNow.AddDays(-2)
            },
            new Product
            {
                Id = 4,
                Name = "Fresh Strawberries",
                Category = "Fruit",
                ProductionDate = new DateTime(2025, 5, 3),
                FarmerId = 2,
                AddedDate = DateTime.UtcNow.AddDays(-3)
            }
        );
    }
}
