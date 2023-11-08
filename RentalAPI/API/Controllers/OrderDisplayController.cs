using API.DataAccess;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDisplayController : ControllerBase
    {
        private readonly IOrderDisplay _vehicle;
        private readonly VehicleDbContext _db;
        private readonly IConfiguration _configuration;

        public OrderDisplayController(VehicleDbContext db, IOrderDisplay vehicle, IConfiguration configuration = null)
        {
            _db = db;
            _vehicle = vehicle;
            _configuration = configuration;
        }
        
        [HttpGet("GetUserOrders/{id}")]
        public IActionResult GetUserOrders(int id)
        {
            return Ok(_vehicle.GetUserOrder(id));
        }

        [HttpGet("GetAllOrders")]
        public IActionResult GetAllOrders()
        {
            return Ok(_vehicle.GetAllOrders());
        }

    }
}
