using ModelsApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//
namespace ShopServer.db
{
    public partial class ProductOrderIn
    {
        public static explicit operator ProductOrderInApi(ProductOrderIn productOrderIn)
        {
            if (productOrderIn == null)
            {
                return null;
            }
            return new ProductOrderInApi { Id = productOrderIn.Id, Count = productOrderIn.Count, Price = productOrderIn.Price, IdOrder=productOrderIn.IdOrder, IdProduct = productOrderIn.IdProduct, Remains=productOrderIn.Remains };
        }

        public static explicit operator ProductOrderIn(ProductOrderInApi productOrderIn)
        {
            if (productOrderIn == null)
            {
                return null;
            }
            return new ProductOrderIn { Id = productOrderIn.Id, Count = productOrderIn.Count, Price = productOrderIn.Price, IdOrder = productOrderIn.IdOrder, IdProduct = productOrderIn.IdProduct, Remains = productOrderIn.Remains };
        }

    }
}
