using API.DataAccess;
using API.Services;
using Dapper;
using API.Models;
using System.Data.SqlClient;

namespace API.Implementation
{
    public class VehicleDisplay : IVehiclesDisplay
    {
        private readonly VehicleDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private string DbConnection;

        public VehicleDisplay(VehicleDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            DbConnection = _configuration["connectionStrings:DBConnect"] ?? "";
        }

        public IList<Object> GetAllVehicles()
        {
            IEnumerable<Vehicle> vehicles = null;
            //getting data from database
            using (var connection = new SqlConnection(DbConnection))
            {
                var sql = "SELECT v.*, c.Category, c.SubCategory FROM Vehicles v JOIN VehicleCategories c ON v.CategoryId = c.Id;";
                vehicles = connection.Query<Vehicle, VehicleCategory, Vehicle>(
                    sql,
                    (vehicle, category) =>
                    {
                        vehicle.Category = category;
                        return vehicle;
                    },
                    splitOn: "CategoryId"
                );
            }

            //displaying it in format mentioned below and returning the list
            return vehicles.Select(vehicle => new
            {
                vehicle.Id,
                vehicle.Name,
                vehicle.Category.Category,
                vehicle.Category.SubCategory,
                vehicle.Price,
                Available = !vehicle.Ordered,
                vehicle.Brand
            }).ToList<Object>();
        }

    }
}
