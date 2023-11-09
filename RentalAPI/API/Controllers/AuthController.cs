using API.DataAccess;
using API.Implementation;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
       
        private readonly IDataAccess _vehicle;
        private readonly VehicleDbContext _db;
        private readonly IConfiguration _configuration;
       
        public AuthController(VehicleDbContext db,IDataAccess vehicle, IConfiguration configuration = null)
        {
            _db = db;
            _vehicle = vehicle;
            _configuration = configuration;

        }

        [HttpPost("CreateAccount")]
        public async Task<ActionResult<User>> CreateAccount(UserDto user)
        {
            if (!_vehicle.isEmailAvailable(user.Email))
            {
                return Ok("Email is not available!");
            }
           user.CreatedOn = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
           user.UserType = UserType.USER;
            
            
            var register = await _vehicle.CreateUser(user);
            return Ok("Account Created Sucessfully");
        }


       // Packages for token creation
        //System.IdentityModel.Tokens.Jwt Microsoft.IdentityModel.Tokens    Microsoft.AspNetCore.Authentication.JwtBearer
        [HttpGet("Login")]
        public IActionResult Login(string email, string password)
        {
            if (_vehicle.AuthenticateUser(email, password, out User? user))
            {
                if (user != null)
                {
                    var jwt = new Jwt(_configuration["Jwt:Key"], _configuration["Jwt:Duration"]);
                    var token = jwt.GenerateToken(user);
                    return Ok(token);
                }
            }
            return Ok("Invalid");
        }



    }
}
