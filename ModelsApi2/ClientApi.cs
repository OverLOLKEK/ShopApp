using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsApi
{
   public  class ClientApi : ApiBaseType 
    {
        public string Phone { get; set; }
        public string Address { get; set; }

        public int OrdersCount { get; set; }
    }
}
