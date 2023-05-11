using ModelsApi;
using ShopClient.Core;
using ShopClient.Helper;
using ShopClient.Views.Add;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ShopClient.ViewModels
{
   public  class WriteOffViewModel : BaseViewModel
    {
        private List<ProductOrderInApi> productOrderIns = new List<ProductOrderInApi>();
        public List<ProductOrderInApi> ProductOrderIns
        {
            get => productOrderIns;
            set
            {
                productOrderIns = value;
                SignalChanged();
            }
        }
        private ProductOrderInApi selectedProductOrderIn;
        public ProductOrderInApi SelectedProductOrderIn
        {
            get => selectedProductOrderIn;
            set
            {
                selectedProductOrderIn = value;
                SignalChanged();
            }
        }
        private object _clickedTreeElement;
        public object ClickedTreeElement
        {
            get => _clickedTreeElement;

            set
            {
                Set(ref _clickedTreeElement, value);
                UpdateListWithTreeView();
            }
        }

        private List<ProductTypeApi> productTypeFilter;
        public List<ProductTypeApi> ProductTypeFilter
        {
            get => productTypeFilter;
            set
            {
                productTypeFilter = value;
                SignalChanged();
            }
        }

        private ProductTypeApi selectedProductTypeFilter;
        public ProductTypeApi SelectedProductTypeFilter
        {
            get => selectedProductTypeFilter;
            set
            {
                selectedProductTypeFilter = value;
                SignalChanged();
                Search();
            }
        }
        private string searchText = "";
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                Search();
            }
        }
        private ObservableCollection<OrderOutVisual> orderOutVisuals = new ObservableCollection<OrderOutVisual>();
        public ObservableCollection<OrderOutVisual> OrderOutVisuals
        {
            get => orderOutVisuals;
            set
            {
                Set(ref orderOutVisuals, value);
                SignalChanged();
            }
        }
        private OrderOutVisual selectedOrderOutVisual;
        public OrderOutVisual SelectedOrderOutVisual
        {
            get => selectedOrderOutVisual;
            set
            {
                selectedOrderOutVisual = value;
                SignalChanged();
            }
        }
        public List<string> SearchType { get; set; }
        private string selectedSearchType;
        public string SelectedSearchType
        {
            get => selectedSearchType;
            set
            {
                selectedSearchType = value;
                Search();
            }
        }
        private ObservableCollection<ProductOrderInApi> productOrderInsToUpdate = new ObservableCollection<ProductOrderInApi>();
        public ObservableCollection<ProductOrderInApi> ProductOrderInsToUpdate
        {
            get => productOrderInsToUpdate;
            set
            {
                Set(ref productOrderInsToUpdate, value);
                SignalChanged();

            }
        }
        private List<ProductTypeApi> productTypes;
        public List<ProductTypeApi> ProductTypes
        {
            get => productTypes;
            set
            {
                Set(ref productTypes, value);
                SignalChanged();

            }
        }
        private ObservableCollection<ProductTypeTreeView> productTypeTreeViews = new ObservableCollection<ProductTypeTreeView>();
        public ObservableCollection<ProductTypeTreeView> ProductTypeTreeViews
        {
            get => productTypeTreeViews;
            set
            {
                Set(ref productTypeTreeViews, value);
                SignalChanged();
            }
        }
        private DateTime selectedDate = DateTime.Now;
        public DateTime SelectedDate
        {
            get => selectedDate;
            set
            {
                selectedDate = value;
                SignalChanged();
            }
        }
        private List<ProductOrderInApi> FullProductOrderIns = new List<ProductOrderInApi>();
        public ObservableCollection<ProductOrderOutApi> ProductOrderOutsToUpdate = new ObservableCollection<ProductOrderOutApi>();
        private List<ProductOrderInApi> ProductOrderInsWithRemains = new List<ProductOrderInApi>();
        private List<ProductApi> FullProducts = new List<ProductApi>();
        private List<ActionTypeApi> ActionTypes = new List<ActionTypeApi>();
        private List<FabricatorApi> Fabricators = new List<FabricatorApi>();
        private List<UnitApi> Units = new List<UnitApi>();
        private List<OrderApi> Orders = new List<OrderApi>();
        private List<ClientApi> Clients = new List<ClientApi>();
        List<ProductOrderInApi> searchResult = new List<ProductOrderInApi>();
    
        public CustomCommand AddProductOrderIn { get; set; }
        public CustomCommand DeleteProductOrderOut { get; set; }
        public CustomCommand AddOrder { get; set; }
        public WriteOffViewModel()
        {
            ProductOrderIns = new List<ProductOrderInApi>();
            GetList();
            SearchType = new List<string>();
            SearchType.AddRange(new string[] { "Наименование", "Артикул", "Производитель", "Дата" });
            selectedSearchType = SearchType.First();

            AddProductOrderIn = new CustomCommand(() =>
            {
                if (SelectedProductOrderIn == null || SelectedProductOrderIn.Id == 0) return;
                if (ProductOrderInsToUpdate.Contains(SelectedProductOrderIn))
                {
                    MessageBox.Show("Такой товар уже присутствует в списке!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                AddWriteOff addWriteOff = new AddWriteOff(SelectedProductOrderIn, ProductOrderInsToUpdate, ProductOrderOutsToUpdate);
                addWriteOff.ShowDialog();

                GenerateOrderOutVisuals();
            });
            DeleteProductOrderOut = new CustomCommand(() =>
            {
                if (SelectedOrderOutVisual == null || SelectedOrderOutVisual.OrderInId == 0) return;
                var productOrderIn = FullProductOrderIns.First(s => s.Id == SelectedOrderOutVisual.OrderInId);
                ProductOrderInsToUpdate.Remove(productOrderIn);
                var productOrderOut = ProductOrderOutsToUpdate.First(s=>s.IdProductOrderIn == SelectedOrderOutVisual.OrderInId);
                ProductOrderOutsToUpdate.Remove(productOrderOut);
                GenerateOrderOutVisuals();
            });
            AddOrder = new CustomCommand(() =>
            {
            MessageBoxResult result = MessageBox.Show("Принять заказ?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                { 
                    if (ProductOrderOutsToUpdate.Count == 0)
                    {
                        MessageBox.Show("Заказ пуст!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    try
                    {
                        ActionTypeApi actionType = ActionTypes.First(c => c.Name == "Списание");
                        ClientApi client = Clients.First(c => c.Phone == "Списание");
                        OrderOutApi orderOut = new OrderOutApi { ProductOrderOuts = ProductOrderOutsToUpdate.ToList()};

                        AddNewOrder(new OrderApi { Date = SelectedDate, IdActionType = actionType.Id, IdClient = client.Id }, orderOut);
                        PutProductOrderIns(ProductOrderInsToUpdate);
                        MessageBox.Show("Заказ принят", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        SelectedDate = DateTime.Now;
                        SignalChanged("SelectedDate");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    };
                }
            });

            UpdateList();
        }
        private async Task GetListOrders()
        {
            Orders = await Api.GetListAsync<List<OrderApi>>("Order");
        }
        private async Task AddNewOrder(OrderApi order, OrderOutApi orderOut)
        {
            var id = await Api.PostAsync<OrderApi>(order, "Order");
            await AddNewOrderOut(orderOut);
        }
        private async Task PutProductOrderIns(ObservableCollection<ProductOrderInApi> productOrderIns)
        {
            foreach (ProductOrderInApi productOrderIn in productOrderIns)
            {
                var id = await Api.PutAsync<ProductOrderInApi>(productOrderIn, "ProductOrderIn");
            }
            ProductOrderInsToUpdate.Clear();
        }
        private async Task AddNewOrderOut(OrderOutApi orderOut)
        {
            await GetListOrders();
            var orderId = Orders.Last().Id;
            orderOut.IdOrder = orderId;
            var id = await Api.PostAsync<OrderOutApi>(orderOut, "OrderOut");
            ProductOrderOutsToUpdate.Clear();
            OrderOutVisuals.Clear();

            await GetList();
        }
        private void GenerateOrderOutVisuals()
        {
            OrderOutVisuals.Clear();
            foreach (ProductOrderOutApi productOrderOutApi in ProductOrderOutsToUpdate)
            {
                OrderOutVisuals.Add(new OrderOutVisual { Count = productOrderOutApi.Count, OrderInId = productOrderOutApi.IdProductOrderIn, Price = productOrderOutApi.Price, Product = productOrderOutApi.Product});
            }
            SignalChanged("OrderOutVisuals");
        }
        private void Search()
        {
            var search = SearchText.ToLower();
            if (SelectedProductTypeFilter == null)
                SelectedProductTypeFilter = ProductTypeFilter.Last();
            if (SelectedProductTypeFilter.Title == "Все типы")
            {
                if (SelectedSearchType == "Наименование")
                    searchResult = ProductOrderInsWithRemains
                        .Where(c => c.Product.Title.ToLower().Contains(search)).ToList();
                else if (SelectedSearchType == "Артикул")
                    searchResult = ProductOrderInsWithRemains
                        .Where(c => c.Product.Article.ToString().Contains(search)).ToList();
                else if (SelectedSearchType == "Производитель")
                    searchResult = ProductOrderInsWithRemains
                        .Where(c => c.Product.Fabricator.Title.ToLower().ToString().Contains(search)).ToList();
                else if (SelectedSearchType == "Дата")
                    searchResult = ProductOrderInsWithRemains
                        .Where(c => c.Order.Date.ToString().Contains(search)).ToList();
            }
            else
            {
                if (SelectedSearchType == "Наименование")
                    searchResult = ProductOrderInsWithRemains
                        .Where(c => c.Product.Title.ToLower().Contains(search) && c.Product.ProductType.Title.Contains(SelectedProductTypeFilter.Title)).ToList();
                else if (SelectedSearchType == "Артикул")
                    searchResult = ProductOrderInsWithRemains
                        .Where(c => c.Product.Article.ToString().Contains(search) && c.Product.ProductType.Title.Contains(SelectedProductTypeFilter.Title)).ToList();
                else if (SelectedSearchType == "Производитель")
                    searchResult = ProductOrderInsWithRemains
                        .Where(c => c.Product.Fabricator.Title.ToLower().ToString().Contains(search) && c.Product.ProductType.Title.Contains(SelectedProductTypeFilter.Title)).ToList();
                else if (SelectedSearchType == "Дата")
                    searchResult = ProductOrderInsWithRemains
                        .Where(c => c.Order.Date.ToString().Contains(search) && c.Product.ProductType.Title.Contains(SelectedProductTypeFilter.Title)).ToList();
            }
            UpdateList();
        }

        private async Task GetList()
        {
            FullProductOrderIns = await Api.GetListAsync<List<ProductOrderInApi>>("ProductOrderIn");
            Clients = await Api.GetListAsync<List<ClientApi>>("Client");
            Orders = await Api.GetListAsync<List<OrderApi>>("Order");
            FullProducts = await Api.GetListAsync<List<ProductApi>>("Product");
            Fabricators = await Api.GetListAsync<List<FabricatorApi>>("Fabricator");
            ProductTypes = await Api.GetListAsync<List<ProductTypeApi>>("ProductType");
            Units = await Api.GetListAsync<List<UnitApi>>("Unit");
            ActionTypes = await Api.GetListAsync<List<ActionTypeApi>>("ActionType");
            ProductOrderIns.Clear();
            ProductOrderInsWithRemains.Clear();
            foreach (ProductOrderInApi productOrderIn in FullProductOrderIns)
            {
                productOrderIn.Product = FullProducts.First(s => s.Id == productOrderIn.IdProduct);
                productOrderIn.Product.ProductType = ProductTypes.First(s => s.Id == productOrderIn.Product.IdProductType);
                productOrderIn.Product.Fabricator = Fabricators.First(s => s.Id == productOrderIn.Product.IdFabricator);
                productOrderIn.Order = Orders.First(s => s.Id == productOrderIn.IdOrder);
                if (productOrderIn.Remains > 0)
                {
                    ProductOrderInsWithRemains.Add(productOrderIn);
                    ProductOrderIns.Add(productOrderIn);
                }
            }
            ProductTypeFilter = await Api.GetListAsync<List<ProductTypeApi>>("ProductType");
            ProductTypeFilter.Add(new ProductTypeApi { Title = "Все типы" });
            SelectedProductTypeFilter = ProductTypeFilter.Last();
            PrepareTreeView();
            Update();
        }
        
        private void UpdateList()
        {
            ProductOrderIns = searchResult;
        }
        private void PrepareTreeView()
        {
            ProductTypeTreeViews.Clear();
            foreach (ProductTypeApi productType in ProductTypes)
            {
                var productTypeTreeView = new ProductTypeTreeView { Title = productType.Title };
                ProductTypeTreeViews.Add(productTypeTreeView);
                var ProductByProductType = FullProducts.Where(s => s.IdProductType == productType.Id);
                foreach (FabricatorApi fab in Fabricators)
                {
                    var a = 0;
                    foreach (ProductApi prod in ProductByProductType)
                    {
                        if (prod.IdFabricator == fab.Id)
                        {
                            var fabricatorTreeView = new FabricatorTreeView { Title = fab.Title };
                            productTypeTreeView.Fabricators.Add(fabricatorTreeView);
                            a = 1;
                        }
                        if (a == 1) break;
                    }
                }
            }
            SignalChanged("ProductTypeTreeViews");
        }
        private void Update()
        {
            //GetList();
            SignalChanged("Units");
            SignalChanged("ProductTypes");
            SignalChanged("Products");
            SignalChanged("ProductOrderIns");
            SignalChanged("LegalClients");
            SelectedProductTypeFilter = ProductTypeFilter.Last();
        }
        private void UpdateListWithTreeView()
        {
            if (ClickedTreeElement == null) ProductOrderIns = ProductOrderInsWithRemains;
            if (ClickedTreeElement is ProductTypeTreeView)
            {
                var clickedEl = (ProductTypeTreeView)ClickedTreeElement;
                ProductOrderIns = ProductOrderInsWithRemains.Where(s => s.Product.ProductType.Title == clickedEl.Title).ToList();
            }
            if (ClickedTreeElement is FabricatorTreeView)
            {
                var clickedEl = (FabricatorTreeView)ClickedTreeElement;
                ProductOrderIns = ProductOrderInsWithRemains.Where(s => s.Product.ProductType.Title == clickedEl.Parent && s.Product.Fabricator.Title == clickedEl.Title).ToList();
            }

        }
    }
}
