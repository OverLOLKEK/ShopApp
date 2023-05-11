using ModelsApi;
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
using System.Windows.Shapes;

namespace ShopClient.Views
{
    /// <summary>
    /// Логика взаимодействия для OrderInDetails.xaml
    /// </summary>
    public partial class OrderInDetails : Window
    {
        public OrderInDetails(OrderApi order)
        {
            InitializeComponent();
            DataContext = new OrderInDetailsViewModel(order);
        }
    }
}
