using ModelsApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopServer.db
{
    public partial class OrderOut
    {
        public static explicit operator OrderOutApi(OrderOut orderOut)
        {
            if (orderOut == null)
            {
                return null;
            }
            return new OrderOutApi { Id = orderOut.Id, IdOrder = orderOut.IdOrder, IdSaleType = orderOut.IdSaleType, Status = orderOut.Status};
        }

        public static explicit operator OrderOut(OrderOutApi orderOut)
        {
            if (orderOut == null)
            {
                return null;
            }
            return new OrderOut { Id = orderOut.Id, IdOrder = orderOut.IdOrder, IdSaleType = orderOut.IdSaleType, Status = orderOut.Status };
        }
    }
}
