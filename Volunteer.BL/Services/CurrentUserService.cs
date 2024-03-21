using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Volunteer.BL.Helper.Exceptions;
using Volunteer.BL.Interfaces;

namespace Volunteer.BL.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public Guid GetIdFromClaims(ClaimsPrincipal user)
        {
            var id = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(id))
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status422UnprocessableEntity,
                    Title = "User ID not found",
                    Detail = "User ID claim not found in claims"
                };
            }

            if (!Guid.TryParse(id, out Guid userId))
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status422UnprocessableEntity,
                    Title = "Error parsing guid",
                    Detail = "Error occurred while parsing guid from user claims"
                };
            }

            return userId;
        }
    }
}
