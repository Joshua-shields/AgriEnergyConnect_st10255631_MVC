using AgriEnergyConnect_st10255631_MVC.Models;
using AgriEnergyConnect_st10255631_MVC.Repositories;
using System.Threading.Tasks;

namespace AgriEnergyConnect_st10255631_MVC.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;


        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> ValidateCredentialsAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);

            if (user == null)
            {
                return null; // User not found
            }

          
            if (user.PasswordHash == password)
            {
                return user; // Credentials are valid
            }

            return null; // Password incorrect
        }
    }
}
////////////////////////////////////////////////////////////END OF FILE////////////////////////////////////////////////////////////