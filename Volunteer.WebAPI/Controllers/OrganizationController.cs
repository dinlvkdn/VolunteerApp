using Domain.DTOs;
using Domain.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volunteer.BL.Helper.Exceptions;
using Volunteer.BL.Interfaces;

namespace Volunteer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService organizationService;
        private readonly ICurrentUserService currentUserService;
        private readonly IResumeService resumeService;
        private readonly IJobOfferService jobOfferService;
        private readonly IVolunteerService volunteerService;

        public OrganizationController(
            IOrganizationService organizationService,
            ICurrentUserService currentUserService,
            IResumeService resumeService,
            IJobOfferService jobOfferService,
            IVolunteerService volunteerService)
        {
            this.organizationService = organizationService;
            this.currentUserService = currentUserService;
            this.resumeService = resumeService;
            this.jobOfferService = jobOfferService;
            this.volunteerService = volunteerService;
        }

        [HttpPost]
        [Authorize(Roles = "Organization")]
        public async Task<IActionResult> AddOrganization(OrganizationInfoDTO organizationInfoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var id = currentUserService.GetIdFromClaims(HttpContext.User);

            var existingOrganization = await organizationService.AddOrganization(id, organizationInfoDTO);

            if (existingOrganization != null)
            {
                return Ok(existingOrganization);
            }
            else
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Title = "Could not add organization",
                    Detail = "Could not add organization"
                };
            }
        }

        [Authorize(Roles = "Organization")]
        [HttpDelete]
        public async Task<IActionResult> DeleteOrganization()
        {
            var id = currentUserService.GetIdFromClaims(HttpContext.User);
            var auth = Request.Headers.Authorization;
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", auth.ToString());

            var response = await client.DeleteAsync($"{Constants.ngrok}/api/User?userId={id}");
            if (response.IsSuccessStatusCode)
            {
                if (await organizationService.DeleteOrganization(id))
                {
                    return NoContent();
                }
            }

            throw new ApiException()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Title = "Organization is not deleted",
                Detail = "Error occured while deleting orhanization on server"
            };

        }

        [Authorize(Roles = "Organization")]
        [HttpPut]
        public async Task<IActionResult> UpdateOrganization([FromBody] OrganizationInfoDTO organizationInfoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var id = currentUserService.GetIdFromClaims(HttpContext.User);

            var organizationToUpdate = await organizationService.GetOrganizationById(id);

            if (organizationToUpdate == null)
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Title = "Organization doesn't exist",
                    Detail = "No organization on database"
                };
            }
            var organization = await organizationService.UpdateOrganization(id, organizationInfoDTO);

            if (organization == null)
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Title = "Organization doesn't update",
                    Detail = "Failed to update organization"
                };
            }
            else
            {
                return Ok(organization);
            }
        }

        [Authorize(Roles = "Organization, Volunteer, Admin")]
        [HttpGet("GetOrganization/{organizationId:Guid}")]
        public async Task<IActionResult> GetOrganizationById([FromRoute] Guid organizationId)
        {
            var organization = await organizationService.GetOrganizationInfo(organizationId);

            if (organization == null)
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Title = "An error occurred",
                    Detail = "An error occurred"
                };
            }
            return Ok(organization);
        }

        [Authorize(Roles = "Organization")]
        [HttpPut("confirmVolunteerOnJobOffer")]
        public async Task<IActionResult> ConfirmVolunteerOnJobOffer(RequestForJobOfferDTO requestForJobOfferDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var confirmVolunteerOnJobOffer = await organizationService.ConfirmVolunteerOnJobOffer(requestForJobOfferDTO.JobOfferId, requestForJobOfferDTO.VolunteerId);

            if (confirmVolunteerOnJobOffer)
            {
                return NoContent();
            }

            throw new ApiException()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Title = "Failed to confirm volunteer on job offer",
                Detail = "Failed to confirm volunteer on job offer"
            };
        }

        [Authorize(Roles = "Organization")]
        [HttpPut("cancelVolunteerJobOfferRequest")]
        public async Task<IActionResult> CancelVolunteerJobOfferRequest(RequestForJobOfferDTO requestForJobOfferDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var cancelVolunteerJobOfferRequest = await organizationService.CancelVolunteerJobOfferRequest(requestForJobOfferDTO.JobOfferId, requestForJobOfferDTO.VolunteerId);

            if (cancelVolunteerJobOfferRequest)
            {
                return NoContent();
            }

            throw new ApiException()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Title = "Failed to cancel volunteer job offer request",
                Detail = "Failed to cancel volunteer job offer request"
            };
        }


        [Authorize(Roles = "Organization")]
        [HttpGet("downloadResume/{volunteerId:Guid}")]
        public async Task<IActionResult> DownloadResume([FromRoute] Guid volunteerId)
        {
            var result = await resumeService.DownloadResume(volunteerId);
            if (result.Item1 == null || result.Item2 == null || result.Item3 == null)
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Title = "Can't download resume",
                    Detail = "Error occured while downloading resume from server"
                };
            }
            return File(result.Item1, result.Item2, result.Item3);
        }

        [Authorize(Roles = "Organization")]
        [HttpGet("getVolunteers")]
        public async Task<IActionResult> GetAllVolunteers([FromQuery] PaginationFilter filter, CancellationToken cancellationToken)
        {
            var volunteers = await volunteerService.GetAllVolunteers(filter, cancellationToken);
            if (volunteers != null)
            {
                return Ok(volunteers);
            }

            throw new ApiException()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Title = "Error retrieving data from the database",
                Detail = "Error retrieving data from the database"
            };
        }

        [Authorize(Roles = "Organization, Volunteer, Admin")]
        [HttpGet("GetListFeedbacks")]
        public async Task<IActionResult> GetListFeedbacks([FromQuery] PaginationFilter filter, CancellationToken cancellationToken)
        {
            var feedbacks = await organizationService.GetListFeedbacks(filter, cancellationToken);

            if (feedbacks != null)
            {
                return Ok(feedbacks);
            }

            throw new ApiException()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Title = "Error retrieving data from the database",
                Detail = "Error retrieving data from the database"
            };
        }
    }
}
