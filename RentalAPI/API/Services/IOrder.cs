namespace API.Services
{
    public interface IOrder
    {
        bool OrderVehicle(int userId, int vehicleId);
    }
}
