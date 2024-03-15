using Domain.DTOs;
using Microsoft.EntityFrameworkCore;
using Volunteer.BL.Interfaces;
using Volunteer.DAL.DataAccess;
using Volunteer.DAL.Models;

namespace Volunteer.BL.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly VolunteerDBContext _dbContext;

        public OrganizationService(VolunteerDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<OrganizationInfoDTO> AddOrganization(Guid id,OrganizationInfoDTO organizationInfoDTO)
        {
            var organization = new Organization
            {
                Id = id,
                Name = organizationInfoDTO.Name,
                YearOfFoundation = organizationInfoDTO.YearOfFoundation,
                Description = organizationInfoDTO.Description
            };

            await _dbContext.Organizations.AddAsync(organization);
            await _dbContext.SaveChangesAsync();

            return organizationInfoDTO;
        }

        public async Task<bool> DeleteOrganization(Guid IdOrganization)
        {
            var organization = await GetOrganizationById(IdOrganization);
            if (organization == null)
            {
                return false;
            }

            _dbContext.Organizations.Remove(organization);
            return await SaveChangesAsync();
        }

        public async Task<Organization> GetOrganizationById(Guid id)
        {
            return await _dbContext.Organizations.FirstOrDefaultAsync(i => i.Id == id);
        }

        //public async Task<bool> UpdateOrganization(OrganizationInfoDTO organizationInfoDTO)
        //{
        //    var organization = await _dbContext.Organizations.FindAsync(organizationInfoDTO.Id);

        //    if (organization == null)
        //    {
        //        return false;
        //    }

            //organization.Name = organizationInfoDTO.Name;
            //organization.YearOfFoundation = organizationInfoDTO.YearOfFoundation;
            //organization.Description = organizationInfoDTO.Description;

        //    _dbContext.Organizations.Update(organization);
        //    return await SaveChangesAsync();
        //}

        public async Task<bool> SaveChangesAsync()
            => await _dbContext.SaveChangesAsync() > 0;
    }
}
