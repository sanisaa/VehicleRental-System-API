using API.DataAccess;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleOrderController : ControllerBase
    {
        private readonly IOrder _vehicle;
        private readonly VehicleDbContext _db;
        private readonly IConfiguration _configuration;

        public VehicleOrderController(VehicleDbContext db, IOrder vehicle, IConfiguration configuration = null)
        {
            _db = db;
            _vehicle = vehicle;
            _configuration = configuration;
        }


        [HttpPost("OrderVehicle/{userId}/{vehicleId}")]
        public IActionResult OrderVehicle(int userId, int vehicleId)
        {
            var result = _vehicle.OrderVehicle(userId, vehicleId) ? "success" : "fail";
            return Ok(result);
        }
    }
}
