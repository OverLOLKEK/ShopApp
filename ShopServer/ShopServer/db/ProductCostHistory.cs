using System;
using System.Collections.Generic;

#nullable disable

namespace ShopServer.db
{
    public partial class ProductCostHistory
    {
        public int Id { get; set; }
        public int? IdProduct { get; set; }
        public decimal? WholesalePirceValue { get; set; }
        public DateTime? ChangeDate { get; set; }
        public decimal? RetailPriceValue { get; set; }

        public virtual Product IdProductNavigation { get; set; }
    }
}
