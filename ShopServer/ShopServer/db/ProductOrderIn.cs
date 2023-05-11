using System;
using System.Collections.Generic;

#nullable disable

namespace ShopServer.db
{
    public partial class ProductOrderIn
    {
        public ProductOrderIn()
        {
            ProductOrderOuts = new HashSet<ProductOrderOut>();
        }

        public int Id { get; set; }
        public int? IdProduct { get; set; }
        public int? IdOrder { get; set; }
        public int? Count { get; set; }
        public decimal? Price { get; set; }
        public int? Remains { get; set; }

        public virtual Order IdOrderNavigation { get; set; }
        public virtual Product IdProductNavigation { get; set; }
        public virtual ICollection<ProductOrderOut> ProductOrderOuts { get; set; }
    }
}
