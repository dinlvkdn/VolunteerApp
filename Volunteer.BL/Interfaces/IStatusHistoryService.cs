using Domain.DTOs;
using Domain.Pagination;

namespace Volunteer.BL.Interfaces
{
    public interface IStatusHistoryService
    {
        Task<PagedPesponse<List<OrganizationDTO>>> GetAllOrganizations(
            PaginationFilter filter,
            CancellationToken cancellationToken);
    }
}
