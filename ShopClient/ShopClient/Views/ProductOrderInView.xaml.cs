using ShopClient.Helper;
using ShopClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShopClient.Views
{
    /// <summary>
    /// Логика взаимодействия для ProductOrderInView.xaml
    /// </summary>
    public partial class ProductOrderInView : Page
    {
        public ProductOrderInView()
        {
            InitializeComponent();
            DataContext = new ProductOrderInViewModel();
        }

        //private void Fabricator_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{

        //    FindParentTreeViewItem(sender);
        //        if (sender is TextBlock tb && tb.DataContext is FabricatorTreeView te)
        //    {

        //        te.Parent = st;
        //        trvName.Tag = te;
        //    }
        //}
        //private void ProductType_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{

        //    if (sender is TextBlock tb && tb.DataContext is ProductTypeTreeView te)
        //    {
        //        st = te.Title;
        //    }
        //}

        public ItemsControl GetSelectedTreeViewItemParent(TreeViewItem item)
            {
            DependencyObject parent = VisualTreeHelper.GetParent(item);
            while (!(parent is TreeViewItem || parent is TreeView))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as ItemsControl;
        }
        
       
        private void TreeViewItem_OnItemSelected(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.OriginalSource as TreeViewItem;
            if (item == null) return;
            var data = item.DataContext;
            if (data is ProductTypeTreeView)
            {
               var prod = (ProductTypeTreeView)data;
                trvName.Tag = prod;
            }
           else if (data is FabricatorTreeView)
            {
                var child = (FabricatorTreeView)data;
                ItemsControl parentItem = GetSelectedTreeViewItemParent(item);
                var parentData = parentItem.DataContext;
                var parent = (ProductTypeTreeView)parentData;
                child.Parent = parent.Title;
                trvName.Tag = child;
            }
            else
            trvName.Tag = null;
        }

      
    }
}
