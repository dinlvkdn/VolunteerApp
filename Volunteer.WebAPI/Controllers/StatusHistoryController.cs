using Domain.DTOs;
using Domain.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volunteer.BL.Helper.Exceptions;
using Volunteer.BL.Interfaces;
using Volunteer.BL.Services;//not needed
using Volunteer.DAL.Models;

namespace Volunteer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusHistoryController : ControllerBase
    {
        private readonly IStatusHistoryService statusHistoryService;
        private readonly IOrganizationService organizationService;

        public StatusHistoryController(IStatusHistoryService statusHistoryService, IOrganizationService organizationService)
        {
            this.statusHistoryService = statusHistoryService;
            this.organizationService = organizationService;
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("deleteOrganization/{organizationId:Guid}")]
        public async Task<IActionResult> DeleteOrganization([FromRoute] Guid organizationId)
        {
            var auth = Request.Headers.Authorization;//same as in OrganizationController - can be extracted and reused
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", auth.ToString());

            var response = await client.DeleteAsync($"{Constants.ngrok}/api/User?userId={organizationId}");
            if (response.IsSuccessStatusCode)
            {
                if (await organizationService.DeleteOrganization(organizationId))
                {
                    return NoContent();
                }
            }

            throw new ApiException()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Title = "Organization is not deleted",
                Detail = "Error occured while deleting organization on server"
            };
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getAllOrganizations")]
        public async Task<IActionResult> GetAllOrganizations([FromQuery] PaginationFilter filter, CancellationToken cancellationToken)
        {
            var organizations = await statusHistoryService.GetAllOrganizations(filter, cancellationToken);

            if (organizations != null)
            {
                return Ok(organizations);
            }

            throw new ApiException()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Title = "Error retrieving data from the database",
                Detail = "Error retrieving data from the database"
            };
        }
    }
}

