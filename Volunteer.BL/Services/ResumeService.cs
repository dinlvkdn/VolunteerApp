﻿using Microsoft.AspNetCore.Http;
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

        public async Task<(byte[], string, string)> DownloadResume(string FileName)
        {
            try
           {
                var resume = await _dbContext.Resumes.Where(f => f.FileName == FileName).FirstOrDefaultAsync();
                var getResumePath = resume.FileUrl;
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(getResumePath, out var contentType))
                {
                    contentType = "application/octet-stream";
                }
                var readAllBytesAsync = await File.ReadAllBytesAsync(getResumePath);
                return (readAllBytesAsync, contentType, Path.Combine(getResumePath));
            }
            catch(Exception ex)
            {
                throw new ApiException()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Title = "Can't download resume",
                    Detail = "Error occured while downloading resume from server"
                };
            }
        }

        public async Task<string> UploadResume(IFormFile file, Guid volunteerId)
        {
            
            string FileName = "";
            try
            {

                FileName = volunteerId.ToString();
                var _getFilePath = GetFilePath(FileName);

                using var _FileStream = new FileStream(_getFilePath, FileMode.Create);
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
                    Title = "Can't upload image",
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
