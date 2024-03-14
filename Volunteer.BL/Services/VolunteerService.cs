using Domain.DTOs;
using Microsoft.EntityFrameworkCore;
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

        public async Task<VolunteerInfoDTO> AddVolunteer(Guid id,VolunteerInfoDTO volunteerInfoDTO)
        {
            var volunteer = new DAL.Models.Volunteer
            {
                Id = id,
                DateOfBirth = volunteerInfoDTO.DateOfBirth,
                Description = volunteerInfoDTO.Description,
                FirstName = volunteerInfoDTO.FirstName,
                LastName  = volunteerInfoDTO.LastName
            };

            await _dbContext.Volunteers.AddAsync(volunteer);
            await _dbContext.SaveChangesAsync();

            return volunteerInfoDTO;
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
            return await _dbContext.Volunteers.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<bool> UpdateVolunteer(VolunteerInfoDTO volunteerInfoDTO)
        {
            //var volunteer = await GetVolunteerById(volunteerInfoDTO.Id);

            //if (volunteer == null)
            //{
            //    return false; 
            //}

            //volunteer.FirstName = volunteerResumeDTO.FirstName;
            //volunteer.LastName = volunteerResumeDTO.LastName;
            //volunteer.Description = volunteerResumeDTO.Description;
            


            //_dbContext.Volunteers.Update(volunteer);


            //var resume = await _dbContext.Resumes.FirstOrDefaultAsync(r => r.VolunteerId == volunteer.Id);
            //if (resume != null)
            //{
            //    resume.FileUrl = volunteerResumeDTO.FileUrl;
            //    resume.FileName = volunteerResumeDTO.FileName;
            //    _dbContext.Resumes.Update(resume);
            //}


            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
            => await _dbContext.SaveChangesAsync() > 0;
    }
}
