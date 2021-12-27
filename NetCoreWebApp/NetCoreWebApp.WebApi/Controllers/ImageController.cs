using Microsoft.AspNetCore.Mvc;
using NetCoreWebApp.Application.DTOs.User;
using NetCoreWebApp.Application.Interfaces;
using NetCoreWebApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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

        //[Route("upload")]
        //[HttpPost]
        //public Task<Response<string>> Upload([FromForm] ImageRequest model)
        //{
        //    return _imageServices.UploadBlodStorage(model, _authenticatedUser.UserId);

        //}

        [Route("upload")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> UploadAsync([FromForm] ImageRequest model)
        {
            try
            {
                //var formCollection = await Request.ReadFormAsync();
                //var file = formCollection.Files.First();
                var file = model.File;
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    Response<string> fileURL = await _imageServices.UploadAsync(file.OpenReadStream(), fileName, file.ContentType);
                    return Ok(fileURL);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
