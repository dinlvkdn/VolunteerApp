using Domain.DTOs;
using Volunteer.BL.Interfaces;
using Volunteer.DAL;
using Volunteer.DAL.DataAccess;

namespace Volunteer.BL.Services
{
    public class StatusHistoryService : IStatusHistoryService
    {
        private readonly VolunteerDBContext _dbContext;

        public StatusHistoryService(VolunteerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StatusHistoryDTO> AddStatusHistory(StatusHistoryDTO statusHistoryDTO)
        {
            var statusHistory = new StatusHistory
            {
                Id = statusHistoryDTO.StatusHistoryId,
                Time = statusHistoryDTO.Time,
                OrganizationId = statusHistoryDTO.OrganizationId,
                Status = statusHistoryDTO.Status
            };

            await _dbContext.StatusHistory.AddAsync(statusHistory);
            await _dbContext.SaveChangesAsync();

            return statusHistoryDTO;
        }
    }
}
