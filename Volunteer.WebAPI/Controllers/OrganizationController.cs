using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Volunteer.BL.Helper.Exceptions;
using Volunteer.BL.Interfaces;
using Volunteer.BL.Services;

namespace Volunteer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Organization")]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;
        private readonly IGuidValidationService guidValidationService;

        public OrganizationController(IOrganizationService organizationService, IGuidValidationService guidValidationService)
        {
            _organizationService = organizationService;
            this.guidValidationService = guidValidationService;
        }


        [HttpPost]
        public async Task<IActionResult> AddOrganization(OrganizationInfoDTO organizationInfoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var id = await guidValidationService.GetIdFromClaims(HttpContext.User);

            var existingOrganization = await _organizationService.AddOrganization(id, organizationInfoDTO);

            if (existingOrganization != null)
            {
                return Ok(existingOrganization);
            }
            else
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Title = "Organization doesn't exist",
                    Detail = "Organization doesn't exist while creating organization"
                };
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteOrganization([FromRoute] Guid id)
        {
            await guidValidationService.CheckForEmptyGuid(id);

            var organization = await _organizationService.GetOrganizationById(id);

            if (organization == null)
            {
                return NotFound("No organization found");
            }

            if (await _organizationService.DeleteOrganization(id))
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
                return BadRequest();
            }

            var id = await guidValidationService.GetIdFromClaims(HttpContext.User);

            var organizationToUpdate = await _organizationService.GetOrganizationById(id);
            
            if (organizationToUpdate == null)
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Title = "Organization doesn't exist",
                    Detail = "No organization on database"
                };
            }
            var organization = await _organizationService.UpdateOrganization(id, organizationInfoDTO);
            
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

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetOrganizationById([FromRoute] Guid id)
        {
            await guidValidationService.CheckForEmptyGuid(id);

            var organization = await _organizationService.GetOrganizationById(id);

            if (organization == null)
            {
                return NotFound("No organization found");
            }

            return Ok(organization);
        }
    }
}
