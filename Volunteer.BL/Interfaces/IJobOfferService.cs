using Domain.DTOs;
using Volunteer.DAL.Models;

namespace Volunteer.BL.Interfaces
{
    public interface IJobOfferService
    {
        Task<bool> CreateJobOffer(JobOfferInfoDTO jobOfferInfoDTO);
        Task<List<JobOffer>> GetAllJobOffers();
        Task<JobOffer> GetGobOfferById(Guid id);
        Task <bool> UpdateJobOffer(JobOfferInfoDTO jobOfferInfoDTO);
        Task <bool> DeleteJobOfferById(Guid id);
        
    }
}
