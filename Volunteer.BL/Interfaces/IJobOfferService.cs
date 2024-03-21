using Domain.DTOs;
using Volunteer.DAL.Models;

namespace Volunteer.BL.Interfaces
{
    public interface IJobOfferService
    {
        Task<JobOfferInfoDTO> CreateJobOffer(Guid id,JobOfferInfoDTO jobOfferInfoDTO);
        Task<List<JobOffer>> GetAllJobOffers();
        Task<JobOfferInfoDTO> GetGobOfferInfo(Guid id);
        Task<JobOffer> GetGobOfferById(Guid id);
        Task <JobOfferInfoDTO> UpdateJobOffer(JobOfferInfoDTO jobOfferInfoDTO);
        Task <bool> DeleteJobOfferById(Guid id);
        Task<List<AllRequestFromVolunteerDTO>> GetAllRequestFromVolunteer(Guid organizationId, Guid jobOfferId);
    }
}
