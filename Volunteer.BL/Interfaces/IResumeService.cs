using Domain.DTOs;
using Microsoft.AspNetCore.Http;

namespace Volunteer.BL.Interfaces
{
    public  interface IResumeService
    {
        Task<string> UploadResume(IFormFile _IFormFile, Guid VolunteerIds);
        Task<(byte[], string, string)> DownloadResume(Guid volunteerId);
    }
}
