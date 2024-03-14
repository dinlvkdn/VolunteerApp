using Domain.DTOs;

namespace Volunteer.BL.Interfaces
{
    public interface IVolunteerService
    {
        public Task<VolunteerInfoDTO> AddVolunteer(Guid id,VolunteerInfoDTO volunteerInfoDTO);
        Task<bool> UpdateVolunteer(VolunteerInfoDTO volunteerInfoDTO);
        public Task<bool> DeleteVolunteer(Guid volunteerId);
        public Task<DAL.Models.Volunteer> GetVolunteerById(Guid id);

    }
}
