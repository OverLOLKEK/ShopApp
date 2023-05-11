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
   public class ProductOrderInViewModel : BaseViewModel
    {

        private object _clickedTreeElement;
        public object ClickedTreeElement
        {
            get => _clickedTreeElement;

            set {
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
        private ObservableCollection<ProductOrderInApi> productOrderIns;
        public ObservableCollection<ProductOrderInApi> ProductOrderIns
        {
            get => productOrderIns;
            set
            {
                Set(ref productOrderIns, value);
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
        private List<LegalClientApi> legalClients;
        public List<LegalClientApi> LegalClients
        {
            get => legalClients;
            set
            {
                Set(ref legalClients, value);
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
        private LegalClientApi selectedLegalClient;
        public LegalClientApi SelectedLegalClient
        {
            get => selectedLegalClient;
            set
            {
                selectedLegalClient = value;
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
        public CustomCommand AddProduct { get; set; }
        public CustomCommand AddOrder { get; set; }
        public CustomCommand DeleteProductOrderIn { get; set; }

        public List<OrderApi> Orders;
        List<ProductApi> searchResult;
        private List<ProductOrderInApi> FullProductOrderIns = new List<ProductOrderInApi>();
        private List<ProductApi> FullProducts = new List<ProductApi>();
        private List<ActionTypeApi> ActionTypes = new List<ActionTypeApi>();
        private List<FabricatorApi> Fabricators = new List<FabricatorApi>();
        public ProductOrderInViewModel()
        {
            Units = new List<UnitApi>();
            ProductTypes = new List<ProductTypeApi>();
            Products = new List<ProductApi>();
            ProductTypeFilter = new List<ProductTypeApi>();
            ProductOrderIns = new ObservableCollection<ProductOrderInApi>();
            GetList();

            SearchType = new List<string>();
            SearchType.AddRange(new string[] { "Наименование", "Артикул", "Производитель" });
            selectedSearchType = SearchType.First();

            AddProduct = new CustomCommand(() =>
            {
                if (SelectedProduct == null || SelectedProduct.Id == 0) return;
                foreach (var item in ProductOrderIns)
                {
                    if (SelectedProduct.Id == item.IdProduct)
                    {
                        MessageBox.Show("Такой продукт уже есть в заказе!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                ProductOrderInApi productOrderIn = new ProductOrderInApi{ IdProduct = SelectedProduct.Id};
                AddProductOrderIn addProductOrderIn = new AddProductOrderIn(productOrderIn);
                addProductOrderIn.ShowDialog();
                if (productOrderIn.Count <= 0 || productOrderIn.Count == null)
                {
                    Update();
                    return;
                }
                GetProperties(productOrderIn);
                ProductOrderIns.Add(productOrderIn);
                Update();
            });
            AddOrder = new CustomCommand(() =>
            {
                MessageBoxResult result = MessageBox.Show("Принять заказ?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (ProductOrderIns.Count == 0 || ProductOrderIns == null)
                    {
                        MessageBox.Show("Заказ пуст!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (SelectedLegalClient == null)
                    {
                        MessageBox.Show("Необходимо выбрать поствщика!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    try
                    {
                        ActionTypeApi actionType = ActionTypes.First(c => c.Name == "Поступление");
                        ClientApi client = SelectedLegalClient.Client;

                        AddNewOrder(new OrderApi {Date = SelectedDate, IdActionType = actionType.Id, IdClient =client.Id });
                        
                       
                     
                        MessageBox.Show("Заказ принят", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                        SelectedLegalClient = null;
                        ProductOrderIns.Clear();
                        SelectedDate = DateTime.Now;
                        SignalChanged("SelectedDate");

                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    };
                    Update();
                }
                else return;


            });
            DeleteProductOrderIn = new CustomCommand(() =>
            {
                if (SelectedProductOrderIn == null || SelectedProductOrderIn.Id == 0) return;
                ProductOrderIns.Remove(SelectedProductOrderIn);
                Update();
            });
            UpdateList();
        }

        private async Task AddProductOrdersIn(ProductOrderInApi productOrderIn)
        {
            await GetListOrders();
            var orderId = Orders.Last().Id;
            await AddNewProductOrderIn(new ProductOrderInApi { Count = productOrderIn.Count, Price = productOrderIn.Price, Remains = productOrderIn.Count, IdOrder = orderId, IdProduct = productOrderIn.IdProduct });
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
        private async Task GetListOrders()
        {
            Orders = await Api.GetListAsync<List<OrderApi>>("Order");
        }
        private async Task GetList()
        {
            ActionTypes = await Api.GetListAsync<List<ActionTypeApi>>("ActionType");
            var legalClients = await Api.GetListAsync<List<LegalClientApi>>("LegalClient");
            LegalClients = legalClients.Where(s => s.IsSupplier == 1).ToList();
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
            ProductTypeFilter = await Api.GetListAsync<List<ProductTypeApi>>("ProductType");
            ProductTypeFilter.Add(new ProductTypeApi { Title = "Все типы" });
            SelectedProductTypeFilter = ProductTypeFilter.Last();
      
            PrepareTreeView();
        }
        private void PrepareTreeView()
        {
            ProductTypeTreeViews.Clear();
            foreach(ProductTypeApi productType in ProductTypes)
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
        private void GetProperties(ProductOrderInApi productOrderIn)
        {
                productOrderIn.Product = Products.First(s => s.Id == productOrderIn.IdProduct);
                productOrderIn.Product.Unit = Units.First(s => s.Id == productOrderIn.Product.IdUnit);
                productOrderIn.Product.ProductType = ProductTypes.First(s => s.Id == productOrderIn.Product.IdProductType);
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
        private async Task AddNewOrder(OrderApi order)
        {
            var id = await Api.PostAsync<OrderApi>(order, "Order");

            foreach (ProductOrderInApi productOrderIn in ProductOrderIns)
            {
                await AddProductOrdersIn(productOrderIn);
            }
        }
        private async Task AddNewProductOrderIn(ProductOrderInApi productOrderIn)
        {
            var id = await Api.PostAsync<ProductOrderInApi>(productOrderIn, "ProductOrderIn");
        }
        private void Update()
        {
            GetList();
            SignalChanged("Units");
            SignalChanged("ProductTypes");
            SignalChanged("Products");
            SignalChanged("ProductOrderIns");
            SignalChanged("LegalClients");
            TotalCalculate();
            SelectedProductTypeFilter = ProductTypeFilter.Last();
        }
        private void TotalCalculate()
        {
            decimal total = 0;
            foreach(ProductOrderInApi productOrderInApi in ProductOrderIns)
            {
                total += (decimal)(productOrderInApi.Count * productOrderInApi.Price);
            }
            Total = total.ToString()+",0000";
            SignalChanged("Total");
        }
    }
}
