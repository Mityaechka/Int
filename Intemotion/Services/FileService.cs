using Intemotion.Data;
using Intemotion.Entities;
using Intemotion.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Intemotion.Services
{
    public interface IFileService
    {
        Task<ServiceResult<FileModel>> SaveFile(IFormFile formFile);
    }

    public class FileService : BaseService, IFileService
    {
        private readonly DataContext dataContext;
        private readonly IWebHostEnvironment appEnvironment;

        public FileService(DataContext dataContext, IWebHostEnvironment appEnvironment)
        {
            this.dataContext = dataContext;
            this.appEnvironment = appEnvironment;
        }
        public async Task<ServiceResult<FileModel>> SaveFile(IFormFile formFile)
        {
            try
            {
                var filePath = $"/Files/" + DateTime.Now.Subtract(new DateTime()).TotalMilliseconds.ToString().Replace(",", "") + Path.GetExtension(formFile.FileName);
                using var fileStream = new FileStream(appEnvironment.WebRootPath + filePath, FileMode.Create);
                await formFile.CopyToAsync(fileStream);
                var file = new FileModel
                {
                    Name = formFile.FileName,
                    Path = filePath
                };
                dataContext.Files.Add(file);
                await dataContext.SaveChangesAsync();
                return Success(file);

            }
            catch (Exception e)
            {
                return Error<FileModel>("Не удалось загрузить файл");
            }
        }
    }

}
