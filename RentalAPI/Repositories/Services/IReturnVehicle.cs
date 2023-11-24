using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Services
{
    public interface IReturnVehicle
    {
        bool ReturnVehicle(int vehicleId, int userId);
    }
}
