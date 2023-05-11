using System;
using System.Collections.Generic;

#nullable disable

namespace ShopServer.db
{
    public partial class ProductOrderOut
    {
        public int IdProductOrderIn { get; set; }
        public int IdOrderOut { get; set; }
        public int? Count { get; set; }
        public decimal? Price { get; set; }
        public decimal? Discount { get; set; }

        public virtual OrderOut IdOrderOutNavigation { get; set; }
        public virtual ProductOrderIn IdProductOrderInNavigation { get; set; }
    }
}
