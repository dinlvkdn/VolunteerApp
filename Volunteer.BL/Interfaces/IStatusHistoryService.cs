using Domain.DTOs;
using Volunteer.DAL;

namespace Volunteer.BL.Interfaces
{
    public interface IStatusHistoryService
    {
        Task<StatusHistoryDTO> AddStatusHistory(StatusHistoryDTO statusHistoryDTO);
    }
}
