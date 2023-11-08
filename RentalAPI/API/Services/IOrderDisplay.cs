using API.Models;

namespace API.Services
{
    public interface IOrderDisplay
    {
        public IList<Orders> GetUserOrder(int userId);
        public IList<Orders> GetAllOrders();
    }
}
