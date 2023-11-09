using API.DataAccess;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehiclesOperation _vehicle;
        private readonly VehicleDbContext _db;
        private readonly IConfiguration _configuration;

        public VehicleController(VehicleDbContext db, IVehiclesOperation vehicle, IConfiguration configuration = null)
        {
            _db = db;
            _vehicle = vehicle;
            _configuration = configuration;
        }


        [HttpGet("GetAllVehicles")]
        public IActionResult GetAllVehicles()
        {
            var vehiclesToSend = _vehicle.GetAllVehicles();
            return Ok(vehiclesToSend);
        }

        [HttpPost("InsertVehicle")]
        public IActionResult InsertVehicle(Vehicle vehicle)
        {
            vehicle.Name = vehicle.Name.Trim();
            vehicle.Brand = vehicle.Brand.Trim();
            vehicle.Category.Category = vehicle.Category.Category.ToLower();
            vehicle.Category.SubCategory = vehicle.Category.SubCategory.ToLower(); 
            
            _vehicle.InsertNewVehicle(vehicle);
            return Ok("Inserted");

        }

        [HttpDelete("DeleteVehicle/{id}")]
        public IActionResult DeleteVehicle(int id)
        {
            var result = _vehicle.DeleteVehicle(id);
            if (result)
            {
                return Ok(new { message = "success" });
            }
            else
            {
                return BadRequest(new { message = "Vehicle not found" });
            }
        }
        [HttpPost("InsertCategory")]
        public IActionResult InsertCategory(VehicleCategory vehicleCategory)
        {
            vehicleCategory.Category= vehicleCategory.Category.ToLower();
            vehicleCategory.SubCategory = vehicleCategory.SubCategory.ToLower();
            _vehicle.CreateCategory(vehicleCategory);
            return Ok("Inserted");
        }
    }
}
