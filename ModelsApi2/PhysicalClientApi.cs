using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsApi
{
    public class PhysicalClientApi : ApiBaseType
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public int? IdClient { get; set; }

        public ClientApi Client { get; set; }
    }
}
