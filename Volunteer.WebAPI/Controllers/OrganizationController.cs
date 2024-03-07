using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Volunteer.BL.Interfaces;

namespace Volunteer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }


        [HttpPost]
        public async Task<IActionResult> AddOrganization([FromBody] OrganizationInfoDTO organizationInfoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var existingOrganization = await _organizationService.GetOrganizationById(organizationInfoDTO.IdOrganization);
            if (existingOrganization != null)
            {
                return BadRequest("Organization is already exist");
            }
            if (await _organizationService.AddOrganization(organizationInfoDTO))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Organization is not added");
            }
        }

        [HttpDelete("{IdOrganization:Guid}")]
        public async Task<IActionResult> DeleteOrganization([FromRoute] Guid IdOrganization)
        {
            if (IdOrganization == Guid.Empty)
            {
                return BadRequest("Id does not exist");
            }
            var organization = await _organizationService.GetOrganizationById(IdOrganization);
            if (organization == null)
            {
                return NotFound("No organization found");
            }
            if (await _organizationService.DeleteOrganization(IdOrganization))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Organization is not deleted");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrganization([FromBody] OrganizationInfoDTO organizationInfoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var organizationToUpdate = await _organizationService.GetOrganizationById(organizationInfoDTO.IdOrganization);
            if (organizationToUpdate == null)
            {
                return NotFound("Organization not found");
            }
            if (await _organizationService.UpdateOrganization(organizationInfoDTO))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update organization");
            }
        }

        [HttpGet("{IdOrganization:Guid}")]
        public async Task<IActionResult> GetOrganizationById([FromRoute] Guid IdOrganization)
        {
            if (IdOrganization == Guid.Empty)
            {
                return BadRequest("Id does not exist");
            }
            var organization = await _organizationService.GetOrganizationById(IdOrganization);
            if (organization == null)
            {
                return NotFound("No organization found");
            }

            return Ok(organization);
        }
    }
}
