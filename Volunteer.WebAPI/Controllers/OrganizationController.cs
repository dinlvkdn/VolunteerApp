using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volunteer.BL.Helper.Exceptions;
using Volunteer.BL.Interfaces;
using Volunteer.DAL.Models;

namespace Volunteer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Organization")]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService organizationService;
        private readonly ICurrentUserService currentUserService;
        private readonly IResumeService resumeService;
        private readonly IJobOfferService jobOfferService;


        public OrganizationController(IOrganizationService organizationService, ICurrentUserService currentUserService, IResumeService resumeService, IJobOfferService jobOfferService)
        {
            this.organizationService = organizationService;
            this.currentUserService = currentUserService;
            this.resumeService = resumeService;
            this.jobOfferService = jobOfferService;
        }


        [HttpPost]
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

        [HttpDelete("delete")]
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
                    return Ok("Organization is deleted");
                }
            }

            throw new ApiException()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Title = "Organization is not deleted",
                Detail = "Error occured while deleting orhanization on server"
            };

        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrganization([FromBody] OrganizationInfoDTO organizationInfoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var id = currentUserService.GetIdFromClaims(HttpContext.User);

            var organizationToUpdate = await organizationService.GetOrganizationInfo(id);

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

        [HttpGet("getOrganization")]
        public async Task<IActionResult> GetOrganizationById()
        {
            var id = currentUserService.GetIdFromClaims(HttpContext.User);

            var organization = await organizationService.GetOrganizationInfo(id);

            if (organization == null)
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Title = "Organization doesn't exist",
                    Detail = "No organization on database"
                };
            }

            return Ok(organization);
        }

        [HttpPut("confirmVolunteerOnJobOffer")]
        public async Task<IActionResult> ConfirmVolunteerOnJobOffer([FromBody] RequestForJobOfferDTO requestForJobOfferDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var confirmVolunteerOnJobOffer = await organizationService.ConfirmVolunteerOnJobOffer(requestForJobOfferDTO.JobOfferId, requestForJobOfferDTO.VolunteerId);

            if (confirmVolunteerOnJobOffer)
            {
                return Ok("Volunteer confirmed");
            }

            throw new ApiException()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Title = "Failed to confirm volunteer on job offer",
                Detail = "Failed to confirm volunteer on job offer"
            };
        }

        [HttpPut("cancelVolunteerJobOfferRequest")]
        public async Task<IActionResult> CancelVolunteerJobOfferRequest([FromBody] RequestForJobOfferDTO requestForJobOfferDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var cancelVolunteerJobOfferRequest = await organizationService.CancelVolunteerJobOfferRequest(requestForJobOfferDTO.JobOfferId, requestForJobOfferDTO.VolunteerId );

            if (cancelVolunteerJobOfferRequest)
            {
                return Ok("Volunteer job offer request cancelled");
            }

            throw new ApiException()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Title = "Failed to cancel volunteer job offer request",
                Detail = "Failed to cancel volunteer job offer request"
            };
        }

        [HttpGet("downloadResume")]
        public async Task<IActionResult> DownloadResume([FromQuery] string fileName)
        {
            var result = await resumeService.DownloadResume(fileName);
            return File(result.Item1, result.Item2, result.Item3);
        }

        [HttpGet("{jobOfferId:Guid}")]
        public async Task<IActionResult> GetAllRequestFromVolunteer([FromRoute] Guid jobOfferId)
        {
            var organizationId = currentUserService.GetIdFromClaims(HttpContext.User);

            var volunteer = await jobOfferService.GetAllRequestFromVolunteer(organizationId, jobOfferId);
            if (volunteer != null)
            {
                return Ok(volunteer);
            }

            throw new ApiException()
            {
                StatusCode = StatusCodes.Status404NotFound,
                Title = "Not found",
                Detail = "Error occured while getting volunteers."
            };
        }
    }
}
