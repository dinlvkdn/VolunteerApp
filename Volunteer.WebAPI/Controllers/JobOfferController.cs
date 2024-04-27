using Domain.DTOs;
using Domain.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volunteer.BL.Helper.Exceptions;
using Volunteer.BL.Interfaces;
using Volunteer.BL.Services;

namespace Volunteer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobOfferController : ControllerBase
    {
        private readonly IJobOfferService jobOfferService;
        private readonly ICurrentUserService currentUserService;

        public JobOfferController(IJobOfferService jobOfferService, ICurrentUserService currentUserService)
        {
            this.jobOfferService = jobOfferService;
            this.currentUserService = currentUserService;
        }

        [Authorize(Roles = "Organization")]
        [HttpPost("createJobOffer")]
        public async Task<IActionResult> CreateJobOffer([FromBody] CreateJobOfferDTO jobOfferDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var organizationId = currentUserService.GetIdFromClaims(HttpContext.User);
            var createJobOffer = await jobOfferService.CreateJobOffer(organizationId, jobOfferDTO);


            if (createJobOffer != null)
            {
                return Ok(createJobOffer);
            }
            else
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Title = "Job offer not added",
                    Detail = "Job offer not added"
                };
            }
        }

        [Authorize(Roles = "Organization")]
        [HttpPut("updateJobOffer")]
        public async Task<IActionResult> UpdateJobOffer([FromBody] JobOfferInfoDTO jobOfferInfoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updateJobOffer = await jobOfferService.UpdateJobOffer(jobOfferInfoDTO);

            if (updateJobOffer != null)
            {
                return Ok(updateJobOffer);
            }
            else
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Title = "Failed to change jobOffer",
                    Detail = "Failed to change jobOffer"
                };
            }
        }

        [Authorize(Roles = "Organization, Volunteer")]
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetJobOfferInfo([FromRoute] Guid id)
        {
            var jobOffer = await jobOfferService.GetGobOfferInfo(id);

            return Ok(jobOffer);
        }

        [Authorize(Roles = "Volunteer, Organization, Admin")]
        [HttpGet("getAllJobOffers")]
        public async Task<IActionResult> GetAllJobOffers([FromQuery] PaginationFilter filter, CancellationToken cancellationToken)
        {
           var jobOffers = await jobOfferService.GetAllJobOffers(filter, cancellationToken);

            if (jobOffers != null)
            {
                return Ok(jobOffers);
            }

            throw new ApiException()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Title = "Error retrieving data from the database",
                Detail = "Error retrieving data from the database"
            };
        }

        [Authorize(Roles = "Organization, Volunteer")]
        [HttpGet("getJobOfferRequests")]
        public async Task<IActionResult> GetJobOfferRequests([FromQuery] PaginationFilter filter, CancellationToken cancellationToken)
        {
            var volunteerId = currentUserService.GetIdFromClaims(HttpContext.User);
            var requests = await jobOfferService.GetJobOfferRequests(volunteerId, filter, cancellationToken);
            if (requests != null)
            {
                return Ok(requests);
            }

            throw new ApiException()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Title = "Error retrieving data from the database",
                Detail = "Error retrieving data from the database"
            };
        }

        [Authorize(Roles = "Organization")]
        [HttpGet("getRequestsFromVolunteers")]
        public async Task<IActionResult> GetRequestsFromVolunteers([FromQuery] PaginationFilter filter, CancellationToken cancellationToken)
        {
            var organizationId = currentUserService.GetIdFromClaims(HttpContext.User);

            var requests = await jobOfferService.GetRequestsFromVolunteers(organizationId, filter, cancellationToken);
            if (requests != null)
            {
                return Ok(requests);
            }

            throw new ApiException()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Title = "Error retrieving data from the database",
                Detail = "Error retrieving data from the database"
            };
        }

        [Authorize(Roles = "Organization, Volunteer")]
        [HttpGet("getOfferStatus/{offerId:Guid}")]
        public async Task<IActionResult> GetOfferStatus([FromRoute] Guid offerId)
        {
            var volunteerId = currentUserService.GetIdFromClaims(HttpContext.User);

            var status = await jobOfferService.GetOfferStatus(volunteerId, offerId);

            return status != null
                ? (IActionResult)Ok(status)
                : throw new ApiException()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Title = "Error retrieving data from the database",
                    Detail = "Error retrieving data from the database"
                };
        }

        [Authorize(Roles = "Organization")]
        [HttpDelete("delete/{offerId:Guid}")]
        public async Task<IActionResult> DeleteJobOffer([FromRoute] Guid offerId)
        {
            var organizationId = currentUserService.GetIdFromClaims(HttpContext.User);
            if (await jobOfferService.DeleteJobOffer(organizationId, offerId))
            {
                return NoContent();
            }
            throw new ApiException()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Title = "Offer is not deleted",
                Detail = "Error occured while deleting offer on server"
            };
        }
    }
}
