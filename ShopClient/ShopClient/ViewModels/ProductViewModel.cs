using ModelsApi;
using ShopClient.Core;
using ShopClient.Views.Add;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ShopClient.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        public List<string> ViewCountRows { get; set; }

        public string SelectedViewCountRows
        {
            get => selectedViewCountRows;
            set
            {
                selectedViewCountRows = value;
                paginationPageIndex = 0;
                Pagination();
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

        public string SearchCountRows
        {
            get => searchCountRows;
            set
            {
                searchCountRows = value;
                SignalChanged();
            }
        }
        private string pages;
        public string Pages
        {
            get => pages;
            set
            {
                pages = value;
                SignalChanged();
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
        private List<FabricatorApi> fabricators;
        public List<FabricatorApi> Fabricators
        {
            get => fabricators;
            set
            {
                Set(ref fabricators, value);
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

        public CustomCommand AddProduct{ get; set; }
        public CustomCommand EditProduct { get; set; }
        public CustomCommand DeleteProduct { get; set; }
        public CustomCommand ShowOrderIns { get; set; }
        public CustomCommand BackPage { get; set; }
        public CustomCommand ForwardPage { get; set; }
        public CustomCommand ProductPriceChange { get; set; }
        
        private List<ProductApi> FullProducts = new List<ProductApi>();
        private List<ProductOrderInApi> ProductOrderIns = new List<ProductOrderInApi>();
        public int rows = 0;
        public int CountPages = 0;
        List<ProductApi> searchResult;
        int paginationPageIndex = 0;
        private string searchCountRows;
        private string selectedViewCountRows;
        public ProductViewModel()
        {
            Fabricators = new List<FabricatorApi>();
            Units = new List<UnitApi>();
            ProductTypes = new List<ProductTypeApi>();
            Products = new List<ProductApi>();
            ProductTypeFilter = new List<ProductTypeApi>();
            GetList();

            ViewCountRows = new List<string>();
            ViewCountRows.AddRange(new string[] { "10", "50", "все" });
            selectedViewCountRows = ViewCountRows.First();

            SearchType = new List<string>();
            SearchType.AddRange(new string[] { "Наименование", "Артикул", "Производитель" });
            selectedSearchType = SearchType.First();

            BackPage = new CustomCommand(() =>
            {
                if (searchResult == null)
                    return;
                if (paginationPageIndex > 0)
                    paginationPageIndex--;
                Pagination();
            });

            ForwardPage = new CustomCommand(() =>
            {
                if (searchResult == null)
                    return;
                int.TryParse(SelectedViewCountRows, out int rowsOnPage);
                if (rowsOnPage == 0)
                    return;
                int countPage = searchResult.Count() / rowsOnPage;
                CountPages = countPage;
                if (searchResult.Count() % rowsOnPage != 0)
                    countPage++;
                if (countPage > paginationPageIndex + 1)
                    paginationPageIndex++;
                Pagination();

            });
            
            AddProduct = new CustomCommand(() =>
            {
                if (Fabricators.Count == 0 || Units.Count == 0 || ProductTypes.Count == 0)
                {
                    MessageBox.Show("Необходимо иметь в БД хотя бы одну запись о производителе, ед. измерения и типе товара", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                AddProduct addProduct = new AddProduct();
                addProduct.ShowDialog();
                Thread.Sleep(200);
                Update();
                InitPagination();
                Pagination();
            });
            ShowOrderIns = new CustomCommand(() =>
            {
                if (SelectedProduct == null || SelectedProduct.Id == 0) return;
                OrderInsByProduct orderIns = new OrderInsByProduct(SelectedProduct);
                orderIns.ShowDialog();
                Thread.Sleep(200);
                Update();
                InitPagination();
                Pagination();
            });
            EditProduct = new CustomCommand(() =>
            {
                if (SelectedProduct == null || SelectedProduct.Id == 0) return;
                int pos = SelectedProduct.Image.LastIndexOf('/');
                SelectedProduct.Image = SelectedProduct.Image.Substring(pos + 1);
                AddProduct addProduct = new AddProduct(SelectedProduct);
                addProduct.ShowDialog();
                Thread.Sleep(200);
                Update();
                InitPagination();
                Pagination();
            });

            ProductPriceChange = new CustomCommand(() =>
            {
                if (SelectedProduct == null || SelectedProduct.Id == 0) return;
                int pos = SelectedProduct.Image.LastIndexOf('/');
                SelectedProduct.Image = SelectedProduct.Image.Substring(pos + 1);
                AddProductPriceChange addProductPriceChange = new AddProductPriceChange(SelectedProduct);
                addProductPriceChange.ShowDialog();
                Thread.Sleep(200);
                Update();
                InitPagination();
                Pagination();
            });

            DeleteProduct = new CustomCommand(() =>
            {
                MessageBoxResult result = MessageBox.Show("Удалить запись?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (SelectedProduct == null || SelectedProduct.Id == 0) return;
                    try
                    {
                        var x = 0;
                        foreach (var item in ProductOrderIns)
                        {
                            if (item.IdProduct == SelectedProduct.Id)
                            {
                                x = 1;
                            }
                        }
                        if (x == 1)
                        {
                            MessageBox.Show("Невозможно удалить, с этим товаром есть записи в БД!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        Delete(SelectedProduct);
                        Update();
                        SignalChanged("Products");
                        InitPagination();
                        Pagination();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    };
                }
                else return;

            });
        }

        private void InitPagination()
        {
            SearchCountRows = $"Найдено записей: {searchResult.Count} из {FullProducts.Count}";
            paginationPageIndex = 0;
        }

        private void Pagination()
        {
            int rowsOnPage = 0;
            if (!int.TryParse(SelectedViewCountRows, out rowsOnPage))
            {
                Products = searchResult;
            }
            else
            {
                Products = searchResult.Skip(rowsOnPage * paginationPageIndex)
                    .Take(rowsOnPage).ToList();

            }

            int.TryParse(SelectedViewCountRows, out rows);
            if (rows == 0)
                rows = FullProducts.Count;
            CountPages = (searchResult.Count() - 1) / rows;
            Pages = $"{paginationPageIndex + 1}/{CountPages + 1}";
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
                        .Where(c => c.Article.ToString().ToLower().Contains(search)).ToList();
                else if (SelectedSearchType == "Производитель")
                    searchResult = FullProducts
                        .Where(c => c.Fabricator.Title.ToString().ToLower().Contains(search)).ToList();
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
                        .Where(c => c.Fabricator.Title.ToString().ToLower().Contains(search) && c.ProductType.Title.Contains(SelectedProductTypeFilter.Title)).ToList();
            }
            InitPagination();
            Pagination();
        }

        private async Task Delete(ProductApi product)
        {
            List<ProductCostHistoryApi> productPriceChanges = await Api.GetListAsync<List<ProductCostHistoryApi>>("ProductCostHistory");
            List<ProductCostHistoryApi> thisPriceChanges = productPriceChanges.Where(s => s.IdProduct == product.Id).ToList();
            foreach (var item in thisPriceChanges)
            {
                var res1 = await Api.DeleteAsync<ProductCostHistoryApi>(item, "ProductCostHistory");
            }
            var res = await Api.DeleteAsync<ProductApi>(product, "Product");
            await GetList();
        }
        private async Task GetList()
        {
            Fabricators = await Api.GetListAsync<List<FabricatorApi>>("Fabricator");
            Units = await Api.GetListAsync<List<UnitApi>>("Unit");
            ProductTypes = await Api.GetListAsync<List<ProductTypeApi>>("ProductType");
            Products = await Api.GetListAsync<List<ProductApi>>("Product");
            ProductOrderIns = await Api.GetListAsync<List<ProductOrderInApi>>("ProductOrderIn");
            FullProducts = Products;
            foreach (ProductApi product in Products)
            {
                if (File.Exists(Environment.CurrentDirectory + "/Products/" + product.Image))
                    product.Image = Environment.CurrentDirectory + "/Products/" + product.Image;
                else
                    product.Image = Environment.CurrentDirectory + "/Products/" + "picture.JPG";
                product.Count = ProductOrderIns.Where(s => s.IdProduct == product.Id).Select(s => s.Remains).Sum();
                product.Fabricator = Fabricators.First(s => s.Id == product.IdFabricator);
                product.Unit = Units.First(s => s.Id == product.IdUnit);
                product.ProductType = ProductTypes.First(s => s.Id == product.IdProductType);
                if (product.Count < product.MinCount)
                {
                    product.ColorForXaml = "#FFE26464";
                }
                else if (product.Count > product.MinCount * 3)
                {
                    product.ColorForXaml = "#FF9EEA82";
             }
            }
            ProductTypeFilter = await Api.GetListAsync<List<ProductTypeApi>>("ProductType");
            ProductTypeFilter.Add(new ProductTypeApi { Title = "Все типы" });
            SelectedProductTypeFilter = ProductTypeFilter.Last();
        
            InitPagination();
            Pagination();
        }

        private void Update()
        { 
            
            GetList();
            SignalChanged("Units");
            SignalChanged("ProductTypes");
            SignalChanged("Products");
            SelectedProductTypeFilter = ProductTypeFilter.Last();
        }
      
    }
}
