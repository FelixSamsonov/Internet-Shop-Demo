using InternetShopAspNetCoreMvc.Data.Repositories.Interfaces;
using InternetShopAspNetCoreMvc.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopAspNetCoreMvc.Data.Repositories
{
    public class UsersRepository : IUserRepository
    {
        private readonly InternetShopDbContext _context;

        public UsersRepository(InternetShopDbContext context)
        {
            _context = context;
        }
        public async Task<User> AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Id == id);

            if (user == null)
                return false;

            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            return true;
        }
        public async Task<User?> EditUserAsync(User user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(p => p.Id == user.Id);

            if (existingUser != null)
            {
                existingUser.Username = user.Username;
                existingUser.Fullname = user.Fullname;
                existingUser.Address = user.Address;
                existingUser.Email = user.Email;
                await _context.SaveChangesAsync();
            }
            return existingUser;
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new KeyNotFoundException("User not found");
        }
        public Task<bool> ExistsByEmailAsync(string email)
        {
            return _context.Users
                           .AsNoTracking()
                           .AnyAsync(u => u.Email == email);
        }
    }
}
