using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Orders : ModelBase
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public int VehicleId { get; set; }
        public string? VehicleName { get; set; }
        public DateTime OrderedOn { get; set; }
        public int Returned { get; set; }
        public int Status { get; set; }
        public int OrderId { get; set; }
        public double Price { get; set; }
    }
}
