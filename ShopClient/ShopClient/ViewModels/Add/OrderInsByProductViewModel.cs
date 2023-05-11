using ModelsApi;
using ShopClient.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopClient.ViewModels.Add
{
    public class OrderInsByProductViewModel : BaseViewModel
    {
        public int ToggleSort = 0;
        internal void Sort(string p)
        {
            if (p == "Date")
            {
                var forSort = searchResult.Where(s => s.Order.Date != null).ToList();
                if (ToggleSort == 0) forSort.Sort((x, y) => y.Order.Date.Value.CompareTo(x.Order.Date));
                else forSort.Sort((x, y) => x.Order.Date.Value.CompareTo(y.Order.Date));
                searchResult = forSort;
            }  
            UpdateList();
            if (ToggleSort == 1) ToggleSort = 0;
            else ToggleSort = 1;
        }

        private List<ProductOrderInApi> orderInsByProduct = new List<ProductOrderInApi>();
        public List<ProductOrderInApi> OrderInsByProduct
        {
            get => orderInsByProduct;
            set
            {
                Set(ref orderInsByProduct, value);
                SignalChanged();
            }
        }

        private ProductOrderInApi selectedOrderIn;
        public ProductOrderInApi SelectedOrderIn
        {
            get => selectedOrderIn;
            set
            {
                Set(ref selectedOrderIn, value);
                SignalChanged();
            }
        }

        private bool isNotEmpty = true;
        public bool IsNotEmpty
        {
            get => isNotEmpty;
            set
            {
                if (value != isNotEmpty)
                {
                    isNotEmpty = value;
                    SignalChanged();
                    Search();
                }
            }
        }

        public CustomCommand Save { get; set; }
        public CustomCommand Cancel { get; set; }

        List<ProductOrderInApi> searchResult;

        private List<ProductOrderInApi> FullOrderByProduct = new List<ProductOrderInApi>();
        private List<ProductOrderInApi> ProductOrderIns = new List<ProductOrderInApi>();
        private List<OrderApi> FullOrders = new List<OrderApi>();

        public OrderInsByProductViewModel(ProductApi product)
        {
            GetList(product);


            //Save = new CustomCommand(() =>
            //{
            //    MessageBoxResult result = MessageBox.Show("Сохранить изменения?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);
            //    if (result == MessageBoxResult.Yes)
            //    {
            //        try
            //        {
            //            foreach (Window window in Application.Current.Windows)
            //            {
            //                if (window.DataContext == this) CloseWin(window);
            //            }
            //        }
            //        catch (Exception e)
            //        {
            //            MessageBox.Show(e.Message);
            //        };
            //    }
            //    else return;
            //});
            Cancel = new CustomCommand(() =>
            {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.DataContext == this)
                        {
                            CloseWin(window);
                        }
                    }
            });
            UpdateList();
        }

        private void Search()
        {
            searchResult = FullOrderByProduct;
            if (IsNotEmpty == true)
            {
                searchResult = FullOrderByProduct.Where(c => c.Remains > 0).ToList();
            }
            UpdateList();
        }

        private void UpdateList()
        {
            OrderInsByProduct = searchResult;
        }

        public void CloseWin(object obj)
        {
            Window win = obj as Window;
            win.Close();
        }

        private async Task GetList(ProductApi product)
        {
            ProductOrderIns = await Api.GetListAsync<List<ProductOrderInApi>>("ProductOrderIn");
            FullOrders = await Api.GetListAsync<List<OrderApi>>("Order");
            FillList(product);
            Search();
        }
        private void FillList(ProductApi product)
        {
            OrderInsByProduct = ProductOrderIns.FindAll(s => s.IdProduct == product.Id);
            foreach (var item in OrderInsByProduct)
            {
                item.Order = FullOrders.Find(s => s.Id == item.IdOrder);
            }
            FullOrderByProduct = OrderInsByProduct;
            SignalChanged("OrderInsByProduct");
        }

    }
}
