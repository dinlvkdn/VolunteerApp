using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Volunteer.BL.Helper.Exceptions;
using Volunteer.DAL.DataAccess;

namespace Volunteer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly VolunteerDBContext _dbContext;

        public UserController(VolunteerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetById()
        {
            Guid Id;
            string roleName;
            try
            {
                Id = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                roleName = HttpContext.User.FindFirstValue(ClaimTypes.Role);
            }
            catch (Exception)
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status422UnprocessableEntity,
                    Title = "Error parsing guid",
                    Detail = "Error occured while parsing guid from user claims"
                };
            }

            if (roleName == "Volunteer")
            {
                return Redirect($"/api/Volunteer/GetVolunteer/{Id}");
            }
            else if (roleName == "Organization")
            {
                return Redirect($"/api/Organization/GetOrganization/{Id}");
            }

            return BadRequest();
        }
    }
}
