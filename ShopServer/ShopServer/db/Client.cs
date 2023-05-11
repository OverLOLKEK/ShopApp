using System;
using System.Collections.Generic;

#nullable disable
//
namespace ShopServer.db
{
    public partial class Client
    {
        public Client()
        {
            LegalClients = new HashSet<LegalClient>();
            Orders = new HashSet<Order>();
            PhysicalClients = new HashSet<PhysicalClient>();
        }

        public int Id { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public virtual ICollection<LegalClient> LegalClients { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<PhysicalClient> PhysicalClients { get; set; }
    }
}
