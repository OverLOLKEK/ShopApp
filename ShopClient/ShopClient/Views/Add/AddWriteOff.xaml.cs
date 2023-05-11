using ModelsApi;
using ShopClient.ViewModels.Add;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ShopClient.Views.Add
{
    /// <summary>
    /// Логика взаимодействия для AddWriteOff.xaml
    /// </summary>
    public partial class AddWriteOff : Window
    {
        public AddWriteOff(ProductOrderInApi productOrderIn, ObservableCollection<ProductOrderInApi> productOrderIns, ObservableCollection<ProductOrderOutApi> productOrderOuts)
        {
            InitializeComponent();
            DataContext = new AddWriteOffViewModel(productOrderIn, productOrderIns, productOrderOuts);
        }
    }
}
