using Domain.DTOs;
using Domain.Pagination;

namespace Volunteer.BL.Interfaces
{
    public interface IVolunteerService
    {
        public Task<VolunteerInfoDTO> AddVolunteer(Guid id,VolunteerInfoDTO volunteerDTO);
        Task<VolunteerShortInfoDTO> UpdateVolunteer(Guid id, VolunteerShortInfoDTO volunteerDTO);
        public Task<bool> DeleteVolunteer(Guid volunteerId);
        public Task<VolunteerInfoDTO> GetVolunteerInfo(Guid id);
        public Task<bool> SendRequestForJobOffer(Guid volunteerId, Guid jobOfferId);
        public Task<PagedPesponse<List<VolunteerPaginationDTO>>> GetAllVolunteers(PaginationFilter filter, CancellationToken cancellationToken);
        public Task<bool> IsMember(Guid organizationId, Guid volunteerId);
        public Task<bool> AddFeedback(Guid volunteerId, FeedbackDTO feedbackDTO);
    }
}
