using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsApi
{
    public  class OrderApi : ApiBaseType 
    {
        public DateTime? Date { get; set; }
        public int? IdClient { get; set; }
        public int? IdActionType { get; set; }

        public ClientApi Client { get; set; }
        public ActionTypeApi ActionType { get; set; }

    }
}
