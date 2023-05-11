using ModelsApi;
using ShopClient.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopClient.ViewModels
{
    public class OrderInDetailsViewModel : BaseViewModel
    {
      
        private ObservableCollection<ProductOrderInApi> productOrderIns;
        public ObservableCollection<ProductOrderInApi> ProductOrderIns
        {
            get => productOrderIns;
            set
            {
                productOrderIns = value;
                SignalChanged();
                SignalChanged("ProductOrderIns");
            }
        }

        private DateTime orderDate;
        public DateTime OrderDate
        { 
            get => orderDate;
            set
            {
                orderDate = value;
                SignalChanged();
            }
        }
        private string total;
        public string Total
        {
            get => total;
            set
            {
                total = value;
                SignalChanged();
            }
        }
        private string supplier;
        public string Supplier 
        { 
            get => supplier;
            set
            {
                supplier = value;
                SignalChanged();
            }
        }

        public List<ProductOrderInApi> FullProductOrderIns = new List<ProductOrderInApi>();
        public List<ProductApi> Products = new List<ProductApi>();
        public List<ClientApi> Clients = new List<ClientApi>();
        public List<PhysicalClientApi> PhysicalClients = new List<PhysicalClientApi>();
        public List<LegalClientApi> LegalClients = new List<LegalClientApi>();

       public CustomCommand Cancel { get; set; }
        public CustomCommand Save { get; set; }


        public OrderInDetailsViewModel(OrderApi order)
        {
            GetList(order);
            Save = new CustomCommand(() =>
            {
                MessageBoxResult result = MessageBox.Show("Сохранить изменения?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                       
                            order.Date = OrderDate;
                            PutOrder(order);

                        foreach (Window window in Application.Current.Windows)
                        {
                            if (window.DataContext == this) CloseWin(window);
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    };
                }
                else return;
            });
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
        }
        public void CloseWin(object obj)
        {
            Window win = obj as Window;
            win.Close();
        }
        private async Task PutOrder(OrderApi order)
        {
            var id = await Api.PutAsync<OrderApi>(order, "Order");
        }
        private async Task GetList(OrderApi order)
        {
            FullProductOrderIns = await Api.GetListAsync<List<ProductOrderInApi>>("ProductOrderIn");
            Products = await Api.GetListAsync<List<ProductApi>>("Product");
            Clients = await Api.GetListAsync<List<ClientApi>>("Client");
            PhysicalClients = await Api.GetListAsync<List<PhysicalClientApi>>("PhysicalClient");
            LegalClients = await Api.GetListAsync<List<LegalClientApi>>("LegalClient");
            OrderDate = (DateTime)order.Date;
            GenerateProductOrderIns(order);
        }
        public void GenerateProductOrderIns(OrderApi order)
        {
            ProductOrderIns = new ObservableCollection<ProductOrderInApi>();
            var thisOrdersIns = FullProductOrderIns.Where(s => s.IdOrder == order.Id).ToList();
            foreach (var item in thisOrdersIns)
            {
                ProductOrderIns.Add(item);
            }

            decimal total = 0;
            foreach (ProductOrderInApi productOrderInApi in ProductOrderIns)
            {
                total += (decimal)(productOrderInApi.Count * productOrderInApi.Price);
            }
            Total = total.ToString();
            SignalChanged("Total");
            //ProductOrderIns.AddRange(FullProductOrderIns.Where(s=>s.IdOrder == order.Id).ToList());
            //foreach (var item in ProductOrderIns)
            //{
            //    item.Product = Products.First(s => s.Id == item.IdProduct);
            //}
            var legalcli = LegalClients.Find(s => s.IdClient == order.IdClient);
            Supplier = legalcli.Title;
            SignalChanged("Supplier");
            SignalChanged("OrderDate");
            SignalChanged("ProductOrderIns");
        }
    }
}
