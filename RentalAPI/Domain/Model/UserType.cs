using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public enum UserType
    {
        [Description("Administrator")]
        ADMIN = 1,

        [Description("Regular User")]
        USER = 2
    }
}
