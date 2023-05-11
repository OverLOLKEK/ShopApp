using ModelsApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//
namespace ShopServer.db
{
    public partial class SaleType
    {
        public static explicit operator SaleTypeApi(SaleType saleType)
        {
            if (saleType == null)
            {
                return null;
            }
            return new SaleTypeApi { Id = saleType.Id, Title = saleType.Title };
        }

        public static explicit operator SaleType(SaleTypeApi saleType)
        {
            if (saleType == null)
            {
                return null;
            }
            return new SaleType { Id = saleType.Id, Title = saleType.Title };
        }
    }
}
