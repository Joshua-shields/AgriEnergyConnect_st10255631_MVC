using AgriEnergyConnect_st10255631_MVC.Models;
using System.Threading.Tasks;

namespace AgriEnergyConnect_st10255631_MVC.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User?> GetUserByIdAsync(int userId); 
     
    }
}