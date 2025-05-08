using AgriEnergyConnect_st10255631_MVC.Data;
using AgriEnergyConnect_st10255631_MVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AgriEnergyConnect_st10255631_MVC.Repositories
{
    public class FarmerRepository : IFarmerRepository
    {
        private readonly ApplicationDbContext _context;

        public FarmerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Farmer?> GetFarmerByUserIdAsync(int userId)
        {
      
            return await _context.Farmers
                .FirstOrDefaultAsync(f => f.UserId == userId);
        }

        public async Task AddFarmerAsync(Farmer farmer)
        {
            _context.Farmers.Add(farmer);
            await _context.SaveChangesAsync();
        }
    }
}