using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Repositories.DataAccess
{
    public class VehicleDbContext : DbContext
    {
        public VehicleDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        // public DbSet<User> Orders { get; set; }
        // public DbSet<User> VehicleCategories { get; set; }
        //public DbSet<User> Vehicles { get; set; }
    }
}
