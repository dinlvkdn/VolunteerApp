using Domain.DTOs;
using Domain.Pagination;
using Volunteer.DAL.Models;

namespace Volunteer.BL.Interfaces
{
    public interface IOrganizationService
    {
        Task<OrganizationInfoDTO> AddOrganization(Guid id,OrganizationInfoDTO organizationInfoDTO);
        Task<OrganizationInfoDTO> UpdateOrganization(Guid id, OrganizationInfoDTO organizationInfoDTO);
        Task<bool> DeleteOrganization(Guid IdOrganization);
        Task<OrganizationInfoDTO> GetOrganizationInfo(Guid id);
        Task<Organization> GetOrganizationById(Guid id);
        Task<bool> ConfirmVolunteerOnJobOffer(Guid jobOfferIdб, Guid volunteerId);
        Task<bool> CancelVolunteerJobOfferRequest(Guid jobOfferId, Guid volunteerId);
        Task<PagedPesponse<List<FeedbackPaginationDTO>>> GetListFeedbacks(
            PaginationFilter filter,
            CancellationToken cancellationToken);
    }
}
