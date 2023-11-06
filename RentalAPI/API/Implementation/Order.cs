using API.DataAccess;
using API.Services;
using Dapper;
using System.Data.SqlClient;

namespace API.Implementation
{
    public class Order : IOrder 
    {
        private readonly VehicleDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private string DbConnection;

        public Order(VehicleDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            DbConnection = _configuration["connectionStrings:DBConnect"] ?? "";
        }
        public bool OrderVehicle(int userId, int vehicleId)
        {
            var ordered = false;
            using (var connection = new SqlConnection(DbConnection))
            {
                var sql = $"insert into Orders (UserId, VehicleId, OrderedOn, Returned) values ({userId}, {vehicleId}, '{DateTime.Now:yyyy-MM-dd HH:mm:ss}',0);";
                var inserted = connection.Execute(sql) == 1;
                if (inserted)
                {
                    sql = $"update Vehicles set Ordered = 1 where Id={vehicleId}";
                    var updated = connection.Execute(sql) == 1;
                    ordered = updated;
                }
            }
            
            return ordered;
          
        }
    }
}
