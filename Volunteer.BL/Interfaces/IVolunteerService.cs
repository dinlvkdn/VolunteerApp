using Domain.DTOs;

namespace Volunteer.BL.Interfaces
{
    public interface IVolunteerService
    {
        public Task<VolunteerDTO> AddVolunteer(VolunteerDTO volunteerDTO);
        Task<bool> UpdateVolunteer(VolunteerDTO volunteerDTO);
        public Task<bool> DeleteVolunteer(Guid volunteerId);
        public Task<DAL.Models.Volunteer> GetVolunteerById(Guid id);

    }
}
