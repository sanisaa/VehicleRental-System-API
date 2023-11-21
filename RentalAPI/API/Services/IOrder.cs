using API.Models;

namespace API.Services
{
    public interface IOrder
    {
        bool OrderVehicle(int userId, int vehicleId);
        public IList<Orders> GetUserOrder(int userId);
        public IList<Orders> GetAllOrders();

        public IList<Orders> VerifyOrder();
        public bool AcceptOrder(Orders order);
        public bool RejectOrder(Orders order);


    }
}
