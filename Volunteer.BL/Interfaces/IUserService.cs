using Domain.DTOs;
using Volunteer.DAL.Models;

namespace Volunteer.BL.Interfaces
{
    public interface IUserService
    {
        Task<bool> AddUser(UserDTO userDTO);
        Task<bool> UpdateUser(UserDTO userDTO);
        Task<bool> DeleteUserById(Guid id);
        Task<User> GetUserById(Guid id);
        Task<User> GetUserByEmail(string email);
        Task<bool> SaveChangesAsync();
        Task<List<User>> GetAllUsers();
    }
}
