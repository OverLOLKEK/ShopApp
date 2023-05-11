using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsApi
{
   public partial class ProductCostHistoryApi : ApiBaseType
    {
        public decimal? WholesalePirceValue { get; set; }
        public decimal? RetailPriceValue { get; set; }
        public DateTime? ChangeDate { get; set; }
        public int? IdProduct { get; set; }

    }
}
