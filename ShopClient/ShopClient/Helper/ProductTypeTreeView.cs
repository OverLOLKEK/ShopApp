using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopClient.Helper
{
    public class ProductTypeTreeView
    {
        public string Title { get; set; }
        public ObservableCollection<FabricatorTreeView> Fabricators { get; set; }
        public ProductTypeTreeView()
        {
            Fabricators = new ObservableCollection<FabricatorTreeView>();
        }
    }
    public class FabricatorTreeView
    { 
        public string Title { get; set; }
        public string Parent { get; set; }
    }
}
