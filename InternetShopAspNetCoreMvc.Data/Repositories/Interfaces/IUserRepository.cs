using InternetShopAspNetCoreMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopAspNetCoreMvc.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> AddUserAsync(User user);
        Task<User?> EditUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> ExistsByEmailAsync(string email);
    }
}
