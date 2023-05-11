using ModelsApi;
using ShopClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для OrderView.xaml
    /// </summary>
    public partial class OrderView : Page
    {
        public List<OrderApi> Orders = new List<OrderApi>();
        public List<OrderOutApi> OrdersOut = new List<OrderOutApi>();
        public OrderView()
        {
            InitializeComponent(); 
            GetList();
            DataContext = new OrderViewModel();

            //for (int i = 0; i < ListView1.Items.Count; i++)
            //{
            //    var item = ListView1.ItemContainerGenerator.ContainerFromIndex(i) as ListViewItem;
            //    if (i % 2 == 0)
            //        item.Background = Brushes.Tomato;
            //    else
            //        item.Background = Brushes.AntiqueWhite;
            //}

            ListView1.ItemContainerGenerator.StatusChanged += ContainerStatusChanged;
        }

        private void ContainerStatusChanged(object sender, EventArgs e)
        {
            if (ListView1.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            {
                int i = 0;
                foreach (OrderApi o in ListView1.Items)
                {
                    var orderOut = OrdersOut.Find(s => s.IdOrder == o.Id);
                    if (orderOut != null)
                    {
                        if (orderOut.Status == "Отменен")
                        {
                            var lvitem = ListView1.ItemContainerGenerator.ContainerFromItem(o) as ListViewItem;
                            if (lvitem != null)
                            {
                               lvitem.Background = Brushes.LightGray;
                               lvitem.Opacity = 0.7;
                               lvitem.Foreground = Brushes.Black;
                            }
                        }
                    }
                    i++;
                }       
            }
        }
        private void ClickColumn(object sender, MouseButtonEventArgs e)
        {
            string p = ((Control)sender).Tag as string;
            ((OrderViewModel)DataContext).Sort(p);
        }
        private async Task GetList()
        {
            Orders = await Api.GetListAsync<List<OrderApi>>("Order");
            OrdersOut = await Api.GetListAsync<List<OrderOutApi>>("OrderOut");
        }

    }
}
