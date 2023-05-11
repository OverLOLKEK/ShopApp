using ModelsApi;
using ShopClient.ViewModels;
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
    /// Логика взаимодействия для AddUnit.xaml
    /// </summary>
    public partial class AddUnit : Window
    {
        public AddUnit()
        {
            InitializeComponent();
            DataContext = new AddUnitViewModel(null);
        }
        public AddUnit(UnitApi unit)
        {
            InitializeComponent();
            DataContext = new AddUnitViewModel(unit);
        }
    }
}
