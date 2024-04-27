using Domain;
using Domain.DTOs;
using Domain.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using Volunteer.BL.Helper.Exceptions;
using Volunteer.BL.Interfaces;
using Volunteer.DAL.DataAccess;
using Volunteer.DAL.Models;

namespace Volunteer.BL.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly VolunteerDBContext _dbContext;

        public OrganizationService(VolunteerDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<OrganizationInfoDTO> AddOrganization(Guid id,OrganizationInfoDTO organizationInfoDTO)
        {
            var organizationExists = await GetOrganizationById(id);
            if (organizationExists != null)
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Title = "Organization already exists",
                    Detail = "A organization with this id already exists"
                };
            }

            var organization = new Organization
            {
                Id = id,
                Name = organizationInfoDTO.Name,
                YearOfFoundation = organizationInfoDTO.YearOfFoundation,
                Description = organizationInfoDTO.Description
            };

            await _dbContext.Organizations.AddAsync(organization);

            await _dbContext.SaveChangesAsync();

            return organizationInfoDTO;
        }

        public async Task<bool> DeleteOrganization(Guid IdOrganization)
        {
            var organization = await GetOrganizationById(IdOrganization);

            if (organization == null)
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Title = "No organization exists",
                    Detail = "Organization does`n exist in the database"
                };
            }
            _dbContext.Organizations.Remove(organization);

            return await SaveChangesAsync();
        }

        public async Task<Organization> GetOrganizationById(Guid id)
        {
            return await _dbContext.Organizations.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<OrganizationInfoDTO> GetOrganizationInfo(Guid id)
        {
            var  organization = await GetOrganizationById(id);
            if (organization == null)
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Title = "No organization exists",
                    Detail = "Organiztion does`n exist in the database"
                };
            }
            return new OrganizationInfoDTO
            {
                Description = organization.Description,
                Name = organization.Name,
                YearOfFoundation = organization.YearOfFoundation
            };
        }

        public async Task<OrganizationInfoDTO> UpdateOrganization(Guid id, OrganizationInfoDTO organizationInfoDTO)
        {
            var organization = await GetOrganizationById(id);

            organization.Name = organizationInfoDTO.Name;
            organization.YearOfFoundation = organizationInfoDTO.YearOfFoundation;
            organization.Description = organizationInfoDTO.Description;

            _dbContext.Organizations.Update(organization);

            await _dbContext.SaveChangesAsync();

            return organizationInfoDTO;
        }

        public async Task<bool> SaveChangesAsync()
            => await _dbContext.SaveChangesAsync() > 0;

        public async Task<bool> ConfirmVolunteerOnJobOffer(Guid jobOfferId, Guid volunteerId)
        {
            var requestForJobOffer = await _dbContext.VolunteerJobOffers.FirstOrDefaultAsync(i => i.JobOfferId == jobOfferId && i.VolunteerId == volunteerId);

            requestForJobOffer.Status = StatusRequest.confirm;

            var member = new Member
            {
                Id = Guid.NewGuid(),
                JobOfferId = jobOfferId,
                VolunteerId = volunteerId
            };

            await _dbContext.Members.AddAsync(member);

            _dbContext.VolunteerJobOffers.Update(requestForJobOffer);
            return await SaveChangesAsync();
        }

        public async Task<bool> CancelVolunteerJobOfferRequest(Guid jobOfferId, Guid volunteerId)
        {
            var requestForJobOffer = await _dbContext.VolunteerJobOffers.FirstOrDefaultAsync(i => i.JobOfferId == jobOfferId && i.VolunteerId == volunteerId);

            requestForJobOffer.Status = StatusRequest.rejected;

            _dbContext.VolunteerJobOffers.Update(requestForJobOffer);
            return await SaveChangesAsync();
        }

        public async Task<PagedPesponse<List<FeedbackPaginationDTO>>> GetListFeedbacks(PaginationFilter filter, CancellationToken cancellationToken)
        {
            var query = _dbContext.Feedbacks
                .Include(t => t.Member)
                .ThenInclude(t => t.Volunteer)
                .Where(t => t.OrganizationId == filter.OrganizationId);


            var countRecords = await query
                .CountAsync(cancellationToken);

            query = query
                .Skip(filter.PageNumber * filter.PageSize)
                .Take(filter.PageSize);



            var result = await query
                .Select(p => new FeedbackPaginationDTO
                {
                    Id = p.Id,
                    Comment = p.Description,
                    Rating = p.Rating
                })
                .ToListAsync(cancellationToken);

            return new PagedPesponse<List<FeedbackPaginationDTO>>(
                result,
                countRecords,
                filter.PageNumber, filter.PageSize);
        }
    }
}
