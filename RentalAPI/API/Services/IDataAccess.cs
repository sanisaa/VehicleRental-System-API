using API.Models;

namespace API.Services
{
    public interface IDataAccess
    {
        Task<string> CreateUser(UserDto user);
        bool isEmailAvailable(string email);
        bool AuthenticateUser(string email, string password, out User? user);
    }
}
