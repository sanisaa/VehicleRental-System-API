using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class UserDto : ModelBase
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? Password { get; set; }
        public bool Blocked { get; set; } = false;
        public bool Active { get; set; } = true;
        public float Fine { get; set; } = 0;
        public UserType UserType { get; set; }
        public string? CreatedOn { get; set; }
    }
}
