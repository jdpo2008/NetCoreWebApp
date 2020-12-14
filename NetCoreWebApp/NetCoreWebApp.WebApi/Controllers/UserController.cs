using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebApp.Application.DTOs.User;
using NetCoreWebApp.Application.Interfaces;
using NetCoreWebApp.Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService UserService)
        {
            _userService = UserService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {

            return Ok(await _userService.GetByIdAsync(id));
        }

        [HttpPost("getAll")]
        public async Task<IActionResult> GetAllAsync(RequestParameter page)
        {
            return Ok(await _userService.GetAllAsync(page));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Super Admin")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            return Ok(await _userService.DeleteAsync(id));
        }
    }
}
