using Domain.DTOs;
using Volunteer.DAL.Models;

namespace Volunteer.BL.Interfaces
{
    public  interface IOrganizationService
    {
        Task<bool> AddOrganization(OrganizationInfoDTO organizationInfoDTO);
        Task<bool> UpdateOrganization(OrganizationInfoDTO organizationInfoDTO);
        Task<bool> DeleteOrganization(Guid IdOrganization);
        Task<Organization> GetOrganizationById(Guid id);
    }
}
