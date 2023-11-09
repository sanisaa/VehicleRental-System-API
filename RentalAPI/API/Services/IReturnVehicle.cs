namespace API.Services
{
    public interface IReturnVehicle
    {
        bool ReturnVehicle(int vehicleId, int userId);
    }
}
