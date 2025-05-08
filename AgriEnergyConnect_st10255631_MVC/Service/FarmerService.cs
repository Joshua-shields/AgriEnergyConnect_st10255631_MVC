using AgriEnergyConnect_st10255631_MVC.Models;
using AgriEnergyConnect_st10255631_MVC.Repositories;
using System.Threading.Tasks;

namespace AgriEnergyConnect_st10255631_MVC.Services
{
    public class FarmerService : IFarmerService
    {
        private readonly IFarmerRepository _farmerRepository;

        public FarmerService(IFarmerRepository farmerRepository)
        {
            _farmerRepository = farmerRepository;
        }

        public async Task<Farmer?> GetFarmerByUserIdAsync(int userId)
        {
            return await _farmerRepository.GetFarmerByUserIdAsync(userId);
        }
    }
}