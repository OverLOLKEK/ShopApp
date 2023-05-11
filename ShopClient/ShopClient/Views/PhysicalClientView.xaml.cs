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
    /// Логика взаимодействия для PhysicalClientView.xaml
    /// </summary>
    public partial class PhysicalClientView : Page
    {
        public PhysicalClientView()
        {
            InitializeComponent();
            DataContext = new PhysicalClientViewModel();
        }

        private void ClickColumn(object sender, MouseButtonEventArgs e)
        {
            string p = ((Control)sender).Tag as string;
            ((PhysicalClientViewModel)DataContext).Sort(p);
        }
    }
}
