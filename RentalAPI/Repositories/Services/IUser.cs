using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Services
{
    public interface IUser
    {
        IList<Object> GetUser();
        void BlockUser(int userId);
        void UnblockUser(int userId);
        void DeactivateUser(int userId);
        void ActivateUser(int userId);

        void AddFeedback(int userId, string feedback);
        List<Feedbacks> GetFeedback();
        
    }
}
