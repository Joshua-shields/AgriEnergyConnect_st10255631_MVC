using AgriEnergyConnect_st10255631_MVC.Models;
using System.Threading.Tasks;

namespace AgriEnergyConnect_st10255631_MVC.Services
{
    public interface IAccountService
    {
        Task<User?> ValidateCredentialsAsync(string username, string password);
     
    }
}
////////////////////////////////////////////////////////////END OF FILE////////////////////////////////////////////////////////////