using Domain.DTOs;

namespace Volunteer.BL.Interfaces
{
    public interface IVolunteerService
    {
        public Task<VolunteerInfoDTO> AddVolunteer(Guid id,VolunteerInfoDTO volunteerDTO);
        Task<VolunteerShortInfoDTO> UpdateVolunteer(Guid id, VolunteerShortInfoDTO volunteerDTO);
        public Task<bool> DeleteVolunteer(Guid volunteerId);
        public Task<DAL.Models.Volunteer> GetVolunteerById(Guid id);

    }
}
