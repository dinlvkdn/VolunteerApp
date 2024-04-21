using Domain;
using Domain.DTOs;
using Domain.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
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

        public async Task<CreateJobOfferDTO> CreateJobOffer(Guid organizationId, CreateJobOfferDTO jobOfferDTO)
        {

            var jobOffer = new JobOffer
            {
                Id = Guid.NewGuid(),
                OrganizationId = organizationId,
                Title = jobOfferDTO.JobOfferTitle,
                Street = jobOfferDTO.JobOfferStreet,
                City = jobOfferDTO.JobOfferCity,
                Country = jobOfferDTO.JobOfferCountry,
                Description = jobOfferDTO.Description,
                DateTime = jobOfferDTO.DateTime
            };

            await _dbContext.JobOffer.AddAsync(jobOffer);

            await _dbContext.SaveChangesAsync();

            return jobOfferDTO;
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

        public async Task<PagedPesponse<List<JobOfferPaginationDTO>>> GetAllJobOffers(PaginationFilter filter, CancellationToken cancellationToken)
        {
            IQueryable<JobOffer> query = _dbContext.JobOffer;

            if (filter.OrganizationId != null)
            {
                query = query.Where(o => o.OrganizationId == filter.OrganizationId);
            }

            if(filter.SearchCriteria != null)
            {
                query = query.Where(s => s.Title.Contains(filter.SearchCriteria));
            }

            query = filter.SortColumn switch
            {
                "DateTime" when filter.SortDirection == "asc" => query
                    .OrderBy(p => p.DateTime)
                    .ThenBy(t => t.Id),
                "DateTime" => query.OrderByDescending(p => p.DateTime)
                                   .ThenBy(t => t.Id),
                _ => query
            };

            var countRecords = await query
                .CountAsync(cancellationToken);

            query = query
                .Skip(filter.PageNumber * filter.PageSize)
                .Take(filter.PageSize);

            

            var result = await query
                .Select(p => new JobOfferPaginationDTO
                {
                    Id = p.Id,
                    JobOfferTitle = p.Title,
                    JobOfferStreet = p.Street,
                    JobOfferCity = p.City,
                    JobOfferCountry = p.Country,
                    DateTime = p.DateTime,
                })
                .ToListAsync(cancellationToken);

                return new PagedPesponse<List<JobOfferPaginationDTO>>(
                    result, 
                    countRecords, 
                    filter.PageNumber, filter.PageSize);
        }

        public async Task<ShowJobOfferDTO> GetGobOfferInfo(Guid id)
        {
            var jobOffer = await _dbContext.JobOffer.FirstOrDefaultAsync(i => i.Id == id);
            var organization = await _dbContext.Organizations
                .FirstOrDefaultAsync(o => o.Id == jobOffer.OrganizationId);


            if (jobOffer == null)
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Title = "JobOffer doesn't exist",
                    Detail = "No jobOffer on database"
                };
            }

            return new ShowJobOfferDTO
            {
                Id = id,
                DateTime = jobOffer.DateTime,
                Description = jobOffer.Description,
                JobOfferCity = jobOffer.City,
                JobOfferCountry = jobOffer.Country,
                JobOfferStreet = jobOffer.Street,
                JobOfferTitle = jobOffer.Title,
                OrganizationId = organization.Id,
                OrganizationName = organization.Name
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

        //public async Task<List<AllRequestFromVolunteerDTO>> GetAllRequestFromVolunteer(Guid organizationId, Guid jobOfferId)
        //{
        //    var resultDTO = await _dbContext.VolunteerJobOffers
        //        .Include(t => t.Volunteer)
        //        .Where(t => t.JobOfferId == jobOfferId)
        //        .Where(t => t.Status == StatusRequest.unapprove)
        //        .Select(v => new AllRequestFromVolunteerDTO()
        //        {
        //            FirstName = v.Volunteer.FirstName,
        //            LastName = v.Volunteer.LastName,
        //            JobOfferId = v.JobOfferId
        //        })
        //        .ToListAsync();

        //    return resultDTO;
        //}

        public async Task<PagedPesponse<List<JobOfferRequestDTO>>> GetJobOfferRequests(Guid volunteerId, PaginationFilter filter, CancellationToken cancellationToken)
        {
            var jobOffersByVolunteerQuery = _dbContext.VolunteerJobOffers
                .Include(t => t.JobOffer)
                .Where(t => t.VolunteerId == volunteerId);

            var countRecords = await jobOffersByVolunteerQuery
                .CountAsync(cancellationToken);

            jobOffersByVolunteerQuery = jobOffersByVolunteerQuery
                .Skip(filter.PageNumber * filter.PageSize)
                .Take(filter.PageSize);

            var result = await jobOffersByVolunteerQuery
                .Select(p => new JobOfferRequestDTO
                {
                    Id = p.JobOfferId,
                    JobOfferTitle = p.JobOffer.Title,
                    JobOfferStreet = p.JobOffer.Street,
                    JobOfferCity = p.JobOffer.City,
                    JobOfferCountry = p.JobOffer.Country,
                    DateTime = p.JobOffer.DateTime,
                    Status = p.Status
                })
                .ToListAsync(cancellationToken);

            return new PagedPesponse<List<JobOfferRequestDTO>>(
                result,
                countRecords,
                filter.PageNumber, filter.PageSize);
        }

        public async Task<PagedPesponse<List<VolunteerRequestsDTO>>> GetRequestsFromVolunteers(Guid organizationId, PaginationFilter filter, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;

            var jobOffersByOrganizationQuery = _dbContext.JobOffer
                .Include(t => t.VolunteerJobOffers)
                .ThenInclude(t => t.Volunteer)
                .Where(t => t.OrganizationId == organizationId && t.DateTime >= now)
                .OrderBy(t => t.DateTime)
                .SelectMany(t => t.VolunteerJobOffers);

            var countRecords =  await jobOffersByOrganizationQuery
                .CountAsync(cancellationToken);

            jobOffersByOrganizationQuery = jobOffersByOrganizationQuery
                .Skip(filter.PageNumber * filter.PageSize)
               .Take(filter.PageSize);

            var result = await jobOffersByOrganizationQuery
               .Select(p => new VolunteerRequestsDTO
               {
                   Id = p.Volunteer.Id,
                   FirstName = p.Volunteer.FirstName,
                   LastName = p.Volunteer.LastName,
                   Description = p.Volunteer.Description,
                   DateOfBirth = p.Volunteer.DateOfBirth,
                   TitleJobOffer = p.JobOffer.Title,
                   JobOfferId = p.JobOffer.Id,
                   Status = p.Status
               })
               .ToListAsync(cancellationToken);

            return new PagedPesponse<List<VolunteerRequestsDTO>>(
                result,
                countRecords,
                filter.PageNumber, filter.PageSize);
        }

        public async Task<StatusRequest> GetOfferStatus(Guid volunteerId, Guid offerId)
        {
            var status = await _dbContext.VolunteerJobOffers
                 .Where(t => t.VolunteerId == volunteerId && t.JobOfferId == offerId).FirstAsync();
            
            if (status == null)
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Title = "No record in database",
                    Detail = "There is no record in the database with such ids"
                };
            }

            return status.Status;
        }
    }
}
