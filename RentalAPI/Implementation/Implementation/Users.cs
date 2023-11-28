using Dapper;
using Domain.DTO;
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
    
        public class Users : IUser
        {
            private readonly VehicleDbContext _dbContext;
            private readonly IConfiguration _configuration;
            private string DbConnection;

            public Users(VehicleDbContext dbContext, IConfiguration configuration)
            {
                _dbContext = dbContext;
                _configuration = configuration;
                DbConnection = _configuration["connectionStrings:DBConnect"] ?? "";
            }

        public Users()
        {
        }

        // In your service or repository
        public IList<Object> GetUser()
            {

                IEnumerable<UserDto> users;
                using (var connection = new SqlConnection(DbConnection))
                {
                    users = connection.Query<UserDto>("select * from Users");

                    var listOfOrders = connection.Query(" select u.id as UserId, o.VehicleId, o.OrderedOn, o.Returned, o.Status from Users u LEFT JOIN Orders o ON u.id = o.userId WHERE o.Status =1");


                    foreach (var user in users)
                    {
                        var orders = listOfOrders.Where(lo => lo.UserId == user.Id).ToList();
                        var fine = 0;
                        foreach (var order in orders)
                        {
                            if (order.VehicleId != null && order.Returned != null && order.Returned == false)
                            {
                                var orderDate = order.OrderedOn;
                                var maxDate = orderDate.AddDays(10);
                                var currentDate = DateTime.Now;

                                var extraDays = (currentDate - maxDate).Days;
                                extraDays = extraDays < 0 ? 0 : extraDays;

                                fine = extraDays * 25;
                                user.Fine += fine;
                            }
                        }
                    }
                }
                return users.Select(user => new
                {
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    user.Mobile,
                    user.Blocked,
                    user.Active,
                    user.CreatedOn,
                    user.UserType,
                    user.Fine
                }).ToList<Object>();
            }



            public void BlockUser(int userId)
            {
                using var connection = new SqlConnection(DbConnection);
                connection.Execute("update Users set Blocked = 1 where Id=@Id", new { Id = userId });

            }
            public void UnblockUser(int userId)
            {
                using var connection = new SqlConnection(DbConnection);
                connection.Execute("update Users set Blocked = 0 where Id=@Id", new { Id = userId });
            }

            public void DeactivateUser(int userId)
            {
                using var connection = new SqlConnection(DbConnection);
                connection.Execute("update Users set Active = 0 where Id=@Id", new { Id = userId });
            }
            public void ActivateUser(int userId)
            {
                using var connection = new SqlConnection(DbConnection);
                connection.Execute("update Users set Active = 1 where Id=@Id", new { Id = userId });
            }

        public bool AddFeedback(int userId, string feedback)
        {
            var result = false;
            using (var connection = new SqlConnection(DbConnection))
            {
                var sql = "select Id, CONCAT(FirstName,' ' ,LastName) as Name From Users Where Id = @Id";
                var data = connection.QueryFirstOrDefault(sql, new { Id = userId });

                var query = "INSERT INTO feedback (Uid, Name, Feedback) VALUES (@Uid, @Name, @Feedback)";
                connection.Query(query, new { Uid = data.Id, Name = data.Name, Feedback = feedback });
                result = true;
            }

            return result;
        }


        }


}
