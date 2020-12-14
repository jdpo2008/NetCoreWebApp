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
            if (objfile.Files.Length > 0)
            {
                try
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\uploads\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\uploads\\");
                    }
                    using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\uploads\\" + id + "_" + objfile.Files.FileName))
                    {
                        objfile.Files.CopyTo(filestream);
                        filestream.Flush();
                        return new Response<string>("\\uploads\\" + id + "_" + objfile.Files.FileName);
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
    }
}
