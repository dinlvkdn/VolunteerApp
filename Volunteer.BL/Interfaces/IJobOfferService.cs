﻿using Domain;
using Domain.DTOs;
using Domain.Pagination;
using Volunteer.DAL.Models;

namespace Volunteer.BL.Interfaces
{
    public interface IJobOfferService
    {
        Task<CreateJobOfferDTO> CreateJobOffer(Guid id, CreateJobOfferDTO jobOfferDTO);
        Task<PagedPesponse<List<JobOfferPaginationDTO>>> GetAllJobOffers(PaginationFilter paginationFilter, CancellationToken cancellationToken);
        Task<ShowJobOfferDTO> GetGobOfferInfo(Guid id);
        Task<JobOffer> GetGobOfferById(Guid id);
        Task<JobOfferInfoDTO> UpdateJobOffer(JobOfferInfoDTO jobOfferInfoDTO);
        Task<bool> DeleteJobOfferById(Guid id);
        Task<StatusRequest> GetOfferStatus(Guid volunteerId, Guid offerId);
        //Task<List<AllRequestFromVolunteerDTO>> GetAllRequestFromVolunteer(Guid organizationId, Guid jobOfferId);
        Task<PagedPesponse<List<JobOfferRequestDTO>>> GetJobOfferRequests(
            Guid volunteerId,
            PaginationFilter filter,
            CancellationToken cancellationToken);
        Task<PagedPesponse<List<VolunteerRequestsDTO>>> GetRequestsFromVolunteers(
            Guid organizationId,
            PaginationFilter filter,
            CancellationToken cancellationToken);
    }
}
