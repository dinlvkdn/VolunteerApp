using Domain.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Volunteer.BL.Interfaces;
using Volunteer.BL.Services;

namespace Volunteer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolunteerController : ControllerBase
    {
        private readonly IVolunteerService volunteerService;

        public VolunteerController(IVolunteerService volunteerService)
        {
            this.volunteerService = volunteerService;
        }

        [HttpPost]
        public async Task<IActionResult> AdditionVolunteer([FromBody]VolunteerDTO volunteerDTO)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);


            }
            catch (Exception) { }

            return StatusCode(StatusCodes.Status500InternalServerError, "Volunteer is not addedfguyilk;!!!!!!!!!!!");

            //if (!ModelState.IsValid) return BadRequest(ModelState);

            //var checkVolunteer = await volunteerService.GetVolunteerById(volunteerDTO.IdVolunteer);

            //if (checkVolunteer != null)
            //{
            //    return BadRequest("Volunteer is already exist");
            //}
            //if (await volunteerService.AddVolunteer(volunteerDTO))
            //{
            //    return Ok();
            //}
            //else
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError, "Volunteer is not added");
            //}
        }

        [HttpDelete("{volunteerId:Guid}")]
        public async Task<IActionResult> DeleteVolunteer([FromRoute] Guid volunteerId)
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
            if (await volunteerService.DeleteVolunteer(volunteerId))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Volunteer is not deleted");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVolunteer([FromBody]VolunteerDTO volunteerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var volunteerToUpdate = await volunteerService.GetVolunteerById(volunteerDTO.IdVolunteer);
            if (volunteerToUpdate == null)
            {
                return NotFound("Volunteer not found");
            }
            if (await volunteerService.UpdateVolunteer(volunteerDTO))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update volunteer");
            }
        }

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
    }
}
