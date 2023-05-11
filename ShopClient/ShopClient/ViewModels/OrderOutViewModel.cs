using ModelsApi;
using ShopClient.Core;
using ShopClient.Helper;
using ShopClient.Views.Add;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopClient.ViewModels
{
    public class OrderOutViewModel : BaseViewModel
    { 
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

        private List<ProductApi> products;
        public List<ProductApi> Products
        {
            get => products;
            set
            {
                Set(ref products, value);
                SignalChanged();
            }
        }
        private List<UnitApi> units;
        public List<UnitApi> Units
        {
            get => units;
            set
            {
                Set(ref units, value);
                SignalChanged();

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
        private ProductApi selectedProduct = new ProductApi { };
        public ProductApi SelectedProduct
        {
            get => selectedProduct;
            set
            {
                selectedProduct = value;
                SignalChanged();
            }
        }

        public List<SaleTypeApi> saleTypes;
        public List<SaleTypeApi> SaleTypes {
            get => saleTypes;
            set
            {
                Set(ref saleTypes, value);
                SignalChanged();
            }
        }

        private ObservableCollection<ProductOrderOutApi> productOrderOuts;
        public ObservableCollection<ProductOrderOutApi> ProductOrderOuts
        {
            get => productOrderOuts;
            set
            {
                Set(ref productOrderOuts, value);
                SignalChanged();
            }
        }
        private ProductOrderOutApi selectedProductOrderOuts;
        public ProductOrderOutApi SelectedProductOrderOuts
        {
            get => selectedProductOrderOuts;
            set
            {
                selectedProductOrderOuts = value;
                SignalChanged();
            }
        }
        private bool isEnableSaleType = true;
        public bool IsEnableSaleType
        {  get => isEnableSaleType;
           set
           {
                isEnableSaleType = value;
                SignalChanged();
            }
    }
    private SaleTypeApi selectedSaleType;
        public SaleTypeApi SelectedSaleType
        {
            get => selectedSaleType;
            set
            {
                selectedSaleType = value;
                SignalChanged();
             
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
        private string clientName = "";
        public string ClientName
        {
            get => clientName;
            set
            {
                clientName = value;
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
        private List<ProductApi> FullProducts = new List<ProductApi>();
        private List<ActionTypeApi> ActionTypes = new List<ActionTypeApi>();
        private List<FabricatorApi> Fabricators = new List<FabricatorApi>();
        public List<ProductOrderOutApi> ProductOrderOutsToUpdate = new List<ProductOrderOutApi>();
        public ClientApi SelectedClient = new ClientApi();
        public List<OrderApi> Orders = new List<OrderApi>();
        List<ProductApi> searchResult;

        public CustomCommand AddProduct { get; set; }
        public CustomCommand AddOrder { get; set; }
        public CustomCommand DeleteProductOrderOut { get; set; }
        public CustomCommand ClientSelect { get; set; }

        public OrderOutViewModel()
        {
            Units = new List<UnitApi>();
            ProductTypes = new List<ProductTypeApi>();
            Products = new List<ProductApi>();
            ProductTypeFilter = new List<ProductTypeApi>();
            GetList();

            SearchType = new List<string>();
            SearchType.AddRange(new string[] { "Наименование", "Артикул", "Производитель" });
            selectedSearchType = SearchType.First();

            AddProduct = new CustomCommand(() =>
            {
                if (SelectedSaleType == null)
                {
                    MessageBox.Show("Выберите тип продажи!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (SelectedProduct == null || SelectedProduct.Id == 0) return;
                if (SelectedProduct.Count <= 0)
                {
                    MessageBox.Show("Продукт отсутствует на складе!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                foreach (var item in ProductOrderOutsToUpdate)
                {
                    if (item.Product == SelectedProduct)
                    {
                        MessageBox.Show("Такой продукт уже есть в заказе!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                var checkUpdate = ProductOrderOutsToUpdate.Count;
                AddOrderOut addOrderOut = new AddOrderOut(ProductOrderOutsToUpdate, SelectedSaleType, ProductOrderInsToUpdate, SelectedProduct);
                addOrderOut.ShowDialog();
                if (checkUpdate == ProductOrderOutsToUpdate.Count) return;
                OrderOutVisuals.Clear();
                foreach ( var ProductOrderOut in ProductOrderOutsToUpdate)
                {
                    var toggle = 0;
                    var sum = ProductOrderOut.Count * (ProductOrderOut.Price - ProductOrderOut.Discount);
                    //заменить article на id
                    if(SelectedProduct.Id == ProductOrderOut.Product.Id)
                    {
                        SelectedProduct.Count -= ProductOrderOut.Count;
                    }
                    foreach (var OrderOutVisual in OrderOutVisuals)
                    {
                        if (OrderOutVisual.Product.Id == ProductOrderOut.Product.Id)
                        {
                            OrderOutVisual.Count += ProductOrderOut.Count;
                            OrderOutVisual.Sum += sum;
                            toggle = 1;
                            break;
                        }  
                    }
                    if (toggle == 0)
                    OrderOutVisuals.Add(new OrderOutVisual { Count = ProductOrderOut.Count, Price = ProductOrderOut.Price, Product = ProductOrderOut.Product, Sum = sum, Discount = ProductOrderOut.Discount});
                }
               ChangeCountProduct();
               IsEnableSaleType = false;
                //if (productOrderIn.Count <= 0 || productOrderIn.Count == null)
                //{
                //    Update();
                //    return;
                //}
                //GetProperties(productOrderIn);
                //ProductOrderIns.Add(productOrderIn);
                Update();
            });

            DeleteProductOrderOut = new CustomCommand(() =>
            {
                if (SelectedOrderOutVisual == null || SelectedOrderOutVisual.OrderInId == 0) return;
                var product = SelectedOrderOutVisual.Product;
                ObservableCollection<ProductOrderOutApi> productOrderOutApis = new ObservableCollection<ProductOrderOutApi>(ProductOrderOutsToUpdate);
                foreach (ProductOrderOutApi productOrderOutApi in productOrderOutApis)
                {
                    if (productOrderOutApi.Product.Id == product.Id)
                    {
                        ProductOrderOutsToUpdate.Remove(productOrderOutApi);
                    }
                }
                List<ProductOrderInApi> productOrderInApis = new List<ProductOrderInApi>(ProductOrderInsToUpdate);
                foreach (ProductOrderInApi productOrderInApi in productOrderInApis)
                {
                    if (productOrderInApi.IdProduct == product.Id)
                    {
                        ProductOrderInsToUpdate.Remove(productOrderInApi);
                    }
                }
                OrderOutVisuals.Remove(SelectedOrderOutVisual);
                ChangeCountProduct();
                if (OrderOutVisuals.Count == 0)
                {
                    IsEnableSaleType = true;
                }
                Update();
            });
            ClientSelect = new CustomCommand(() =>
            {
                ClientSelect clientSelect = new ClientSelect(this);
                clientSelect.ShowDialog();
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
                    if (ClientName == "")
                    {
                        MessageBox.Show("Клиент не выбран!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    try
                    {
                        ActionTypeApi actionType = ActionTypes.First(c => c.Name == "Продажа");
                        ClientApi client = SelectedClient;
                        OrderOutApi orderOut = new OrderOutApi { IdSaleType = SelectedSaleType.Id, ProductOrderOuts = ProductOrderOutsToUpdate, Status = "Подтвержден"};

                        AddNewOrder(new OrderApi { Date = SelectedDate, IdActionType = actionType.Id, IdClient = client.Id }, orderOut);
                        PutProductOrderIns(ProductOrderInsToUpdate);
                        MessageBox.Show("Заказ принят", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        SelectedDate = DateTime.Now;
                        IsEnableSaleType = true;
                        SignalChanged("SelectedDate");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    };
                    Update();
                }
            });
             UpdateList();
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
        private async Task GetListOrders()
        {
            Orders = await Api.GetListAsync<List<OrderApi>>("Order");
        }
        private async Task PutProductOrderIns(ObservableCollection<ProductOrderInApi> productOrderIns)
        {
            foreach(ProductOrderInApi productOrderIn in productOrderIns)
            {
                var id = await Api.PutAsync<ProductOrderInApi>(productOrderIn,"ProductOrderIn");
            }
            ProductOrderInsToUpdate.Clear();
        }
        private async Task AddNewOrder(OrderApi order, OrderOutApi orderOut)
        {
            var id = await Api.PostAsync<OrderApi>(order, "Order");
            await AddNewOrderOut(orderOut);
        }
        private void Search()
        {
            var search = SearchText.ToLower();
            if (SelectedProductTypeFilter == null)
                SelectedProductTypeFilter = ProductTypeFilter.Last();
            if (SelectedProductTypeFilter.Title == "Все типы")
            {
                if (SelectedSearchType == "Наименование")
                    searchResult = FullProducts
                        .Where(c => c.Title.ToLower().Contains(search)).ToList();
                else if (SelectedSearchType == "Артикул")
                    searchResult = FullProducts
                        .Where(c => c.Article.ToString().Contains(search)).ToList();
                else if (SelectedSearchType == "Производитель")
                    searchResult = FullProducts
                        .Where(c => c.Fabricator.Title.ToLower().ToString().Contains(search)).ToList();
            }
            else
            {
                if (SelectedSearchType == "Наименование")
                    searchResult = FullProducts
                        .Where(c => c.Title.ToLower().Contains(search) && c.ProductType.Title.Contains(SelectedProductTypeFilter.Title)).ToList();
                else if (SelectedSearchType == "Артикул")
                    searchResult = FullProducts
                        .Where(c => c.Article.ToString().Contains(search) && c.ProductType.Title.Contains(SelectedProductTypeFilter.Title)).ToList();
                else if (SelectedSearchType == "Производитель")
                    searchResult = FullProducts
                        .Where(c => c.Fabricator.Title.ToLower().ToString().Contains(search) && c.ProductType.Title.Contains(SelectedProductTypeFilter.Title)).ToList();
            }
            UpdateList();
        }

        private async Task GetList()
        {
            ActionTypes = await Api.GetListAsync<List<ActionTypeApi>>("ActionType");
            SaleTypes = await Api.GetListAsync<List<SaleTypeApi>>("SaleType");
            Units = await Api.GetListAsync<List<UnitApi>>("Unit");
            Fabricators = await Api.GetListAsync<List<FabricatorApi>>("Fabricator");
            ProductTypes = await Api.GetListAsync<List<ProductTypeApi>>("ProductType");
            Products = await Api.GetListAsync<List<ProductApi>>("Product");
            FullProductOrderIns = await Api.GetListAsync<List<ProductOrderInApi>>("ProductOrderIn");
            FullProducts = Products;
            foreach (ProductApi product in Products)
            {
                product.Count = FullProductOrderIns.Where(s => s.IdProduct == product.Id).Select(s => s.Remains).Sum();
                product.Unit = Units.First(s => s.Id == product.IdUnit);
                product.ProductType = ProductTypes.First(s => s.Id == product.IdProductType);
                product.Fabricator = Fabricators.First(s => s.Id == product.IdFabricator);
            }
            SelectedClient = new ClientApi();
            ClientName = "";
            ProductTypeFilter = await Api.GetListAsync<List<ProductTypeApi>>("ProductType");
            ProductTypeFilter.Add(new ProductTypeApi { Title = "Все типы" });
            SelectedProductTypeFilter = ProductTypeFilter.Last();
            PrepareTreeView();
        }

        private void PrepareTreeView()
        {
            ProductTypeTreeViews.Clear();
            foreach (ProductTypeApi productType in ProductTypes)
            {
                var productTypeTreeView = new ProductTypeTreeView { Title = productType.Title };
                ProductTypeTreeViews.Add(productTypeTreeView);
                var ProductByProductType = Products.Where(s => s.IdProductType == productType.Id);
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
        private void UpdateList()
        {
            Products = searchResult;
        }
        private void UpdateListWithTreeView()
        {
            if (ClickedTreeElement == null) Products = FullProducts;
            if (ClickedTreeElement is ProductTypeTreeView)
            {
                var clickedEl = (ProductTypeTreeView)ClickedTreeElement;
                Products = FullProducts.Where(s => s.ProductType.Title == clickedEl.Title).ToList();
            }
            if (ClickedTreeElement is FabricatorTreeView)
            {
                var clickedEl = (FabricatorTreeView)ClickedTreeElement;
                Products = FullProducts.Where(s => s.ProductType.Title == clickedEl.Parent && s.Fabricator.Title == clickedEl.Title).ToList();
            }

        }
        private void Update()
        {
            //GetList();
            SignalChanged("Units");
            SignalChanged("ProductTypes");
            SignalChanged("Products");
            SignalChanged("ProductOrderIns");
            SignalChanged("LegalClients");
            SignalChanged("OrderOutVisuals");
            TotalCalculate();
            SelectedProductTypeFilter = ProductTypeFilter.Last();
        }

        public void ChangeCountProduct()
        {
            List<ProductOrderInApi> productOrderInApis = new List<ProductOrderInApi>(FullProductOrderIns);
            foreach (ProductOrderInApi productOrderIn in FullProductOrderIns)
            {
                foreach (ProductOrderInApi productOrderInToUpdate in ProductOrderInsToUpdate)
                {
                    if (productOrderIn.Id == productOrderInToUpdate.Id)
                    {
                        productOrderInApis.Remove(productOrderIn);
                        productOrderInApis.Add(productOrderInToUpdate);
                    }
                }
            }
            foreach (ProductApi product in Products)
            {
                product.Count = productOrderInApis.Where(s => s.IdProduct == product.Id).Select(s => s.Remains).Sum();
            }
        }

        private void TotalCalculate()
        {
            decimal total = 0;
            foreach (OrderOutVisual orderOutVisual in OrderOutVisuals)
            {
                total += (decimal)(orderOutVisual.Count * (orderOutVisual.Price - orderOutVisual.Discount));
            }
            Total = total.ToString();
            SignalChanged("Total");
        }
    }
}
