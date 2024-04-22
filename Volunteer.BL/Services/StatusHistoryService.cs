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

        public StatusHistoryService(VolunteerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedPesponse<List<OrganizationDTO>>> GetAllOrganizations(PaginationFilter filter, CancellationToken cancellationToken)
        {
            IQueryable<Organization> query = _dbContext.Organizations;

            query = filter.SortColumn switch
            {
                "Id" when filter.SortDirection == "asc" => query
                    .OrderBy(p => p.Id),
                "Id" => query.OrderByDescending(p => p.Id),
                _ => query
            };

            var countRecords = await query
              .CountAsync(cancellationToken);

            query = query
                .Skip(filter.PageNumber * filter.PageSize)
                .Take(filter.PageSize);

            var result = await query
                .Select(p => new OrganizationDTO
                {
                    Id = p.Id,
                    NameOrganization = p.Name,
                    YearOfFoundation = p.YearOfFoundation
                })
                .ToListAsync(cancellationToken);

            return new PagedPesponse<List<OrganizationDTO>>(
                result,
                countRecords,
                filter.PageNumber, filter.PageSize);
        }
    }
}
