using System;
using System.Collections.Generic;

#nullable disable
//
namespace ShopServer.db
{
    public partial class Order
    {
        public Order()
        {
            OrderOuts = new HashSet<OrderOut>();
            ProductOrderIns = new HashSet<ProductOrderIn>();
        }

        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int? IdClient { get; set; }
        public int? IdActionType { get; set; }

        public virtual ActionType IdActionTypeNavigation { get; set; }
        public virtual Client IdClientNavigation { get; set; }
        public virtual ICollection<OrderOut> OrderOuts { get; set; }
        public virtual ICollection<ProductOrderIn> ProductOrderIns { get; set; }
    }
}
