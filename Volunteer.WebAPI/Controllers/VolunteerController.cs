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

        public VolunteerController(IVolunteerService volunteerService, IResumeService resumeService)
        {
            this.volunteerService = volunteerService;
            this.resumeService = resumeService;
        }

        [HttpPost]
        
        public async Task<IActionResult> AdditionVolunteer(VolunteerInfoDTO volunteerInfoDTO)
        {
            Guid id;
            try
            {
                 id = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            }
            catch (Exception e)
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status422UnprocessableEntity,
                    Title = "Error parsing guid",
                    Detail = "Error occured while parsing guid from volunteer claims"
                };
            }


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


        [HttpDelete("{volunteerId:Guid}")]
        public async Task<IActionResult> DeleteVolunteer([FromRoute] Guid volunteerId)
        {
            if (volunteerId == Guid.Empty)
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Title = "Invalid guid",
                    Detail = "Guid does not exist"
                };
            }
            var volunteer = await volunteerService.GetVolunteerById(volunteerId);
            if (volunteer == null)
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Title = "No volunteer exists",
                    Detail = "Volunteer does`n exist in the database"
                };
            }
            if (await volunteerService.DeleteVolunteer(volunteerId))
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

        //[HttpPut]
        //public async Task<IActionResult> UpdateVolunteer([FromBody] VolunteerInfoDTO volunteerInfoDTO)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);

        //    }
        //    var volunteerToUpdate = await volunteerService.GetVolunteerById(volunteerInfoDTO.IdVolunteer);
        //    if (volunteerToUpdate == null)
        //    {
        //        return NotFound("Volunteer not found");
        //    }
        //    if (await volunteerService.UpdateVolunteer(volunteerInfoDTO))
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update volunteer");
        //    }
        //}

        [HttpGet("{volunteerId:Guid}")]
        public async Task<IActionResult> GetVolunteerById([FromRoute]Guid volunteerId)
        {
            if (volunteerId == Guid.Empty)
            {
                return BadRequest("Id does not exist");
            }
            var volunteer = await volunteerService.GetVolunteerById(volunteerId);
            if (volunteer == null)
            {
                return NotFound("No volunteer found");
            }

            return Ok(volunteer);
        }
  
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UploadResume(IFormFile file)
        {
            Guid volunteerId;
            try
            {
                volunteerId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            }
            catch (Exception e)
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status422UnprocessableEntity,
                    Title = "Error parsing guid",
                    Detail = "Error occured while parsing guid from volunteer claims"
                };
            }

            var result = await resumeService.UploadResume(file, volunteerId);
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> DownloadResume(string fileName)
        {
            var result = await resumeService.DownloadResume(fileName);
            return File(result.Item1, result.Item2, result.Item3);
        }
    }
}
