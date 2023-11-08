using API.DataAccess;
using API.Models;
using API.Services;
using Dapper;
using System.Data.SqlClient;

namespace API.Implementation
{
    public class OrderDisplay : IOrderDisplay
    {
        private readonly VehicleDbContext _dbContext;
        private readonly IConfiguration _config;
        private readonly string Dbconnection;

        public OrderDisplay(VehicleDbContext dbContext, IConfiguration config )
        {
            _dbContext = dbContext;
            _config = config;
            Dbconnection = _config["connectionStrings:DBConnect"] ?? "";
        }
        public IList<Orders> GetUserOrder(int userId)
        {
            IEnumerable<Orders> order;
            using (var connection = new SqlConnection(Dbconnection))
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
            using(var connection = new SqlConnection(Dbconnection))
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
