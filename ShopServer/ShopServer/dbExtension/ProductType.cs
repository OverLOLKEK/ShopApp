using ModelsApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//
namespace ShopServer.db
{
    public partial class ProductType
    {
        public static explicit operator ProductTypeApi(ProductType productType)
        {
            if (productType == null)
            {
                return null;
            }
            return new ProductTypeApi { Id = productType.Id, Title = productType.Title };
        }

        public static explicit operator ProductType(ProductTypeApi productType)
        {
            if (productType == null)
            {
                return null;
            }
            return new ProductType { Id = productType.Id, Title = productType.Title };
        }
    }
}
