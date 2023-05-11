using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsApi
{
    public class ProductOrderOutApi
    {
        public int IdProductOrderIn { get; set; }
        public int IdOrderOut { get; set; }
        public int? Count { get; set; }
        public decimal? Price { get; set; }
        public decimal? Discount { get; set; }

        public ProductApi Product { get; set; }
    }
}
