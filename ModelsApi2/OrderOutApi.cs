using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsApi
{
   public  class OrderOutApi : ApiBaseType
    {
        public string Status { get; set; }
        public int? IdSaleType { get; set; }
        public int? IdOrder { get; set; }

        public List<ProductOrderOutApi> ProductOrderOuts { get; set; }
       
    }
}
