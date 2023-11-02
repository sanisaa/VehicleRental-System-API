using System.ComponentModel;

namespace API.Models
{
    public enum UserType
    {
        [Description("Administrator")]
        ADMIN = 1,

        [Description("Regular User")]
        USER = 2
    }
}
