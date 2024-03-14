using Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using Volunteer.BL.Interfaces;
using Volunteer.DAL.DataAccess;
using Volunteer.DAL.Models;

namespace Volunteer.BL.Services
{
    public class JobOfferService : IJobOfferService
    {
        private readonly VolunteerDBContext _dbContext;

        public JobOfferService(VolunteerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateJobOffer(JobOfferInfoDTO jobOfferInfoDTO)
        {
            if (jobOfferInfoDTO == null)
            {
                return false;
            }
            var jobOffer = new JobOffer
            {
                Title = jobOfferInfoDTO.JobOfferTitle,
                Street = jobOfferInfoDTO.JobOfferStreet,
                City = jobOfferInfoDTO.JobOfferCity,
                Country = jobOfferInfoDTO.JobOfferCountry,
                Description = jobOfferInfoDTO.Description,
                DateTime = jobOfferInfoDTO.DateTime
            };
            await _dbContext.JobOffer.AddAsync(jobOffer);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteJobOfferById(Guid id)
        {
            var jobOffer = await _dbContext.JobOffer.FindAsync(id);
            if (jobOffer == null)
            {
                return false;
            }
            _dbContext.JobOffer.Remove(jobOffer);
            return await SaveChangesAsync();
        }

        public async Task<List<JobOffer>> GetAllJobOffers()
        {
            return await _dbContext.JobOffer.ToListAsync();
        }

        public async Task<JobOffer> GetGobOfferById(Guid id)
        {
            return await _dbContext.JobOffer.FindAsync(id);
        }

        public async Task<bool> UpdateJobOffer(JobOfferInfoDTO jobOfferInfoDTO)
        {
            var jobOffer = await _dbContext.JobOffer.FindAsync(jobOfferInfoDTO.Id);

            if (jobOffer == null)
            {
                return false; 
            }

            jobOffer.Title = jobOfferInfoDTO.JobOfferTitle;
            jobOffer.Street = jobOfferInfoDTO.JobOfferStreet;
            jobOffer.City = jobOfferInfoDTO.JobOfferCity;
            jobOffer.Country = jobOfferInfoDTO.JobOfferCountry;
            jobOffer.Description = jobOfferInfoDTO.Description;
            jobOffer.DateTime = jobOfferInfoDTO.DateTime;

            _dbContext.JobOffer.Update(jobOffer);
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
             => await _dbContext.SaveChangesAsync() > 0;
    }
}
