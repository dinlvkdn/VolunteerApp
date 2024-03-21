using Domain;
using Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Volunteer.BL.Helper.Exceptions;
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

        public async Task<JobOfferInfoDTO> CreateJobOffer(Guid id,JobOfferInfoDTO jobOfferInfoDTO)
        {
            var jobOfferExists = await GetGobOfferById(jobOfferInfoDTO.Id);

            if (jobOfferExists != null)
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Title = "JobOffer already exists",
                    Detail = "A jobOffer with this id already exists"
                };
            }

            var jobOffer = new JobOffer
            {
                Id = Guid.NewGuid(),
                OrganizationId = id,
                Title = jobOfferInfoDTO.JobOfferTitle,
                Street = jobOfferInfoDTO.JobOfferStreet,
                City = jobOfferInfoDTO.JobOfferCity,
                Country = jobOfferInfoDTO.JobOfferCountry,
                Description = jobOfferInfoDTO.Description,
                DateTime = jobOfferInfoDTO.DateTime
            };

            await _dbContext.JobOffer.AddAsync(jobOffer);

            await _dbContext.SaveChangesAsync();

            return jobOfferInfoDTO;
        }

        public async Task<bool> DeleteJobOfferById(Guid id)
        {
            var jobOffer = await _dbContext.JobOffer.FindAsync(id);
            if (jobOffer == null)
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Title = "JobOffer doesn't exist",
                    Detail = "No jobOffer on database"
                };
            }
            _dbContext.JobOffer.Remove(jobOffer);
            return await SaveChangesAsync();
        }

        public async Task<List<JobOffer>> GetAllJobOffers()
        {
            return await _dbContext.JobOffer.ToListAsync();
        }

        public async Task<JobOfferInfoDTO> GetGobOfferInfo(Guid id)
        {
            var jobOffer = await _dbContext.JobOffer.FirstOrDefaultAsync(i => i.Id == id);
            if (jobOffer == null)
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Title = "JobOffer doesn't exist",
                    Detail = "No jobOffer on database"
                };
            }

            return new JobOfferInfoDTO
            {
                Id = id,
                DateTime = jobOffer.DateTime,
                Description = jobOffer.Description,
                JobOfferCity = jobOffer.City,
                JobOfferCountry = jobOffer.Country,
                JobOfferStreet = jobOffer.Street,
                JobOfferTitle = jobOffer.Title
            };
        }
        public async Task<JobOffer> GetGobOfferById(Guid id)
        {
            return await _dbContext.JobOffer.FirstOrDefaultAsync(i => i.Id == id);
        }


        public async Task<JobOfferInfoDTO> UpdateJobOffer(JobOfferInfoDTO jobOfferInfoDTO)
        {
            var jobOffer = await GetGobOfferById(jobOfferInfoDTO.Id);

            if (jobOffer == null)
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Title = "JobOffer doesn't exist",
                    Detail = "No jobOffer on database"
                };
            }

            jobOffer.Title = jobOfferInfoDTO.JobOfferTitle;
            jobOffer.Street = jobOfferInfoDTO.JobOfferStreet;
            jobOffer.City = jobOfferInfoDTO.JobOfferCity;
            jobOffer.Country = jobOfferInfoDTO.JobOfferCountry;
            jobOffer.Description = jobOfferInfoDTO.Description;
            jobOffer.DateTime = jobOfferInfoDTO.DateTime;

            _dbContext.JobOffer.Update(jobOffer);
            await _dbContext.SaveChangesAsync();

            return jobOfferInfoDTO;
        }

        public async Task<bool> SaveChangesAsync()
             => await _dbContext.SaveChangesAsync() > 0;

        public async Task<List<AllRequestFromVolunteerDTO>> GetAllRequestFromVolunteer(Guid organizationId, Guid jobOfferId)
        {
            var resultDTO = await _dbContext.VolunteerJobOffers
                .Include(t => t.Volunteer)
                .Where(t => t.JobOfferId == jobOfferId)
                .Where(t => t.Status == StatusRequest.unapprove)
                .Select(v => new AllRequestFromVolunteerDTO()
                {
                    FirstName = v.Volunteer.FirstName,
                    LastName = v.Volunteer.LastName,
                    JobOfferId = v.JobOfferId
                })
                .ToListAsync();

            //var jobOffers = await _dbContext.JobOffer.Where(i => i.OrganizationId == organizationId).ToListAsync();
            //var volDTO = jobOffers.Select(jo => _dbContext.VolunteerJobOffers.Select(x => x.JobOfferId == jo.Id && x.Status == "unapprove").ToList());

            //var volunteerDTO = await _dbContext.JobOffer
            //    .Include(x => x.VolunteerJobOffers)
            //    .ThenInclude(x => x.Volunteer)
            //    .Where(i => i.OrganizationId == organizationId && i.Id == Guid.Empty)
            //    .SelectMany(x => x.VolunteerJobOffers
            //    .Where(s => s.Status == "unapprove")
            //    .Select(v => new AllRequestFromVolunteerDTO()
            //    {
            //        FirstName = v.Volunteer.FirstName,
            //        LastName = v.Volunteer.LastName,
            //        JobOfferId = v.JobOfferId
            //    })
            //    .ToList()).ToListAsync();
            return resultDTO;
        }
    }
}
