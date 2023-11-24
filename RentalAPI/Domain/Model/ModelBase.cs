using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{ 
        public class ModelBase
        {
            public int Id { get; set; }
        }
   

    //will inherit all the models through this
    //contains only Id to prevent writing Id again and again
    
}
