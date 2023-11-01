using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DataAccess
{
    public class VehicleDbContext: DbContext
    {
        public VehicleDbContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<User> Users { get; set; }
       // public DbSet<User> Orders { get; set; }
       // public DbSet<User> VehicleCategories { get; set; }
        //public DbSet<User> Vehicles { get; set; }
    }
}
