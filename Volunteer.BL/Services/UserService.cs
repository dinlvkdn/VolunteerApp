using Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Volunteer.BL.Helper.Exceptions;
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

        public async Task<bool> GetUserById(Guid id, String roleName)
        {
            object user = null;

            if (roleName =="Volunteer")
            {
                user = await _dbContext.Volunteers.FirstOrDefaultAsync(i => i.Id == id);
            }
            else if (roleName =="Organization")
            {
                user = await _dbContext.Organizations.FirstOrDefaultAsync(i => i.Id == id);
            }

            if (user == null)
            {
                return false;
            }
            return true;
        }
    }
}
