using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
     public class Feedbacks : ModelBase
    {
        public int Uid { get; set; }
        public string Name { get; set; }
        public string Feedback { get; set; }
    }
}
