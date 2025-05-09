using AgriEnergyConnect_st10255631_MVC.Models;
using System.Threading.Tasks;

namespace AgriEnergyConnect_st10255631_MVC.Services
{
    public interface IFarmerService
    {
        Task<Farmer?> GetFarmerByUserIdAsync(int userId);
        
        
      
        Task<IEnumerable<Farmer>> GetAllFarmersAsync();
        Task<(bool Success, string? ErrorMessage)> CreateFarmerWithUserAsync(AddFarmerViewModel model);

    }
}