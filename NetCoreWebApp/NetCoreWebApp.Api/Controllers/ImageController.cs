using Microsoft.AspNetCore.Mvc;
using NetCoreWebApp.Application.DTOs.User;
using NetCoreWebApp.Application.Interfaces;
using NetCoreWebApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : Controller
    {
        private readonly IImageServices _imageServices;
        private readonly IAuthenticatedUserService _authenticatedUser;
        public ImageController(IImageServices IImageServices, IAuthenticatedUserService IAuthenticatedUserService)
        {
            _imageServices = IImageServices;
            _authenticatedUser = IAuthenticatedUserService;
        }
        [HttpPost]
        public Task<Response<string>> Post([FromForm]ImageRequest objfile)
        {
            var id = _authenticatedUser.UserId;
            return _imageServices.UploadImage(objfile, id);
        }
    }
}
