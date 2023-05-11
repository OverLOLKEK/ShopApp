using ModelsApi;
using ShopClient.ViewModels.Add;
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

namespace ShopClient.Views.Add
{
    /// <summary>
    /// Логика взаимодействия для AddFabricator.xaml
    /// </summary>
    public partial class AddFabricator : Window
    {
        public AddFabricator()
        {
            InitializeComponent();
            DataContext = new AddFabricatorViewModel(null);
        }
        public AddFabricator(FabricatorApi fabricator)
        {
            InitializeComponent();
            DataContext = new AddFabricatorViewModel(fabricator);
        }
    }
}
