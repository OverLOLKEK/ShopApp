using System;
using System.Collections.Generic;

#nullable disable
//
namespace ShopServer.db
{
    public partial class Fabricator
    {
        public Fabricator()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
