using ModelsApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//
namespace ShopServer.db
{ 
    public partial class ProductOrderOut
    {
        public static explicit operator ProductOrderOutApi(ProductOrderOut productOrderOut)
        {
            if (productOrderOut == null)
            {
                return null;
            }
            return new ProductOrderOutApi { Count = productOrderOut.Count, Discount = productOrderOut.Discount, IdOrderOut = productOrderOut.IdOrderOut, IdProductOrderIn = productOrderOut.IdProductOrderIn, Price = productOrderOut.Price };
        }

        public static explicit operator ProductOrderOut(ProductOrderOutApi productOrderOut)
        {
            if (productOrderOut == null)
            {
                return null;
            }
            return new ProductOrderOut { Count = productOrderOut.Count, Discount = productOrderOut.Discount, IdOrderOut = productOrderOut.IdOrderOut, IdProductOrderIn = productOrderOut.IdProductOrderIn, Price = productOrderOut.Price };
        }
    }
}
