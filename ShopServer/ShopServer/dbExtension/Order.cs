using ModelsApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopServer.db
{
    public partial class Order
    {
        public static explicit operator OrderApi(Order order)
        {
            if (order == null)
            {
                return null;
            }
            return new OrderApi { Id = order.Id, Date = order.Date , IdActionType = order.IdActionType, IdClient = order.IdClient };
        }

        public static explicit operator Order(OrderApi order)
        {
            if (order == null)
            {
                return null;
            }
            return new Order { Id = order.Id, Date = order.Date, IdActionType = order.IdActionType, IdClient = order.IdClient };
        }

    }
}
