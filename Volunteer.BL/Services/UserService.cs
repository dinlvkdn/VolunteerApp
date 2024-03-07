using Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using Volunteer.BL.Interfaces;
using Volunteer.DAL.DataAccess;
using Volunteer.DAL.Models;

namespace Volunteer.BL.Services
{
    public class UserService : IUserService
    {
        private readonly VolunteerDBContext _dbContext;

        public UserService(VolunteerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddUser(UserDTO userDTO)
        {
            var user = new User
            {
                Name = userDTO.Name,
                Email = userDTO.Email,
            };
            await _dbContext.Users.AddAsync(user);
            return await SaveChangesAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbContext.Users.FindAsync(email);
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<bool> UpdateUser(UserDTO userDTO)
        {
            var user = await _dbContext.Users.Where(e => e.Email == userDTO.Email).FirstOrDefaultAsync();
            
            if(user == null)
            {
                return false;
            }

            user = new User()
            {
                Name = userDTO.Name,
                Email = userDTO.Email,
            };

            _dbContext.Users.Update(user);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteUserById(Guid id)
        {
            var user = await GetUserById(id);
            if (user == null)
            {
                return false;
            }
            _dbContext.Users.Remove(user);
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            var save = await _dbContext.SaveChangesAsync();
            return save > 0 ? true : false;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }
    }
}
