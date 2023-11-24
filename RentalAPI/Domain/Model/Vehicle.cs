using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Vehicle : ModelBase
    {
        public string? Name { get; set; }
        public string? Brand { get; set; }
        public float Price { get; set; } = 0;
        public bool Ordered { get; set; } = false;
        public int CategoryId { get; set; }
        public VehicleCategory Category { get; set; } = new VehicleCategory();
    }
}
