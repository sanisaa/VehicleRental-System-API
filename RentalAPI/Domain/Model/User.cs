using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class User : ModelBase
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public bool Blocked { get; set; } = false;
        public bool Active { get; set; } = true;
        //public float Fine { get; set; } = 0;
        public string UserType { get; set; }
        public string? CreatedOn { get; set; }
    }
}
