using API.DataAccess;
using API.Models;
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
        public IList<Orders> GetUserOrder(int userId)
        {
            IEnumerable<Orders> order;
            using (var connection = new SqlConnection(DbConnection))
            {
                var sql = @"select o.Id, u.Id as UserId, CONCAT(u.FirstName, ' ', u.LastName) as Name, 
                            v.Id as VehicleId, v.Name as VehicleName, 
                            o.OrderedOn, o.Returned
                            from Users u 
                            LEFT JOIN Orders o ON u.Id = o.UserId                            
                             LEFT JOIN Vehicles v ON o.VehicleId = v.Id 
                            where o.UserId IN (@Id);";
                order = connection.Query<Orders>(sql, new { Id = userId });
            }
            return order.ToList();
        }
        public IList<Orders> GetAllOrders()
        {
            IEnumerable<Orders> orders;
            using (var connection = new SqlConnection(DbConnection))
            {
                var sql = @"select o.Id, u.Id as UserId, CONCAT(u.FirstName, ' ', u.LastName) as Name, 
                            v.Id as VehicleId, v.Name as VehicleName, 
                            o.OrderedOn, o.Returned
                            from Users u 
                            LEFT JOIN Orders o ON u.Id = o.UserId                            
                             LEFT JOIN Vehicles v ON o.VehicleId = v.Id 
                             where o.Id is NOT NULL;";
                orders = connection.Query<Orders>(sql);
            }
            return orders.ToList();
        }
    }
}
