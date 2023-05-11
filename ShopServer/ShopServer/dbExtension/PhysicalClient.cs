using ModelsApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopServer.db
{
    public partial class PhysicalClient
    {
        public static explicit operator PhysicalClientApi(PhysicalClient physicalClient)
        {
            if (physicalClient == null)
            {
                return null;
            }
            return new PhysicalClientApi { Id = physicalClient.Id, FirstName = physicalClient.FirstName, LastName = physicalClient.LastName, Patronymic = physicalClient.Patronymic, IdClient = physicalClient.IdClient };
        }

        public static explicit operator PhysicalClient(PhysicalClientApi physicalClient)
        {
            if (physicalClient == null)
            {
                return null;
            }
            return new PhysicalClient { Id = physicalClient.Id, FirstName = physicalClient.FirstName, LastName = physicalClient.LastName, Patronymic = physicalClient.Patronymic, IdClient = physicalClient.IdClient };
        }
    }
}
