using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreWebApp.Application.Interfaces
{
    public interface IAuthenticatedUserService
    {
        string UserId { get; }
    }
}
