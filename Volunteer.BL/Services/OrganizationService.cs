using Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using Volunteer.BL.Interfaces;
using Volunteer.DAL.DataAccess;
using Volunteer.DAL.Models;

namespace Volunteer.BL.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly VolunteerDBContext dbContext;

        public OrganizationService(VolunteerDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<bool> AddOrganization(OrganizationInfoDTO organizationInfoDTO)
        {
            var existingOrganization = await dbContext.Organizations.FindAsync(organizationInfoDTO.IdOrganization);
            
            if (existingOrganization != null)
            {
                return false; 
            }

            var organization = new Organization
            {
                Id = organizationInfoDTO.IdOrganization,
                Name = organizationInfoDTO.Name,
                YearOfFoundation = organizationInfoDTO.YearOfFoundation,
                Description = organizationInfoDTO.Description
            };

            await dbContext.Organizations.AddAsync(organization);
            return await SaveChangesAsync();
        }

        public async Task<bool> DeleteOrganization(Guid IdOrganization)
        {
            var organization = await GetOrganizationById(IdOrganization);
            if (organization == null)
            {
                return false;
            }
            
            dbContext.Organizations.Remove(organization);
            return await SaveChangesAsync();
        }

        public async Task<Organization> GetOrganizationById(Guid id)
        {
            return await dbContext.Organizations.FindAsync(id);
        }

        public async Task<bool> UpdateOrganization(OrganizationInfoDTO organizationInfoDTO)
        {
            var organization = await dbContext.Organizations.FindAsync(organizationInfoDTO.IdOrganization);

            if (organization == null)
            {
                return false;
            }

            organization.Name = organizationInfoDTO.Name;
            organization.YearOfFoundation = organizationInfoDTO.YearOfFoundation;
            organization.Description = organizationInfoDTO.Description;

            dbContext.Organizations.Update(organization);
            return await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            var save = await dbContext.SaveChangesAsync();
            return save > 0 ? true : false;
        }
    }
}
