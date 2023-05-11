using ModelsApi;
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

namespace ShopClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainVM();
            //Test();
           
        }

 

        private void tabItem_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            mainRadio.IsChecked = true;
        }

        private void tabItemLists_MouseDown(object sender, MouseButtonEventArgs e)
        {
            listRadio.IsChecked = true;
        }
        //async Task Test()
        //{
        //    var result = await Api.GetAsync<UnitApi>(1, "Unit");
        //    var units = await Api.GetListAsync<List<UnitApi>>("Unit");
        //    //var id = await Api.PostAsync(new UnitApi { Title = "шт2"}, "Unit");
        //    List<UnitApi> unitApis = new List<UnitApi>();
        //    unitApis = (List<UnitApi>)units;

        //}
    }
}
