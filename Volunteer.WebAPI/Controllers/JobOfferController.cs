using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volunteer.BL.Helper.Exceptions;
using Volunteer.BL.Interfaces;

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
        public async Task<IActionResult> CreateJobOffer([FromBody] JobOfferInfoDTO jobOfferInfoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var organizationId = currentUserService.GetIdFromClaims(HttpContext.User);
            var createJobOffer = await jobOfferService.CreateJobOffer(organizationId, jobOfferInfoDTO);


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

        [Authorize(Roles = "Organization")]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteJobOfferById([FromRoute] Guid id)
        {
            if (await jobOfferService.DeleteJobOfferById(id))
            {
                return Ok("JobOffer is deleted");
            }
            else
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Title = "JobOffer is not deleted",
                    Detail = "Error occured while deleting jobOffer on server"
                };
            }
        }

        [Authorize(Roles = "Volunteer")]
        [HttpGet]
        public async Task<IActionResult> GetAllJobOffers()
        {
            var jobOffers = await jobOfferService.GetAllJobOffers();
            if (jobOffers != null && jobOffers.Count != 0)
            {
                return Ok(jobOffers);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError,

                    "Error retrieving data from the database");
            }
        }  
    }
}
