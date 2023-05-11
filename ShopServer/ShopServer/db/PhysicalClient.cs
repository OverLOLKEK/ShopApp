using System;
using System.Collections.Generic;

#nullable disable
//
namespace ShopServer.db
{
    public partial class PhysicalClient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public int? IdClient { get; set; }

        public virtual Client IdClientNavigation { get; set; }
    }
}
