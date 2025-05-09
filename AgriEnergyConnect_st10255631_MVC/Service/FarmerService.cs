using System.Threading.Tasks;
using AgriEnergyConnect_st10255631_MVC.Models;
using AgriEnergyConnect_st10255631_MVC.Repositories;
using Microsoft.Extensions.Logging;

namespace AgriEnergyConnect_st10255631_MVC.Services
{
    public class FarmerService : IFarmerService
    {
        private readonly IFarmerRepository _farmerRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<FarmerService> _logger;

        public FarmerService(
            IFarmerRepository farmerRepository,
            IUserRepository userRepository,
            ILogger<FarmerService> logger)
        {
            _farmerRepository = farmerRepository;
            _userRepository = userRepository;
            _logger = logger;
        }
        
        public async Task<IEnumerable<Farmer>> GetAllFarmersAsync()
        {
            return await _farmerRepository.GetAllFarmersAsync();
        }


        public async Task<Farmer?> GetFarmerByUserIdAsync(int userId)
        {
            return await _farmerRepository.GetFarmerByUserIdAsync(userId);
        }

        public async Task<(bool Success, string? ErrorMessage)> CreateFarmerWithUserAsync(AddFarmerViewModel model)
        {
            // Check if username already exists
            var existingUser = await _userRepository.GetUserByUsernameAsync(model.Username);
            if (existingUser != null)
            {
                return (false, "Username already exists. Please choose a different username.");
            }

            var newUser = new User
            {
                Username = model.Username,
                PasswordHash = model.Password, // For prototype only!
                Role = "Farmer"
            };

            try
            {
                await _userRepository.AddUserAsync(newUser);

                var newFarmer = new Farmer
                {
                    Name = model.FarmerName,
                    ContactDetails = model.ContactDetails,
                    UserId = newUser.Id
                };
                await _farmerRepository.AddFarmerAsync(newFarmer);

                return (true, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating new farmer and user account for username {Username}", model.Username);
                return (false, "An unexpected error occurred while creating the account.");
            }
        }
    }
}
