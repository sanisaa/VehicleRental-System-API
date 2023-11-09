using API.Models;

namespace API.Services
{
    public interface IVehiclesOperation
    {
        public IList<Object> GetAllVehicles();
        void InsertNewVehicle(Vehicle vehicle);
        bool DeleteVehicle(int vehicleId);
        void CreateCategory(VehicleCategory vehicleCategory);
    }
}
