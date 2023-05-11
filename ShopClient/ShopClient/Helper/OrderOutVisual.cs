using ModelsApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopClient.Helper
{
    public class OrderOutVisual
    {
        public int? Count { get; set; }
        public int? OrderInId { get; set; }
        public decimal? Price { get; set; }
        public decimal? Sum { get; set; }
        public decimal? Discount { get; set; }
        public ProductApi Product { get; set; }
    }
}
