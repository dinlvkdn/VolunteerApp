using System.Security.Claims;

namespace Volunteer.BL.Interfaces
{
    public interface ICurrentUserService
    {
        public Guid GetIdFromClaims(ClaimsPrincipal user);
    }
}
