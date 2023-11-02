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

        public IList<Models.Vehicle> GetAllVehicles()
        {
            IEnumerable<Vehicle> vehicles = null;
            using (var connection = new SqlConnection(DbConnection))
            {
                var sql = "select * from Vehicles;";
                vehicles = connection.Query<Vehicle>(sql);
                foreach(var vehicle in vehicles)
                {
                    sql = "select * from VehicleCategories where Id =" + vehicle.CategoryId;
                    vehicle.Category = connection.QuerySingle<VehicleCategory>(sql);
                }
            }
            return (IList<Models.Vehicle>)vehicles.ToList();
        }
    }
}
