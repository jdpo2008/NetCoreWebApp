using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetCoreWebApp.Application.DTOs.User;
using NetCoreWebApp.Application.Exceptions;
using NetCoreWebApp.Application.Interfaces;
using NetCoreWebApp.Application.Parameters;
using NetCoreWebApp.Application.Wrappers;
using NetCoreWebApp.Infrastructure.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApp.Infrastructure.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Response<UserResponse>> GetByIdAsync(string id)
        {
            ApplicationUser obj = await _userManager.FindByIdAsync(id);

            if (obj == null)
                throw new ApiException($"User Not Found.");

            UserResponse user = new UserResponse
            {
                Id = obj.Id.ToString(),
                Firstname = obj.FirstName,
                LastName = obj.LastName,
                Username = obj.UserName,
                Email = obj.Email,
                EmailConfirmed = obj.EmailConfirmed,
                Created = obj.Created,
                LastModified = obj.LastModified
            };

            return new Response<UserResponse>(user);
        }

        public async Task<Response<string>> DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
               throw new ApiException($"User Not Found.");

            await _userManager.DeleteAsync(user);

            return new Response<string>(id, message: "User Deleted Successfull");
        }

        public async Task<PagedResponse<List<UserResponse>>> GetAllAsync(RequestParameter page)
        {
            List<UserResponse> list = new List<UserResponse>();

            List<ApplicationUser> user = await _userManager.Users.Skip<ApplicationUser>((page.PageNumber - 1) * page.PageSize)
                .Take(page.PageSize)
                .AsNoTracking()
                .ToListAsync();

            foreach (var item in user)
            {
                UserResponse obj = new UserResponse
                {
                    Id = item.Id.ToString(),
                    Firstname = item.FirstName,
                    LastName = item.LastName,
                    Username = item.UserName,
                    Email = item.Email,
                    EmailConfirmed = item.EmailConfirmed,
                    Created = item.Created,
                    LastModified = item.LastModified,
                };

                list.Add(obj);
            }

            return new PagedResponse<List<UserResponse>>(list, page.PageNumber, page.PageSize);
        }

      
    }
}
