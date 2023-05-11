using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsApi
{
    public class LegalClientApi : ApiBaseType
    {
        public string Inn { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public int? IdClient { get; set; }
        public int? IsSupplier { get; set; }

        public ClientApi Client { get; set; }
    }
}
