using AgriEnergyConnect_st10255631_MVC.Models;
using System.Threading.Tasks;

namespace AgriEnergyConnect_st10255631_MVC.Repositories
{
    public interface IFarmerRepository
    {
        Task<Farmer?> GetFarmerByUserIdAsync(int userId);
        Task AddFarmerAsync(Farmer farmer);
        
        Task<IEnumerable<Farmer>> GetAllFarmersAsync();


    }
}