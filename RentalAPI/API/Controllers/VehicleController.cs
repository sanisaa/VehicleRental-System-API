using API.DataAccess;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehiclesDisplay _vehicle;
        private readonly VehicleDbContext _db;
        private readonly IConfiguration _configuration;

        public VehicleController(VehicleDbContext db, IVehiclesDisplay vehicle, IConfiguration configuration = null)
        {
            _db = db;
            _vehicle = vehicle;
            _configuration = configuration;
        }


        [HttpGet("GetAllVehicles")]
        public IActionResult GetAllVehicles()
        {
            var v = _vehicle.GetAllVehicles();
            var vehiclesToSend = v.Select(vehicle => new
            {
                vehicle.Id,
                vehicle.Name,
                vehicle.Category.Category,
                vehicle.Category.SubCategory,
                vehicle.Price,
                Available = !vehicle.Ordered,
                vehicle.Brand
            }).ToList();
            return Ok(vehiclesToSend);
        }
    }
}
