using API.DataAccess;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleReturnController : ControllerBase
    {
        private readonly IReturnVehicle _vehicle;
        private readonly VehicleDbContext _db;
        private readonly IConfiguration _configuration;

        public VehicleReturnController(VehicleDbContext db, IReturnVehicle vehicle, IConfiguration configuration)
        {
            _db = db;
            _vehicle = vehicle;
            _configuration = configuration;
        }

        [HttpPost("ReturnVehicle/{vehicleId}/{userId}")]
        public IActionResult ReturnVehicle(string vehicleId, string userId)
        {
            var result = _vehicle.ReturnVehicle(int.Parse(vehicleId), int.Parse(userId));
            if (result)
            {
                return Ok(new { message = "success"});
            }
            else
            {
                return BadRequest(new { message = "not returned" });
            }
        }

    }
}
