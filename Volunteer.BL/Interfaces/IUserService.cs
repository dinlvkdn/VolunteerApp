using Domain.DTOs;

namespace Volunteer.BL.Interfaces
{
    public interface IUserService
    {
        public Task<bool> GetUserById(Guid id, string RoleName);
    }
}
