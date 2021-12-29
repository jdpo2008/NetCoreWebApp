using Microsoft.AspNetCore.Hosting;
using NetCoreWebApp.Application.DTOs.User;
using NetCoreWebApp.Application.Interfaces;
using NetCoreWebApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApp.WebApi.Services
{
    public class ImageService : IImageServices
    {
        public static IWebHostEnvironment _environment;
        public ImageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async  Task<Response<String>> UploadImage(ImageRequest objfile, string id)
        {
            if (objfile.File.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\uploads\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\uploads\\");
                    }
                    using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\uploads\\" + id + "_" + objfile.File.FileName))
                    {
                        objfile.File.CopyTo(filestream);
                        filestream.Flush();
                        return new Response<string>("\\uploads\\" + id + "_" + objfile.File.FileName);
                    }
                }
                catch (Exception ex)
                {
                    return new Response<string>(ex.ToString());
                }
            }
            else
            {
                return new Response<string>("Unsuccessful");
            }
        }

        public Task<Response<string>> DownloadFile(string fileName)
        {
            throw new NotImplementedException();
        }

        public Task<Response<string>> UploadAsync(Stream fileStream, string fileName, string contentType)
        {
            throw new NotImplementedException();
        }
    }
}
