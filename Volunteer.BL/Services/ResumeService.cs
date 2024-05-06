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

        public async Task<(byte[], string, string)> DownloadResume(Guid volunteerId)//can be extracted into class instead of returning a tuple.
                                                                                    //as far as I don't see any opportunity to use this class, it can remain as tuple
        {
          
            var resume = await _dbContext.Resumes.Where(f => f.VolunteerId == volunteerId).FirstOrDefaultAsync();
            if (resume == null)
            {
                throw new ApiException()
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
            
            string FileName = "";//you don't need it here
            try
            {
                FileName = volunteerId.ToString();//can be var fileName

                var existingResume = await _dbContext.Resumes.FirstOrDefaultAsync(r => r.FileName == FileName);
                if (existingResume != null)
                {
                    throw new ApiException()
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Title = "File already exists",
                        Detail = "A file with the same name already exists in the database"
                    };
                }

                var _getFilePath = GetFilePath(FileName);//underscore in var name. also method can be renamed to BuildFileName

                using var _FileStream = new FileStream(_getFilePath, FileMode.Create);//underscore in var name. also can be awaited
                await file.CopyToAsync(_FileStream);

                var resume = new Resume
                {
                    Id = Guid.NewGuid(),
                    FileUrl = _getFilePath,
                    FileName = FileName,
                    VolunteerId = volunteerId
                };
                await _dbContext.Resumes.AddAsync(resume);

                await _dbContext.SaveChangesAsync();
                return FileName;
            }
            catch (Exception ex)
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
            var result = "C:\\Users\\Diana\\Exoft\\ResumeUploads";
            if (!Directory.Exists(result))
            {
                Directory.CreateDirectory(result);
            }

            return result;
        }

        public static string GetFilePath(string fileName)
        {
            var _GetStaticContentDirectory = GetStaticContentDirectory();

            return Path.Combine(_GetStaticContentDirectory, fileName);
        }

        public async Task<bool> SaveChangesAsync()
           => await _dbContext.SaveChangesAsync() > 0;
    }
}
