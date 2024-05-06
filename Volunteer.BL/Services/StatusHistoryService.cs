using Domain;
using Domain.DTOs;
using Domain.Pagination;
using Microsoft.EntityFrameworkCore;
using Volunteer.BL.Interfaces;
using Volunteer.DAL;
using Volunteer.DAL.DataAccess;
using Volunteer.DAL.Models;

namespace Volunteer.BL.Services
{
    public class StatusHistoryService : IStatusHistoryService
    {
        private readonly VolunteerDBContext _dbContext;
        private readonly PagingService _pagingService;

        public StatusHistoryService(VolunteerDBContext dbContext, PagingService pagingService)
        {
            _dbContext = dbContext;
            _pagingService = pagingService;
        }

        public async Task<PagedPesponse<List<OrganizationDTO>>> GetAllOrganizations(PaginationFilter filter, CancellationToken cancellationToken)
        {
            var result = await _pagingService.ApplyPagination<Organization>(filter, cancellationToken);//i've seen the paging logic duplication a lot of times.
                                                                                                       //added raw version of how it can be extracted. this is just for example, can be way better than I did

            return new PagedPesponse<List<OrganizationDTO>>(
                result.Data.Select(ToDto).ToList(),
                result.TotalRecords,
                result.PageNumber, result.PageSize);
        }

        private OrganizationDTO ToDto(Organization organization)
        {
            return new OrganizationDTO
            {
                Id = organization.Id,
                NameOrganization = organization.Name,
                YearOfFoundation = organization.YearOfFoundation
            };
        }
    }
}
