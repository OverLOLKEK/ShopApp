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
    /// Логика взаимодействия для AddOrderOut.xaml
    /// </summary>
    public partial class AddOrderOut : Window
    {
        public AddOrderOut(List<ProductOrderOutApi> productOrderOuts, SaleTypeApi saleType, ObservableCollection<ProductOrderInApi> productOrderInsToUpdate, ProductApi productApi)
        {
            InitializeComponent();
            DataContext = new AddOrderOutViewModel(productOrderOuts, saleType, productOrderInsToUpdate, productApi);
        }
    }
}
