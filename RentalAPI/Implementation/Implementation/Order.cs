using Dapper;
using Domain.Model;
using Microsoft.Extensions.Configuration;
using Repositories.DataAccess;
using Repositories.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Implementation
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
                var sql = $"insert into Verify (UserId, VehicleId, OrderedOn, Returned, Status) values ({userId}, {vehicleId}, '{DateTime.Now:yyyy-MM-dd HH:mm:ss}',0,0);";
                var inserted = connection.Execute(sql) == 1;
                if (inserted)
                {
                    var orderSql = $"INSERT INTO Orders (UserId, VehicleId, OrderedOn, Returned, Status) VALUES ({userId}, {vehicleId}, '{DateTime.Now:yyyy-MM-dd HH:mm:ss}',0,0);";
                    var insertedOrder = connection.Execute(orderSql) == 1;

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
                            o.OrderedOn, o.Returned, o.Status
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
                             where o.Id is NOT NULL AND o.Status = 1;";
                orders = connection.Query<Orders>(sql);
            }
            return orders.ToList();
        }

        public IList<Orders> VerifyOrder()
        {
            IEnumerable<Orders> orders;
            using (var connection = new SqlConnection(DbConnection))
            {
                var sql = @"select v.Id, u.Id as UserId, CONCAT(u.FirstName, ' ', u.LastName) as Name, 
                            ve.Id as VehicleId, ve.Name as VehicleName, 
                            v.OrderedOn, v.Returned, v.Status, 
							o.Id as OrderId
                            from Users u 
                             LEFT JOIN Orders o ON u.Id = o.UserId
                            LEFT JOIN Verify v ON u.Id = v.UserId AND o.VehicleId = v.VehicleId AND o.OrderedOn = v.OrderedOn
                             LEFT JOIN Vehicles ve ON o.VehicleId = ve.Id 
                             where o.Id is NOT NULL;";
                orders = connection.Query<Orders>(sql);
            }
            return orders.ToList();
        }
        public bool AcceptOrder(Orders order)
        {
            var ordered = false;
            using (var connection = new SqlConnection(DbConnection))
            {
                var sql = $"update Verify set Status = 1 where Id=@Id";
                var updated = connection.Execute(sql, new { Id = order.Id }) == 1;
                ordered = updated;
                if (updated)
                {
                    var query = $"update Orders set Status = 1 where Id=@OrderId";
                    connection.Execute(query, new { OrderId = order.OrderId });

                }
            }

            return ordered;
        }

        public bool RejectOrder(Orders order)
        {
            var ordered = false;
            using (var connection = new SqlConnection(DbConnection))
            {
                var sql = $"update Verify set Status = 2 where Id=@Id";
                var updated = connection.Execute(sql, new { Id = order.Id }) == 1;
                ordered = updated;
                if (updated)
                {
                    var query = $"update Orders set Status = 2 where UserId=@Id AND VehicleId=@vId";
                    connection.Execute(query, new { Id = order.UserId, vId = order.VehicleId });

                    var query2 = "update Vehicles set Ordered = 0 where Id=@vId";
                    connection.Execute(query2, new { vId = order.VehicleId });

                }
            }

            return ordered;
        }

        public IList<Orders> GetOrderById(int id)
        {
            IEnumerable<Orders> order;
            using (var connection = new SqlConnection(DbConnection))
            {

                var sql = @"select v.Id, u.Id as UserId, CONCAT(u.FirstName, ' ', u.LastName) as Name, 
                            ve.Id as VehicleId, ve.Name as VehicleName, ve.Price as Price,
                            v.OrderedOn, v.Status, 
							o.Id as OrderId
                            from Users u 
                             LEFT JOIN Orders o ON u.Id = o.UserId
                            LEFT JOIN Verify v ON u.Id = v.UserId AND o.VehicleId = v.VehicleId AND o.OrderedOn = v.OrderedOn
                             LEFT JOIN Vehicles ve ON o.VehicleId = ve.Id 
                             where o.Id = @oid";
                order = connection.Query<Orders>(sql, new { oid = id });
            }
            return order.ToList();
        }

    }

}
