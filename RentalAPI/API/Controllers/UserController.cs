using API.DataAccess;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _vehicle;
        private readonly VehicleDbContext _db;
        private readonly IConfiguration _configuration;

        public UserController(VehicleDbContext db, IUser vehicle, IConfiguration configuration = null)
        {
            _db = db;
            _vehicle = vehicle;
            _configuration = configuration;
        }
        [HttpGet("GetAllUsers")]
        public IActionResult GetUsers()
        {
            var users = _vehicle.GetUser();
            return Ok(users);
        }

        [HttpGet("ChangeBlockStatus/{status}/{id}")]
        public IActionResult ChangeBlockStatus(int status, int id)
        {
            if(status == 1)
            {
                _vehicle.BlockUser(id);
            }
            else
            {
                _vehicle.UnblockUser(id);
            }
            return Ok("success");
        }

        [HttpGet("ChangeEnableStatus/{status}/{id}")]
        public IActionResult ChangeEnableStatus(int status, int id)
        {
            if (status == 1)
            {
                _vehicle.ActivateUser(id);
            }
            else
            {
                _vehicle.DeactivateUser(id);
            }
            return Ok("success");
        }
    }
}
