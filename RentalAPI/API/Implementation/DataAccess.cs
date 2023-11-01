using API.DataAccess;
using API.Models;
using API.Services;
using AutoMapper;
using Dapper;
using System.Data.SqlClient;

namespace API.Implementation
{
    public class DataAccess : IDataAccess
    {
        private readonly VehicleDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly string DbConnection;
       

        public DataAccess(VehicleDbContext dbContext, IConfiguration configuration, IMapper mapper)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _mapper = mapper;
            DbConnection = _configuration["connectionStrings:DBConnect"] ?? "";

        }
        public async Task<string> CreateUser(UserDto user)
        {
            //using (var connection = new SqlConnection(DbConnection))
            //{
            //    var sql = "INSERT INTO Users (FirstName, LastName, Email, Mobile, Password, Blocked, Active, CreatedOn, UserType) " +
            //              "VALUES (@FirstName, @LastName, @Email, @Mobile, @Password, @Blocked, CAST(@Active AS BIT), @CreatedOn, @UserType);";

            //    var result = await connection.ExecuteAsync(sql, user);

            //    return result > 0 ? "Account Created Successfully" : "Account Creation Failed";
            //}
            var add = _mapper.Map<User>(user);
            await _dbContext.Users.AddAsync(add);
            await _dbContext.SaveChangesAsync();
            return "ok";
        }


        public bool isEmailAvailable(string email)
        {
            var result = false;
            using (var connection = new SqlConnection(DbConnection))
            {
                result = connection.ExecuteScalar<bool>("select count(*) from Users where Email = @email;", new { email });

            }

            return !result;
            //sending negative value if 0 then return true and if 1 then return false
        }

        public bool AuthenticateUser(string email, string password, out User? user)
        {
            var result = false;
            using (var connection = new SqlConnection(DbConnection))
            {
                result = connection.ExecuteScalar<bool>("select count(1) from Users where email=@email and password=@password;", new { email, password });
                if (result)
                {
                    user = connection.QueryFirst<User>("select * from Users where email=@email;", new { email });
                }
                else
                {
                    user = null;
                }
            }
            return result;
        }
    }
}
