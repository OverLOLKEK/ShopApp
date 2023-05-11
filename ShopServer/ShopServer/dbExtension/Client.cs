using ModelsApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopServer.db
{
    public partial class Client
    {
        public static explicit operator ClientApi(Client client)
        {
            if (client == null)
            {
                return null;
            }
            return new ClientApi { Id = client.Id, Address = client.Address, Phone = client.Phone };
        }

        public static explicit operator Client(ClientApi client)
        {
            if (client == null)
            {
                return null;
            }
            return new Client { Id = client.Id, Address = client.Address, Phone = client.Phone };
        }
    }
}
