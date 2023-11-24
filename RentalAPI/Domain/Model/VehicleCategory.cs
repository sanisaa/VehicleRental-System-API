using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class VehicleCategory : ModelBase
    {
        public string? Category { get; set; }
        public string? SubCategory { get; set; }
    }
}
