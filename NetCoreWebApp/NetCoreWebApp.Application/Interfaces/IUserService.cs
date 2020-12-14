using NetCoreWebApp.Application.DTOs.User;
using NetCoreWebApp.Application.Parameters;
using NetCoreWebApp.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApp.Application.Interfaces
{
    public interface IUserService
    {
        Task<Response<UserResponse>> GetByIdAsync(string id);
        Task<Response<string>> DeleteAsync(string id);
        Task<PagedResponse<List<UserResponse>>> GetAllAsync(RequestParameter page);

    }
}
