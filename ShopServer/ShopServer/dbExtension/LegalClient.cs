using ModelsApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopServer.db
{    
    public partial class LegalClient
    {
        public static explicit operator LegalClientApi(LegalClient legalClient)
        {
            if (legalClient == null)
            {
                return null;
            }
            return new LegalClientApi { Id = legalClient.Id, Inn = legalClient.Inn, Email = legalClient.Email, Title = legalClient.Title, IdClient = legalClient.IdClient, IsSupplier = legalClient.IsSupplier };
        }

        public static explicit operator LegalClient(LegalClientApi legalClient)
        {
            if (legalClient == null)
            {
                return null;
            }
            return new LegalClient { Id = legalClient.Id, Inn = legalClient.Inn, Email = legalClient.Email, Title = legalClient.Title, IdClient = legalClient.IdClient, IsSupplier = legalClient.IsSupplier };
        }
    }
}
//