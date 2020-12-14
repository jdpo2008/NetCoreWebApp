using NetCoreWebApp.Application.DTOs.User;
using NetCoreWebApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApp.Application.Interfaces
{
    public interface IImageServices
    {
        Task<Response<String>> UploadImage(ImageRequest file, string id);
    }
}
