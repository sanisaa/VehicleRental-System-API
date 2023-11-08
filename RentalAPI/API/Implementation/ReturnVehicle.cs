using API.DataAccess;
using API.Services;
using Dapper;
using System.Data.SqlClient;

namespace API.Implementation
{
    public class ReturnVehicle : IReturnVehicle
    {
        private readonly VehicleDbContext _dbContext;
        private readonly IConfiguration _config;
        private readonly string DbConnection;

        public ReturnVehicle(VehicleDbContext dbContext, IConfiguration config)
        {
            _dbContext = dbContext;
            _config = config;
            DbConnection = _config["connectionStrings:DBConnect"] ?? "";
        }
        bool IReturnVehicle.ReturnVehicle(int vehicleId, int userId)
        {
            var returned = false;
            using(var connection = new SqlConnection(DbConnection))
            {
                var sql = $"update Vehicles set Ordered=0 where Id=@Id;";
                connection.Execute(sql, new {Id = vehicleId });
                sql = $"update Orders set Returned = 1 where UserId = @uId and VehicleId = @vId;";
                returned = connection.Execute(sql, new {uId = userId, vId = vehicleId}) == 1;
            }
            return returned;
            
        }
    }
}
