
using Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.DataAccess;
using Repositories.Services;

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
            var result = _vehicle.OrderVehicle(userId, vehicleId);

            // Return a JSON response with a success property
            return Ok(new { success = result });
        }

        [HttpGet("GetUserOrders/{id}")]
        public IActionResult GetUserOrders(int id)
        {
            return Ok(_vehicle.GetUserOrder(id));
        }
        [HttpGet("GetOrdersById/{id}")]
        public IActionResult GetOrdersById(int id)
        {
            return Ok(_vehicle.GetOrderById(id));
        }

        [HttpGet("GetAllOrders")]
        public IActionResult GetAllOrders()
        {
            return Ok(_vehicle.GetAllOrders());
        }
        [HttpGet("VerifyOrders")]
        public IActionResult VerifyOrders()
        {
            return Ok(_vehicle.VerifyOrder());
        }

        [HttpPost("AcceptOrder")]
        public IActionResult AcceptOrder(Orders order)
        {
            var result = _vehicle.AcceptOrder(order);

            // Return a JSON response with a success property
            return Ok(new { success = result });
        }
        [HttpPost("RejectOrder")]
        public IActionResult RejectOrder(Orders order)
        {
            var result = _vehicle.RejectOrder(order);

            // Return a JSON response with a success property
            return Ok(new { success = result });
        }

    }
}
