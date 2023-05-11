using ModelsApi;
using ShopClient.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ShopClient.ViewModels
{
    public class OrderOutDetailsViewModels : BaseViewModel
    {
        private List<ProductOrderOutApi> productOrderOuts = new List<ProductOrderOutApi>();
        public List<ProductOrderOutApi> ProductOrderOuts
        {
            get => productOrderOuts;
            set
            {
                Set(ref productOrderOuts, value);
                SignalChanged();
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
        private List<string> orderStatuses;
        public List<string> OrderStatuses
        {
            get => orderStatuses;
            set
            {
                orderStatuses = value;
                SignalChanged();
            }
        }
        private string selectedOrderStatus;
        public string SelectedOrderStatus
        {
            get => selectedOrderStatus;
            set
            {
                selectedOrderStatus = value;
                SignalChanged();
            }
        }

        private string orderStatus;
        public string OrderStatus
        {
            get => orderStatus;
            set
            {
                orderStatus = value;
                SignalChanged();
            }
        }

        private string saleTypeName;
        public string SaleTypeName
        {
            get => saleTypeName;
            set
            {
                saleTypeName = value;
                SignalChanged();
            }
        }

        private string client;
        public string Client
        {
            get => client;
            set
            {
                client = value;
                SignalChanged();
            }
        }
        private bool statusEnable = true;
        public bool StatusEnable
        {
            get => statusEnable;
            set
            {
                statusEnable = value;
                SignalChanged();
            }
        }

        private List<ProductOrderInApi> productOrderInsToUpdate = new List<ProductOrderInApi>();
        public List<ProductOrderInApi> ProductOrderInsToUpdate
        {
            get => productOrderInsToUpdate;
            set
            {
                Set(ref productOrderInsToUpdate, value);
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

        public List<OrderOutApi> FullOrderOuts = new List<OrderOutApi>();
        public List<ProductOrderInApi> FullProductOrderIns = new List<ProductOrderInApi>();
        public List<ProductApi> Products = new List<ProductApi>();
        public List<ClientApi> Clients = new List<ClientApi>();
        public List<PhysicalClientApi> PhysicalClients = new List<PhysicalClientApi>();
        public List<LegalClientApi> LegalClients = new List<LegalClientApi>();
        public List<SaleTypeApi> SaleTypes = new List<SaleTypeApi>();
        private string oldStatus;
        public OrderOutApi orderOutApi = new OrderOutApi();
       

        public CustomCommand Cancel { get; set; }
        public CustomCommand Save { get; set; }

        public OrderOutDetailsViewModels(OrderApi order)
        {

            OrderStatuses = new List<string>();
            OrderStatuses.AddRange(new string[] { "Подтвержден", "Отменен" });

            GetList(order);

            Save = new CustomCommand(() =>
            {
                MessageBoxResult result = MessageBox.Show("Сохранить изменения?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        if (SelectedOrderStatus == "Отменен" && oldStatus != "Отменен")
                        {
                            var OrderIns = GenerateOrderInsToUpdate();
                            PutProductOrderIns(OrderIns);
                            orderOutApi.Status = "Отменен";
                            PutOrderOut(orderOutApi);
                        }
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
        private async Task PutProductOrderIns(List<ProductOrderInApi> productOrderIns)
        {
            foreach (ProductOrderInApi productOrderIn in productOrderIns)
            {
                var id = await Api.PutAsync<ProductOrderInApi>(productOrderIn, "ProductOrderIn");
            }
        }
        private async Task PutOrderOut(OrderOutApi orderOut)
        {
            var id = await Api.PutAsync<OrderOutApi>(orderOut, "OrderOut");
        }
        private async Task PutOrder(OrderApi order)
        {
            var id = await Api.PutAsync<OrderApi>(order, "Order");
        }
        public void CloseWin(object obj)
        {
            Window win = obj as Window;
            win.Close();
        }
        private async Task GetList(OrderApi order)
        {
            SaleTypes = await Api.GetListAsync<List<SaleTypeApi>>("SaleType");
            FullOrderOuts = await Api.GetListAsync<List<OrderOutApi>>("OrderOut");
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
            var orderOut = FullOrderOuts.First(s => s.IdOrder == order.Id);
            List<ProductOrderOutApi> list = new List<ProductOrderOutApi>();
            ProductOrderOuts = new List<ProductOrderOutApi>();
            foreach (var item in orderOut.ProductOrderOuts)
            {
                list.Add(item);
            }
            //ProductOrderOuts.AddRange(orderOut.ProductOrderOuts);
            foreach (var item in list)
            {
                item.Product = FullProductOrderIns.First(s => s.Id == item.IdProductOrderIn).Product;
                //item.Product = Products.First(s => s.Id == productOrderIn.Id);
            }
            ProductOrderOuts = list;
            SignalChanged("ProductOrderOuts");

            OrderStatus = orderOut.Status;
            var saleType = SaleTypes.First(s => s.Id == orderOut.IdSaleType);
            SaleTypeName = saleType.Title;
            oldStatus = orderOut.Status;
            orderOutApi = orderOut;
            SelectedOrderStatus = OrderStatuses.First(s => s.Contains(orderOut.Status));
            if (SelectedOrderStatus == "Отменен")
            {
                StatusEnable = false;
            }
            var legalcli = LegalClients.Find(s => s.IdClient == order.IdClient);
            if (legalcli != null)
            {
                Client = $"{legalcli.Title} (ИНН:{legalcli.Inn})";
            }
            else
            {
                var phyClient = PhysicalClients.Find(s => s.IdClient == order.IdClient);
                if (phyClient != null)
                    Client = $"{phyClient.LastName} " + $"{phyClient.FirstName} " + $"{phyClient.Patronymic}";
            }

            decimal total = 0;
            foreach (ProductOrderOutApi productOrderOut in ProductOrderOuts)
            {
                total += (decimal)(productOrderOut.Count * (productOrderOut.Price - productOrderOut.Discount));
            }
            Total = total.ToString();
            SignalChanged("Total");

            SignalChanged("SelectedOrderStatus");
            SignalChanged("SaleTypeName");
            SignalChanged("OrderStatus");
            SignalChanged("Client");
            SignalChanged("OrderDate");
            SignalChanged("ProductOrderIns");
        }
        private List<ProductOrderInApi> GenerateOrderInsToUpdate()
        {
            foreach(ProductOrderOutApi productOrderOutApi in ProductOrderOuts)
            {
                var productOrderIn = FullProductOrderIns.First(s => s.Id == productOrderOutApi.IdProductOrderIn);
                productOrderIn.Remains += productOrderOutApi.Count;
                ProductOrderInsToUpdate.Add(productOrderIn);
            }
            return ProductOrderInsToUpdate;
        }
    }
}
