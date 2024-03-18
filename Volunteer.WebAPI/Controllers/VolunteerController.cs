using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Volunteer.BL.Helper.Exceptions;
using Volunteer.BL.Interfaces;

namespace Volunteer.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles ="Volunteer")]
    public class VolunteerController : ControllerBase
    {
        private readonly IVolunteerService volunteerService;
        private readonly IResumeService resumeService;
        private readonly IGuidValidationService guidValidationService;


        public VolunteerController(IVolunteerService volunteerService, IResumeService resumeService, IGuidValidationService guidValidationService)
        {
            this.volunteerService = volunteerService;
            this.resumeService = resumeService;
            this.guidValidationService = guidValidationService;
        }

        [HttpPost]
        
        public async Task<IActionResult> AdditionVolunteer(VolunteerInfoDTO volunteerInfoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var id = await guidValidationService.GetIdFromClaims(HttpContext.User);

            var createVolunteer = await volunteerService.AddVolunteer(id, volunteerInfoDTO);

            if (createVolunteer != null)
            {
                return Ok(createVolunteer);
            }
            else
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Title = "Volunteer doesn't exist",
                    Detail = "Volunteer doesn't exist while creating volunteer"
                };
            }
        }


        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteVolunteer([FromRoute] Guid id)
        {
            await guidValidationService.CheckForEmptyGuid(id);

            var volunteer = await volunteerService.GetVolunteerById(id);

            if (volunteer == null)
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Title = "No volunteer exists",
                    Detail = "Volunteer does`n exist in the database"
                };
            }

            if (await volunteerService.DeleteVolunteer(id))
            {
                return Ok("Volunteer is deleted");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Volunteer is not deleted");
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Title = "Volunteer is not deleted",
                    Detail = "Error occured while deleting volunteer on server"
                };
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVolunteer([FromBody] VolunteerShortInfoDTO volunteerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var id = await guidValidationService.GetIdFromClaims(HttpContext.User);

            var volunteerToUpdate = await volunteerService.GetVolunteerById(id);

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

        [HttpGet]
        public async Task<IActionResult> GetVolunteerById()
        {
            var id = await guidValidationService.GetIdFromClaims(HttpContext.User);

            var volunteer = await volunteerService.GetVolunteerById(id);

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
  
        [HttpPost("uploadResume")]
        public async Task<IActionResult> UploadResume(IFormFile file)
        {
            var id = await guidValidationService.GetIdFromClaims(HttpContext.User);

            var result = await resumeService.UploadResume(file, id);
            return Ok(result);
        }


        [HttpGet("downloadResume")]
        public async Task<IActionResult> DownloadResume([FromQuery] string fileName)
        {
            var result = await resumeService.DownloadResume(fileName);
            return File(result.Item1, result.Item2, result.Item3);
        }
    }
}
