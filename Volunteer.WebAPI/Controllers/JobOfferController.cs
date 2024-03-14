using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Volunteer.BL.Interfaces;

namespace Volunteer.WebAPI.Controllers
{
    public class JobOfferController : ControllerBase
    {
        private readonly IJobOfferService jobOfferService;

        public JobOfferController(IJobOfferService jobOfferService)
        {
            this.jobOfferService = jobOfferService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateJobOffer([FromBody] JobOfferInfoDTO jobOfferInfoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            if (jobOfferInfoDTO == null)
            {
                return BadRequest("Object is invalid or empty");
            }
            if (await jobOfferService.CreateJobOffer(jobOfferInfoDTO))
            {
                return Ok();    
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Job offer not added");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateJobOffer([FromBody] JobOfferInfoDTO jobOfferInfoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (jobOfferInfoDTO == null)
            {
                return BadRequest("Object is invalid or empty");
            }
            if( await jobOfferService.UpdateJobOffer(jobOfferInfoDTO))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update job offer");
            }
        }

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

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetGobOfferById([FromRoute]Guid id)
        {
            if (id == Guid.Empty) return BadRequest("Id does not exist");

            var jobOffer = await jobOfferService.GetGobOfferById(id);
            if(jobOffer != null)
            {
                return Ok(id);
            }
            else
            {
                return NotFound("No job offer found");
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteJobOfferById([FromRoute] Guid id)
        {
            if (id == Guid.Empty) return BadRequest("Id does not exist");

            var jobOffer = await jobOfferService.GetGobOfferById(id);
            if (jobOffer == null)
            {
                return NotFound("No job offer found");
            }
            if (await jobOfferService.DeleteJobOfferById(id))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Job offer is not deleted");
            }
        }
    }
}
