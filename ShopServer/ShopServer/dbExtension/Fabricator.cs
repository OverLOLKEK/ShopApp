using ModelsApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopServer.db
{ 
    public partial class Fabricator
    {
        public static explicit operator FabricatorApi(Fabricator fabricator)
        {
            if (fabricator == null)
            {
                return null;
            }
            return new FabricatorApi { Id = fabricator.Id, Title = fabricator.Title };
        }

        public static explicit operator Fabricator(FabricatorApi fabricator)
        {
            if (fabricator == null)
            {
                return null;
            }
            return new Fabricator { Id = fabricator.Id, Title = fabricator.Title };
        }
    }
}
