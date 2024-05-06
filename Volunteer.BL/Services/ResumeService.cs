using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Volunteer.BL.Helper.Exceptions;
using Volunteer.BL.Interfaces;
using Volunteer.DAL.DataAccess;
using Volunteer.DAL.Models;

namespace Volunteer.BL.Services
{
    public class ResumeService : IResumeService
    {
        private readonly VolunteerDBContext _dbContext;

        public ResumeService(VolunteerDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(byte[], string, string)> DownloadResume(Guid volunteerId)
        {
            var resume = await _dbContext.Resumes.Where(f => f.VolunteerId == volunteerId).FirstOrDefaultAsync();
            if (resume == null)
            {
                throw new ApiException
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Title = "Can`t find resume",
                    Detail = "Error occured while finding resume"
                };
            }
            var getResumePath = resume.FileUrl;
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(getResumePath, out var contentType))
            {
                contentType = "application/pdf";
            }
            var readAllBytesAsync = await File.ReadAllBytesAsync(getResumePath);
            return (readAllBytesAsync, contentType, Path.Combine(getResumePath));
        }
         
        public async Task<string> UploadResume(IFormFile file, Guid volunteerId)
        {
            string fileName;
            try
            {
                fileName = volunteerId.ToString();

                var existingResume = await _dbContext.Resumes.FirstOrDefaultAsync(r => r.FileName == fileName);
                if (existingResume != null)
                {
                    throw new ApiException()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Title = "File already exists",
                        Detail = "A file with the same name already exists in the database"
                    };
                }

                var getFilePath = GetFilePath(fileName);

                await using var fileStream = new FileStream(getFilePath, FileMode.Create);
                await file.CopyToAsync(fileStream);

                var resume = new Resume
                {
                    Id = Guid.NewGuid(),
                    FileUrl = getFilePath,
                    FileName = fileName,
                    VolunteerId = volunteerId
                };
                await _dbContext.Resumes.AddAsync(resume);

                await _dbContext.SaveChangesAsync();
                return fileName;
            }
            catch (Exception)
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Title = "Can't upload file",
                    Detail = "Error occured while uploading file on server"
                };
            }
        }


        public static string GetStaticContentDirectory()
        {
            const string result = "C:\\Users\\Diana\\Exoft\\ResumeUploads";
            if (!Directory.Exists(result))
            {
                Directory.CreateDirectory(result);
            }

            return result;
        }

        public static string GetFilePath(string fileName)
        {
            var getStaticContentDirectory = GetStaticContentDirectory();

            return Path.Combine(getStaticContentDirectory, fileName);
        }

        public async Task<bool> SaveChangesAsync()
           => await _dbContext.SaveChangesAsync() > 0;
    }
}
