using System.Security.Claims;

namespace Volunteer.BL.Interfaces
{
    public interface IGuidValidationService
    {
        public Task<Guid> GetIdFromClaims(ClaimsPrincipal user);
        public Task CheckForEmptyGuid(Guid id);

    }
}
