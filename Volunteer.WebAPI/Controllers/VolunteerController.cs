using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volunteer.BL.Helper.Exceptions;
using Volunteer.BL.Interfaces;

namespace Volunteer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Volunteer")]
    public class VolunteerController : ControllerBase
    {
        private readonly IVolunteerService volunteerService;
        private readonly IResumeService resumeService;
        private readonly ICurrentUserService currentUserService;


        public VolunteerController(IVolunteerService volunteerService, IResumeService resumeService, ICurrentUserService currentUserService)
        {
            this.volunteerService = volunteerService;
            this.resumeService = resumeService;
            this.currentUserService = currentUserService;
        }

        [HttpPost("addVolunteer")]

        public async Task<IActionResult> AdditionVolunteer(VolunteerInfoDTO volunteerInfoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var id = currentUserService.GetIdFromClaims(HttpContext.User);

            var createVolunteer = await volunteerService.AddVolunteer(id, volunteerInfoDTO);

            if (createVolunteer != null)
            {
                return Ok(createVolunteer);
            }
            else
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Title = "Could not add volunteer",
                    Detail = "Could not add volunteer"
                };
            }
        }
            
        [HttpPost("uploadResume")]
        public async Task<IActionResult> UploadResume([FromForm]IFormFile file)
        {
            var id = currentUserService.GetIdFromClaims(HttpContext.User);
            var result = await resumeService.UploadResume(file, id);
            return Ok(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteVolunteer()
        {
            var id = currentUserService.GetIdFromClaims(HttpContext.User);
            var auth = Request.Headers.Authorization;
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", auth.ToString());
            var response = await client.DeleteAsync($"{Constants.ngrok}/api/User?userId={id}");
            if (response.IsSuccessStatusCode)
            {
                if (await volunteerService.DeleteVolunteer(id))
                {
                    return Ok("Volunteer is deleted");
                }
            }

            throw new ApiException()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Title = "Volunteer is not deleted",
                Detail = "Error occured while deleting volunteer on server"
            };
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateVolunteer([FromBody] VolunteerShortInfoDTO volunteerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var id = currentUserService.GetIdFromClaims(HttpContext.User);

            var volunteerToUpdate = await volunteerService.GetVolunteerInfo(id);

            if (volunteerToUpdate == null)
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Title = "Volunteer doesn't exist",
                    Detail = "No volunteer on database"
                };
            }

            var volunteer = await volunteerService.UpdateVolunteer(id, volunteerDTO);

            if (volunteer == null)
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Title = "Volunteer doesn't update",
                    Detail = "Failed to update volunteer"
                };
            }
            else
            {
                return Ok(volunteer);
            }
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetVolunteerById()
        {
            var id = currentUserService.GetIdFromClaims(HttpContext.User);

            var volunteer = await volunteerService.GetVolunteerInfo(id);

            if (volunteer == null)
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Title = "Volunteer doesn't exist",
                    Detail = "No volunteer on database"
                };
            }

            return Ok(volunteer);
        }


        [HttpPost("sendRequestForJobOffer")]
        public async Task<IActionResult> SendRequestForJobOffer([FromBody] RequestForJobOfferDTO requestForJobOfferDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var volunteerId = currentUserService.GetIdFromClaims(HttpContext.User);

            var sendRequestForJobOffer = await volunteerService.SendRequestForJobOffer(volunteerId, requestForJobOfferDTO);

            if (sendRequestForJobOffer)
            {
                return Ok("Successfully sent a request to job offer");
            }
            
            throw new ApiException()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Title = "Failed to send request to job offer",
                Detail = "Failed to send request to job offer"
            };
        }
    }
}
