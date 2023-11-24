using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Services
{
    public interface IVehiclesOperation
    {
        public IList<Object> GetAllVehicles();
        void InsertNewVehicle(Vehicle vehicle);
        bool DeleteVehicle(int vehicleId);
        void CreateCategory(VehicleCategory vehicleCategory);
    }
}
