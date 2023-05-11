using System;
using System.Collections.Generic;

#nullable disable
//
namespace ShopServer.db
{
    public partial class LegalClient
    {
        public int Id { get; set; }
        public string Inn { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public int? IdClient { get; set; }
        public int? IsSupplier { get; set; }

        public virtual Client IdClientNavigation { get; set; }
    }
}
