using NetCoreWebApp.Application.DTOs.User;
using NetCoreWebApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApp.Application.Interfaces
{
    public interface IImageServices
    {
        Task<Response<String>> UploadImage(ImageRequest file, string id);
        //Task<Response<String>> UploadBlodStorage(ImageRequest file, string id);
        Task<Response<String>> UploadAsync(Stream fileStream, string fileName, string contentType);
        Task<Response<String>> DownloadFile(string fileName);
    }
}
