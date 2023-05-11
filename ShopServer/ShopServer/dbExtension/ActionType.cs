using ModelsApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopServer.db
{
    public partial class ActionType
    {

        public static explicit operator ActionTypeApi(ActionType actionType)
        {
            if (actionType == null)
            {
                return null;
            }
            return new ActionTypeApi { Id = actionType.Id, Name = actionType.Name };
        }

        public static explicit operator ActionType(ActionTypeApi actionType)
        {
            if (actionType == null)
            {
                return null;
            }
            return new ActionType { Id = actionType.Id, Name = actionType.Name };
        }
    }
}
