using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsApi
{
    public class ProductOrderInApi : ApiBaseType
    {
        public int? Count { get; set; }
        public int? Remains { get; set; }
        public decimal? Price { get; set; }
        public int? IdProduct { get; set; }
        public int? IdOrder { get; set; }

        public OrderApi Order { get; set; }
        public ProductApi Product { get; set; }
    }
}
