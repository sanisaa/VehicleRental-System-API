using API.Models;

namespace API.Services
{
    public interface IUser
    {
        IList<Object> GetUser();
        void BlockUser(int userId);
        void UnblockUser(int userId);
        void DeactivateUser(int userId);
        void ActivateUser(int userId);
    }
}
