using API.Models;

namespace API.Services
{
    public interface IVehiclesDisplay
    {
        public IList<Vehicle> GetAllVehicles();
    }
}
