using Domain;
using Domain.DTOs;
using Domain.Pagination;
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

            if (volunteer == null)
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Title = "No volunteer exists",
                    Detail = "Volunteer does`n exist in the database"
                };
            }

            _dbContext.Volunteers.Remove(volunteer);

            return await SaveChangesAsync();
        }

        public async Task<DAL.Models.Volunteer> GetVolunteerById(Guid id)
        {
            return await _dbContext.Volunteers.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<VolunteerInfoDTO> GetVolunteerInfo(Guid id)
        {
            var volunteer = await GetVolunteerById(id);
            if (volunteer == null)
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Title = "No volunteer exists",
                    Detail = "Volunteer does`n exist in the database"
                };
            }
            return new VolunteerInfoDTO
            {
                DateOfBirth = volunteer.DateOfBirth,
                FirstName = volunteer.FirstName,
                LastName = volunteer.LastName,
                Description = volunteer.Description
            };
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

        public async Task<bool> SendRequestForJobOffer(Guid volunteerId, Guid jobOfferId)
        {
 
            var requestForJobOffer = new VolunteerJobOffer
            {
                Status = StatusRequest.unapprove,
                JobOfferId = jobOfferId,
                VolunteerId = volunteerId
            };
            await _dbContext.VolunteerJobOffers.AddAsync(requestForJobOffer);

            var isMember = _dbContext.Members
                .Where(t => t.JobOfferId == jobOfferId && t.VolunteerId == volunteerId)
                .FirstOrDefault();//can be .FirstOrDefault(t => t.JobOfferId == jobOfferId && t.VolunteerId == volunteerId) without .Where(...)
            //or you can use IsMember

            if (isMember == null)
            {
                var member = new Member
                {
                    Id = Guid.NewGuid(),
                    VolunteerId = volunteerId,
                    JobOfferId = jobOfferId
                };

                await _dbContext.Members.AddAsync(member);
            }   
           
            return await SaveChangesAsync();
        }

        public async Task<PagedPesponse<List<VolunteerPaginationDTO>>> GetAllVolunteers(PaginationFilter filter, CancellationToken cancellationToken)
        {
            IQueryable<DAL.Models.Volunteer> query = _dbContext.Volunteers;

            if (filter.SearchCriteria != null)
            {
                query = query.Where(s =>
                    s.LastName.Contains(filter.SearchCriteria) ||
                    s.FirstName.Contains(filter.SearchCriteria)
                );
            }

            query = filter.SortColumn switch
            {
                "Id" when filter.SortDirection == "asc" => query
                    .OrderBy(p => p.Id),
                "Id" => query.OrderByDescending(p => p.Id),
                _ => query
            };

            var countRecords = await query
                .CountAsync(cancellationToken);

            query = query
                .Skip(filter.PageNumber * filter.PageSize)
                .Take(filter.PageSize);

            var result = await query
                .Select(p => new VolunteerPaginationDTO
                {
                    Id = p.Id,
                    DateOfBirth = p.DateOfBirth,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Description = p.Description
                })
                .ToListAsync(cancellationToken);

            return new PagedPesponse<List<VolunteerPaginationDTO>>(
                result,
                countRecords,
                filter.PageNumber, filter.PageSize);
        }

        public async Task<bool> IsMember(Guid organizationId, Guid volunteerId)
        {
            return await _dbContext.Members
                .AnyAsync(m => m.VolunteerId == volunteerId && m.JobOffer.OrganizationId == organizationId);

            if (isMember)//funny :)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> AddFeedback(Guid volunteerId, FeedbackDTO feedbackDTO)
        {

            var feedback = new Feedback
            {
                Id = Guid.NewGuid(),
                OrganizationId = feedbackDTO.OrganizationId,
                VolunteerId = volunteerId,
                Description = feedbackDTO.Comment,
                Rating = feedbackDTO.Rating
            };

            await _dbContext.Feedbacks.AddAsync(feedback);
            return await SaveChangesAsync();
        }
    }
}
