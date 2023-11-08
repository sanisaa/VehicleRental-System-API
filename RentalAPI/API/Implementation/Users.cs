using API.DataAccess;
using API.Models;
using API.Services;
using Dapper;
using System.Data.SqlClient;

namespace API.Implementation
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
        // In your service or repository
        public IList<Object> GetUser()
        {

            IEnumerable<UserDto> users;
            using (var connection = new SqlConnection(DbConnection))
            {
                users = connection.Query<UserDto>("select * from Users;");

                var listOfOrders = connection.Query("select u.id as UserId, o.VehicleId, o.OrderedOn, o.Returned from Users u LEFT JOIN Orders o ON u.id = o.userId;");


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

                            fine = extraDays * 50;
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

        
    }

    }

