using System;
using System.Collections.Generic;

#nullable disable
//
namespace ShopServer.db
{
    public partial class Product
    {
        public Product()
        {
            ProductCostHistories = new HashSet<ProductCostHistory>();
            ProductOrderIns = new HashSet<ProductOrderIn>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public int? Article { get; set; }
        public int? Barcode { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int? IdUnit { get; set; }
        public int? IdProductType { get; set; }
        public decimal? WholesalePrice { get; set; }
        public decimal? RetailPrice { get; set; }
        public int? IdFabricator { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? MinCount { get; set; }

        public virtual Fabricator IdFabricatorNavigation { get; set; }
        public virtual ProductType IdProductTypeNavigation { get; set; }
        public virtual Unit IdUnitNavigation { get; set; }
        public virtual ICollection<ProductCostHistory> ProductCostHistories { get; set; }
        public virtual ICollection<ProductOrderIn> ProductOrderIns { get; set; }
    }
}
