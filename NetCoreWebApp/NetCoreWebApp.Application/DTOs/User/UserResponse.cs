using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreWebApp.Application.DTOs.User
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }

    }
}
