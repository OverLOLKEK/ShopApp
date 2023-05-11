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

namespace ShopClient.Helper
{
    /// <summary>
    /// Логика взаимодействия для OrderInReport.xaml
    /// </summary>
    public partial class OrderInReport : UserControl
    {
        public OrderInReport()
        {
            InitializeComponent();
            DataContext = new ReportOrderInViewModel();
        }
    }
}
