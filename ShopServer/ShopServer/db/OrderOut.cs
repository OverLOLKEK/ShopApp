using System;
using System.Collections.Generic;

#nullable disable
//
namespace ShopServer.db
{
    public partial class OrderOut
    {
        public OrderOut()
        {
            ProductOrderOuts = new HashSet<ProductOrderOut>();
        }

        public int Id { get; set; }
        public int? IdSaleType { get; set; }
        public string Status { get; set; }
        public int? IdOrder { get; set; }

        public virtual Order IdOrderNavigation { get; set; }
        public virtual SaleType IdSaleTypeNavigation { get; set; }
        public virtual ICollection<ProductOrderOut> ProductOrderOuts { get; set; }
    }
}
