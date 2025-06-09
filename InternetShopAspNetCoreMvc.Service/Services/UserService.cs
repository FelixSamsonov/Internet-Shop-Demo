using InternetShopAspNetCoreMvc.Data.Repositories.Interfaces;
using InternetShopAspNetCoreMvc.Models;
using InternetShopAspNetCoreMvc.Service.Interfaces;


namespace InternetShopAspNetCoreMvc.Service.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            if (await _userRepository.ExistsByEmailAsync(user.Email))
                throw new InvalidOperationException("User with this email already exists.");
            var newUser = new User
            {
                Username = user.Username,
                Fullname = user.Fullname,
                Email = user.Email,
                Address = user.Address,
                CreatedAt = DateTime.UtcNow
            };
            await _userRepository.AddUserAsync(newUser);
            return newUser;
        }

        public async Task<User?> UpdateUserAsync(User user)
        {
            return await _userRepository.EditUserAsync(user);
        }
        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

    }
}
