using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsApi
{
   public partial class ProductApi : ApiBaseType
    {
        public string Title { get; set; }
        public int? Article { get; set; }
        public int? Barcode { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal? WholesalePrice { get; set; }
        public decimal? RetailPrice { get; set; }
        public int? Count { get; set; }
        public int? IdUnit { get; set; }
        public int? IdProductType { get; set; }
        public int? IdFabricator { get; set; }
        public int? MinCount { get; set; }
        public DateTime? deleted_at { get; set; }

        public string ColorForXaml { get; set; } = "#FFFFFF";
        public UnitApi Unit { get; set; }
        public ProductTypeApi ProductType { get; set; }
        public FabricatorApi Fabricator { get; set; }
    }
}
