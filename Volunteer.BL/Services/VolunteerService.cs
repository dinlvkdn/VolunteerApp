using Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using Volunteer.BL.Interfaces;
using Volunteer.DAL.DataAccess;

namespace Volunteer.BL.Services
{
    public class VolunteerService : IVolunteerService
    {
        private readonly VolunteerDBContext _dbContext;

        public VolunteerService(VolunteerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddVolunteer(VolunteerDTO volunteerDTO)
        {
            var volunteer = new DAL.Models.Volunteer
            {
                FirstName = volunteerDTO.FirstName,
                LastName = volunteerDTO.LastName,
                DateOfBirth = volunteerDTO.DateOfBirth,
                Description = volunteerDTO.Description,
                Id = volunteerDTO.IdVolunteer
            };

            var createVolunteerResult = await _dbContext.Volunteers.AddAsync(volunteer);
            if (createVolunteerResult != null)
            {

            }
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteVolunteer(Guid volunteerId)
        {
            var volunteer = await GetVolunteerById(volunteerId);
            if (volunteer == null)
            {
                return false;
            }
            _dbContext.Volunteers.Remove(volunteer);
            return await SaveChangesAsync();
        }

        public async Task<DAL.Models.Volunteer> GetVolunteerById(Guid id)
        {
            return await _dbContext.Volunteers.FindAsync(id);
        }

        public async Task<bool> UpdateVolunteer(VolunteerDTO volunteerDTO)
        {
            var volunteer = await _dbContext.Volunteers.FindAsync(volunteerDTO.IdVolunteer);

            if (volunteer == null)
            {
                return false; 
            }

            volunteer.FirstName = volunteerDTO.FirstName;
            volunteer.LastName = volunteerDTO.LastName;
            volunteer.DateOfBirth = volunteerDTO.DateOfBirth;
            volunteer.Description = volunteerDTO.Description;

            _dbContext.Volunteers.Update(volunteer);
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            var save = await _dbContext.SaveChangesAsync();
            return save > 0 ? true : false;
        }
    }
}
