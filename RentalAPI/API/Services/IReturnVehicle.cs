namespace API.Services
{
    public interface IReturnVehicle
    {
        bool ReturnVehicle(int userId, int vehicleId);
    }
}
