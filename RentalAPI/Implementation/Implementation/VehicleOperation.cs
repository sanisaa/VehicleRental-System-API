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
        public class VehicleOperation : IVehiclesOperation
        {
            private readonly VehicleDbContext _dbContext;
            private readonly IConfiguration _configuration;
            private string DbConnection;

            public VehicleOperation(VehicleDbContext dbContext, IConfiguration configuration)
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

            public void InsertNewVehicle(Vehicle vehicle)
            {
                using var connection = new SqlConnection(DbConnection);
                var sql = @"Select Id from VehicleCategories where Category = @cat AND SubCategory = @subCat;";
                var parameter1 = new
                {
                    cat = vehicle.Category.Category,
                    subCat = vehicle.Category.SubCategory
                };
                var categoryId = connection.ExecuteScalar<int>(sql, parameter1);

                sql = "insert into Vehicles (Name, Brand, Price, Ordered, CategoryId) values (@name, @brand, @price, @ordered, @catId);";
                var parameter2 = new
                {
                    name = vehicle.Name,
                    brand = vehicle.Brand,
                    price = vehicle.Price,
                    ordered = false,
                    catId = categoryId
                };
                connection.Execute(sql, parameter2);
            }

            public bool DeleteVehicle(int vehicleId)
            {
                var deleted = false;
                using (var connection = new SqlConnection(DbConnection))
                {
                    var sql = $"delete from Vehicles where Id=@Id;";
                    deleted = connection.Execute(sql, new { Id = vehicleId }) == 1;
                }
                return deleted;
            }

            public void CreateCategory(VehicleCategory vehicleCategory)
            {
                using var connection = new SqlConnection(DbConnection);
                var parameter = new
                {
                    cat = vehicleCategory.Category,
                    subcat = vehicleCategory.SubCategory
                };
                var sql = "insert into VehicleCategories (category, subcategory) values (@cat, @subcat);";
                connection.Execute(sql, parameter);
            }

        }
    

}
