using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApp.Application.DTOs.User
{
    public class ImageRequest
    {
        public IFormFile Files { get; set; }
    }
}
