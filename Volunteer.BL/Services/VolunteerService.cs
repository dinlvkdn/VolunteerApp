using Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Volunteer.BL.Helper.Exceptions;
using Volunteer.BL.Interfaces;
using Volunteer.DAL.DataAccess;
using Volunteer.DAL.Models;

namespace Volunteer.BL.Services
{
    public class VolunteerService : IVolunteerService
    {
        private readonly VolunteerDBContext _dbContext;

        public VolunteerService(VolunteerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<VolunteerInfoDTO> AddVolunteer(Guid id,VolunteerInfoDTO volunteerDTO)
        {
            var volunteerExists = await GetVolunteerById(id);
            if(volunteerExists != null)
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Title = "Volunteer already exists",
                    Detail = "A volunteer with this id already exists"
                };
            }

            var volunteer = new DAL.Models.Volunteer
            {
                Id = id,
                DateOfBirth = volunteerDTO.DateOfBirth,
                Description = volunteerDTO.Description,
                FirstName = volunteerDTO.FirstName,
                LastName  = volunteerDTO.LastName
            };

            await _dbContext.Volunteers.AddAsync(volunteer);
            await _dbContext.SaveChangesAsync();

            return volunteerDTO;
        }

        public async Task<bool> DeleteVolunteer(Guid volunteerId)
        {
            var volunteer = await GetVolunteerById(volunteerId);

            _dbContext.Volunteers.Remove(volunteer);

            return await SaveChangesAsync();
        }

        public async Task<DAL.Models.Volunteer> GetVolunteerById(Guid id)
        {
            return await _dbContext.Volunteers.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<VolunteerShortInfoDTO> UpdateVolunteer(Guid  id, VolunteerShortInfoDTO volunteerDTO)
        {
            var volunteer = await GetVolunteerById(id);

            volunteer.FirstName = volunteerDTO.FirstName;
            volunteer.LastName = volunteerDTO.LastName;
            volunteer.Description = volunteerDTO.Description;

            _dbContext.Volunteers.Update(volunteer);

            await _dbContext.SaveChangesAsync();

            return volunteerDTO;
        }

        public async Task<bool> SaveChangesAsync()
            => await _dbContext.SaveChangesAsync() > 0;
    }
}
