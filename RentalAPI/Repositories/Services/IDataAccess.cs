using Domain.DTO;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Services
{
    public interface IDataAccess
    {
        Task<string> CreateUser(UserDto user);
        bool isEmailAvailable(string email);

        bool AuthenticateUser(string email, string password, out User? user);

    }
}
