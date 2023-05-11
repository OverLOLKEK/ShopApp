using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Win32;
using ModelsApi;
using ShopClient.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Media;
using ShopClient.Views.Add;
using Spire.Barcode;
using ShopClient.Helper;

namespace ShopClient.ViewModels.Add
{
    public class AddProductViewModel : BaseViewModel
    {
        private List<string> timeStamps;
        public List<string> TimeStamps
        {
            get => timeStamps;
            set
            {
                timeStamps = value;
                SignalChanged();
            }
        }
        private string selectedTimeStamp;
        public string SelectedTimeStamp
        {
            get => selectedTimeStamp;
            set
            {
                if (value != selectedTimeStamp)
                {
                    selectedTimeStamp = value;
                    SignalChanged();
                    Chart(AddProduct);
                }
            }
        }

        public ProductApi AddProduct { get; set; }

        private BitmapImage imageProduct;
        public BitmapImage ImageProduct
        {
            get => imageProduct;
            set
            {
                imageProduct = value;
                SignalChanged();
            }
        }
        private List<FabricatorApi> fabricators;
        public List<FabricatorApi> Fabricators
        {
            get => fabricators;
            set
            {
                fabricators = value;
                SignalChanged();
            }
        }
        private List<UnitApi> units;
        public List<UnitApi> Units
        {
            get => units;
            set
            {
                units = value;
                SignalChanged();
            }
        }
        private List<ProductTypeApi> productTypes;
        public List<ProductTypeApi> ProductTypes
        {
            get => productTypes;
            set
            {
                productTypes = value;
                SignalChanged();
            }
        }
        private List<LegalClientApi> legalClients = new List<LegalClientApi>();
        public List<LegalClientApi> LegalClients
        {
            get => legalClients;
            set
            {
                Set(ref legalClients, value);
                SignalChanged();
            }
        }
        private FabricatorApi selectedFabricator;
        public FabricatorApi SelectedFabricator
        {
            get => selectedFabricator;
            set
            {
                selectedFabricator = value;
                SignalChanged();
            }
        }
        private ProductTypeApi selectedProductType;
        public ProductTypeApi SelectedProductType
        {
            get => selectedProductType;
            set
            {
                selectedProductType = value;
                SignalChanged();
            }
        }
        private UnitApi selectedUnit;
        public UnitApi SelectedUnit
        {
            get => selectedUnit;
            set
            {
                selectedUnit = value;
                SignalChanged();
            }
        }
        private LegalClientApi selectedLegalClient;
        public LegalClientApi SelectedLegalClient
        {
            get => selectedLegalClient;
            set
            {
                if (value != selectedLegalClient)
                {
                    selectedLegalClient = value;
                    SignalChanged();
                    Chart(AddProduct);
                }
            }
        }


        public CustomCommand Save { get; set; }
        public CustomCommand Cancel { get; set; }
        public CustomCommand SelectImage { get; set; }
        public CustomCommand UpdateChart { get; set; }

        private decimal? oldWholesale;
        private decimal? oldRetail;
        public List<ProductApi> Products;
        private List<ProductOrderInApi> FullProductOrderIns = new List<ProductOrderInApi>();
        private List<OrderApi> FullOrders = new List<OrderApi>();
        private List<ProductCostHistoryApi> ProductCostHistories = new List<ProductCostHistoryApi>();
        private List<ProductCostHistoryApi> ThisProductCostHistory = new List<ProductCostHistoryApi>();
        ChartValues<double> Retail = new ChartValues<double>();
        ChartValues<double> Wholesale = new ChartValues<double>();
        ChartValues<double> Purchase = new ChartValues<double>();
        List<DateTime> Dates = new List<DateTime>();
        List<DateTime> DatesPurchase = new List<DateTime>();

        public AddProductViewModel(ProductApi product)
        {
            Units = new List<UnitApi>();
            ProductTypes = new List<ProductTypeApi>();
            Fabricators = new List<FabricatorApi>();

            TimeStamps = new List<string>();
            TimeStamps.AddRange(new string[] { "За год", "За месяц", "За все время"});
            SelectedTimeStamp = TimeStamps.Last();

            if (product == null)
            {
                AddProduct = new ProductApi { Image="picture.JPG", Barcode = 0, Article = 0, RetailPrice = 0, WholesalePrice = 0};
                GetList(product);
            }
            else
            {
                AddProduct = new ProductApi
                {
                    Id = product.Id,
                    Title = product.Title,
                    Description = product.Description,
                    Article = product.Article,
                    Barcode = product.Barcode,
                    Image = product.Image,
                    IdFabricator = product.IdFabricator,
                    deleted_at = product.deleted_at,
                    IdUnit = product.IdUnit,
                    IdProductType = product.IdProductType,
                    RetailPrice = product.RetailPrice,
                    MinCount = product.MinCount,
                    WholesalePrice = product.WholesalePrice
                };
                oldWholesale = product.WholesalePrice;
                oldRetail = product.RetailPrice;

                GetList(product);

                if ( product.Image == null)
                {
                    product.Image = "picture.JPG";
                }
                if (product.Image.Length == 0)
                {
                    product.Image = "picture.JPG";
                }
                ImageProduct = GetImageFromPath(Environment.CurrentDirectory + "/Products/" + product.Image);
            }

            Save = new CustomCommand(() =>
            {
                MessageBoxResult result = MessageBox.Show("Сохранить изменения?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        if (AddProduct.Barcode.ToString().Length != 8 )
                        {
                            MessageBox.Show("Длина штрихкода - 8 символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        else if (AddProduct.Article == null || AddProduct.MinCount == null || AddProduct.Title == null )
                        {
                            MessageBox.Show("Заполнены не все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        foreach (var product in Products)
                        {
                            if (AddProduct.Article == product.Article)
                            {
                                if (AddProduct.Id == 0)
                                {
                                    MessageBox.Show("Артикул должен быть уникальным", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return;
                                }
                                else if (AddProduct.Id != product.Id)
                                {
                                    MessageBox.Show("Артикул должен быть уникальным", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return;
                                }
                            }
                        }
                        AddProduct.IdProductType = SelectedProductType.Id;
                        AddProduct.IdFabricator = SelectedFabricator.Id;
                        AddProduct.IdUnit = SelectedUnit.Id;
                        if (AddProduct.Id == 0)
                        { 
                          //  AddProduct.WholesalePrice = 0;
                          //  AddProduct.RetailPrice = 0;
                            Add(AddProduct);
                        }

                        else
                        {
                            Edit(AddProduct);
                            if (AddProduct.WholesalePrice != oldWholesale || AddProduct.RetailPrice != oldRetail)
                            {
                                AddProductPriceChange(new ProductCostHistoryApi { ChangeDate = DateTime.Now, IdProduct = AddProduct.Id, RetailPriceValue = AddProduct.RetailPrice, WholesalePirceValue = AddProduct.WholesalePrice});
                            }
                        }

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
                MessageBoxResult result = MessageBox.Show("Отменить изменения?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.DataContext == this)
                        {
                            CloseWin(window);
                        }
                    }
                else return;
            });

            UpdateChart = new CustomCommand(() =>
            {
                Chart(product);
            });

            SelectImage = new CustomCommand(() =>
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == true)
                {
                    try
                    {
                        var info = new FileInfo(ofd.FileName);
                        ImageProduct = GetImageFromPath(ofd.FileName);
                        AddProduct.Image = $"{info.Name}";
                        var newPath = Environment.CurrentDirectory + "/Products/" + AddProduct.Image;
                        File.Copy(ofd.FileName, newPath);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            });

            BuildChart();
        }

        private bool IsValid(ProductApi product) //сделать валидацию
        {
            foreach(ProductApi product1 in Products)
            {
                if (product.Article == product1.Article)
                {
                    MessageBox.Show("Артикул должен быть уникальным!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
        
            return true;
        }

        public SeriesCollection SeriesCollection { get; set; }
        private string[] labels;
        public string[] Labels 
        {
            get => labels;
            set
            {
                labels = value;
                SignalChanged();
            }
        }
        public Func<double, string> YFormatter { get; set; }

        public SeriesCollection SeriesCollectionPurchase { get; set; }
        private string[] labelsPurchase;
        public string[] LabelsPurchase
        {
            get => labelsPurchase;
            set
            {
                labelsPurchase = value;
                SignalChanged();
            }
        }
        public Func<double, string> YFormatterPurchase { get; set; }

        public void CloseWin(object obj)
        {
            Window win = obj as Window;
            win.Close();
        }

        private async Task AddProductPriceChange(ProductCostHistoryApi productCostHistory)
        {
            var id = await Api.PostAsync<ProductCostHistoryApi>(productCostHistory, "ProductCostHistory");
        }
        private async Task Add(ProductApi product)
        {
            var id = await Api.PostAsync<ProductApi>(product, "Product");
            await GetProducts();
            var lastProduct = Products.Last();
            await AddProductPriceChange(new ProductCostHistoryApi { ChangeDate = DateTime.Now, IdProduct = lastProduct.Id, RetailPriceValue = AddProduct.RetailPrice, WholesalePirceValue = AddProduct.WholesalePrice });
        }
        private async Task Edit(ProductApi product)
        {
            var id = await Api.PutAsync<ProductApi>(product, "Product");
        }
        private async Task GetProducts()
        {
            Products = await Api.GetListAsync<List<ProductApi>>("Product");
        }
            private async Task GetList(ProductApi product)
        {
            FullOrders = await Api.GetListAsync<List<OrderApi>>("Order");
            FullProductOrderIns = await Api.GetListAsync<List<ProductOrderInApi>>("ProductOrderIn");
            ProductCostHistories = await Api.GetListAsync<List<ProductCostHistoryApi>>("ProductCostHistory");
            ProductTypes = await Api.GetListAsync<List<ProductTypeApi>>("ProductType");
            if (product != null)
            {
                List<ChartProductOrderIn> purchaseHistory = new List<ChartProductOrderIn>();
                ThisProductCostHistory = ProductCostHistories.Where(c => c.IdProduct == product.Id).ToList();
                var thisOrderIns = FullProductOrderIns.Where(p => p.IdProduct == product.Id).ToList();
                foreach (var productOrderin in thisOrderIns)
                {
                    var order = FullOrders.First(s => s.Id == productOrderin.IdOrder);
                    purchaseHistory.Add(new ChartProductOrderIn { Date = (DateTime)order.Date, Price = (decimal)productOrderin.Price });
                }
                PrepareChart(ThisProductCostHistory, purchaseHistory);
            }
            Fabricators = await Api.GetListAsync<List<FabricatorApi>>("Fabricator");
            Units = await Api.GetListAsync<List<UnitApi>>("Unit");
            Products = await Api.GetListAsync<List<ProductApi>>("Product");
            if (product == null)
            {
                SelectedUnit = Units.First();
                SelectedFabricator = Fabricators.First();
                SelectedProductType = ProductTypes.First();
            }
            else
            {
                SelectedFabricator = Fabricators.First(s => s.Id == product.IdFabricator);
                SelectedUnit = Units.First(s => s.Id == product.IdUnit);
                SelectedProductType = ProductTypes.First(s => s.Id == product.IdProductType);
            }
            await fillSuppliers();
            SignalChanged("Fabricators");
            SignalChanged("Units");
            SignalChanged("ProductTypes");
            SignalChanged("SelectedLegalClient");
        }
        private async Task fillSuppliers()
        {
            LegalClients.Clear();
            var legalClients = await Api.GetListAsync<List<LegalClientApi>>("LegalClient");
            var thisOrderIns = FullProductOrderIns.Where(p => p.IdProduct == AddProduct.Id).ToList();
            foreach (var productOrderin in thisOrderIns)
            {
                var order = FullOrders.First(s => s.Id == productOrderin.IdOrder);
                var legalClient = legalClients.First(s=>s.IdClient == order.IdClient);
                if (!LegalClients.Contains(legalClient))
                {
                     LegalClients.Add(legalClient);
                }
            }
            LegalClients.Add(new LegalClientApi { Title = "Все поставщики" });
            SelectedLegalClient = LegalClients.Last();
        }
        private BitmapImage GetImageFromPath(string url)
        {
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.CacheOption = BitmapCacheOption.OnLoad;
            img.UriSource = new Uri(url, UriKind.Absolute);
            img.EndInit();
            return img;
        }
        public void Chart(ProductApi product)
        {
            List<ProductCostHistoryApi> TimeStampHistory;
            List<ChartProductOrderIn> PurchaseHistory = new List<ChartProductOrderIn>();
            if (SelectedTimeStamp == "За год")
            {
                TimeStampHistory = ProductCostHistories.Where(c => c.IdProduct == product.Id && c.ChangeDate > (DateTime.Now - TimeSpan.FromDays(365))).ToList();
                var thisOrderIns = FullProductOrderIns.Where(p => p.IdProduct == product.Id).ToList();
                foreach (var productOrderin in thisOrderIns)
                {
                    var order = FullOrders.First(s => s.Id == productOrderin.IdOrder);
                    if (order.Date > (DateTime.Now - TimeSpan.FromDays(365)))
                    {
                        if (!(SelectedLegalClient.Title == "Все поставщики"))
                        {
                            if (SelectedLegalClient.IdClient == order.IdClient)
                            {
                                PurchaseHistory.Add(new ChartProductOrderIn { Date = (DateTime)order.Date, Price = (decimal)productOrderin.Price });
                            }
                        }
                        else
                        PurchaseHistory.Add(new ChartProductOrderIn { Date = (DateTime)order.Date, Price = (decimal)productOrderin.Price });
                    }
                }
                PrepareChart(TimeStampHistory, PurchaseHistory);
            }
            else if (SelectedTimeStamp == "За месяц")
            {
                TimeStampHistory = ProductCostHistories.Where(c => c.IdProduct == product.Id && c.ChangeDate > (DateTime.Now - TimeSpan.FromDays(30))).ToList();
                var thisOrderIns = FullProductOrderIns.Where(p => p.IdProduct == product.Id).ToList();
                foreach (var productOrderin in thisOrderIns)
                {
                    var order = FullOrders.First(s => s.Id == productOrderin.IdOrder);
                    if (order.Date > (DateTime.Now - TimeSpan.FromDays(30)))
                    {
                        if (!(SelectedLegalClient.Title == "Все поставщики"))
                        {
                            if (SelectedLegalClient.IdClient == order.IdClient)
                            {
                                PurchaseHistory.Add(new ChartProductOrderIn { Date = (DateTime)order.Date, Price = (decimal)productOrderin.Price });
                            }
                        }
                        else
                            PurchaseHistory.Add(new ChartProductOrderIn { Date = (DateTime)order.Date, Price = (decimal)productOrderin.Price });
                    }
                }
                PrepareChart(TimeStampHistory, PurchaseHistory);
            }
            else if (SelectedTimeStamp == "За все время")
            {
                TimeStampHistory = ProductCostHistories.Where(c => c.IdProduct == product.Id).ToList();
                var thisOrderIns = FullProductOrderIns.Where(p => p.IdProduct == product.Id).ToList();
                foreach (var productOrderin in thisOrderIns)
                {
                    var order = FullOrders.First(s => s.Id == productOrderin.IdOrder);
                    if (!(SelectedLegalClient.Title == "Все поставщики"))
                    {
                        if (SelectedLegalClient.IdClient == order.IdClient)
                        {
                            PurchaseHistory.Add(new ChartProductOrderIn { Date = (DateTime)order.Date, Price = (decimal)productOrderin.Price });
                        }
                    }
                    else
                        PurchaseHistory.Add(new ChartProductOrderIn { Date = (DateTime)order.Date, Price = (decimal)productOrderin.Price });
                }
                PrepareChart(TimeStampHistory, PurchaseHistory);
            }
        }

        public void PrepareChart(List<ProductCostHistoryApi> productCostHistories, List<ChartProductOrderIn> productOrderIns)
        {
            Retail.Clear();
            Wholesale.Clear();
            Purchase.Clear();
            Dates.Clear();
            DatesPurchase.Clear();
            var result = productCostHistories.OrderBy(x => x.ChangeDate);
            var resultPurchase = productOrderIns.OrderBy(x => x.Date);
            foreach (ProductCostHistoryApi productCostHistory in result)
            {
                Retail.Add((double)productCostHistory.RetailPriceValue);
                Wholesale.Add((double)productCostHistory.WholesalePirceValue);
                Dates.Add((DateTime)productCostHistory.ChangeDate);
            }
            foreach (ChartProductOrderIn chartProductOrderIn in resultPurchase)
            {
                Purchase.Add((double)chartProductOrderIn.Price);
                DatesPurchase.Add((DateTime)chartProductOrderIn.Date);
            }
            string[] dateTimes = new string[Dates.Count];
            for (int i = 0; i < Dates.Count; i++)
            {
                dateTimes[i] = Dates[i].Date.ToShortDateString().ToString();
            }
            string[] dateTimesPurchase = new string[DatesPurchase.Count];
            for (int i = 0; i < DatesPurchase.Count; i++)
            {
                dateTimesPurchase[i] = DatesPurchase[i].Date.ToShortDateString().ToString();
            }
            Labels = dateTimes;
            LabelsPurchase = dateTimesPurchase;
            BuildChart();
        }

        public void BuildChart()
        {

            SeriesCollection = new SeriesCollection
              {
                new LineSeries
                {
                    Title = "Розн. цена",
                     Values = Retail
                },
                new LineSeries
                {
                    Title = "Опт. цена",
                    Values = Wholesale
                },

              };
            SeriesCollectionPurchase = new SeriesCollection
              {
                new LineSeries
                {
                    Title = "Закуп. цена",
                     Values = Purchase
                },
              };

            YFormatterPurchase = value => value.ToString("C");
            YFormatter = value => value.ToString("C");
        }
    }
}
