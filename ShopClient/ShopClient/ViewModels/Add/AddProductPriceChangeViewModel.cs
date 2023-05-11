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
    public class AddProductPriceChangeViewModel : BaseViewModel
    {

        public string ProductTitle { get; set; }
        public decimal? OldRetailPrice { get; set; }
        public decimal? OldWholesalePrice { get; set; }

        public decimal? newRetailPrice;
        public decimal? NewRetailPrice
        {
            get => newRetailPrice;
            set
            {
                newRetailPrice = value;
                RetailTotal = newRetailPrice;
                SignalChanged();
            }

        }
        public decimal? newWholesalePrice;
        public decimal? NewWholesalePrice
        {
            get => newWholesalePrice;
            set
            {
                newWholesalePrice = value;
                WholesaleTotal = newWholesalePrice;
                SignalChanged();
            }

        }

       
        public decimal? RetailTotal
        {
            get => newRetailPrice;
            set
            {
                newRetailPrice = value;
                SignalChanged();
            }
        }

        public decimal? WholesaleTotal
        {
            get => newWholesalePrice;
            set
            {
                newWholesalePrice = value;
                SignalChanged();
            }
        }

        private DateTime changeDate;
        public DateTime ChangeDate
        {
            get => changeDate;
            set
            {
                changeDate = value;
                SignalChanged();
            }
        }
        public List<ProductCostHistoryApi> productCostHistories = new List<ProductCostHistoryApi>();

        public CustomCommand Save { get; set; }
        public CustomCommand Cancel { get; set; }

        public AddProductPriceChangeViewModel(ProductApi product)
        {
            GetList();
            ProductTitle = $"{product.Fabricator.Title} " + product.Title + $" ({product.Unit.Title})";
            OldRetailPrice = product.RetailPrice;
            OldWholesalePrice = product.WholesalePrice;
            ChangeDate = DateTime.Now;
            NewWholesalePrice = product.WholesalePrice;
            NewRetailPrice = product.RetailPrice;


            Save = new CustomCommand(() =>
            {
                MessageBoxResult result = MessageBox.Show("Сохранить изменения?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        if (ChangeDate > DateTime.Now)
                        {
                            MessageBox.Show("Дата изменения не может быть позднней сегодняшней даты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        product.RetailPrice = RetailTotal;
                        product.WholesalePrice = WholesaleTotal;
                        
                        productCostHistories.OrderBy(x => x.ChangeDate).ToList();
                        if (productCostHistories.Last().ChangeDate < ChangeDate)
                        {
                            Edit(product);
                        }
                        Add(new ProductCostHistoryApi {IdProduct = product.Id, ChangeDate = ChangeDate, RetailPriceValue = RetailTotal, WholesalePirceValue = WholesaleTotal});
                      
                   
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

        }

        public void CloseWin(object obj)
        {
            Window win = obj as Window;
            win.Close();
        }

        private async Task Add(ProductCostHistoryApi productCostHistory)
        {
            var id = await Api.PostAsync<ProductCostHistoryApi>(productCostHistory, "ProductCostHistory");
        }
        private async Task GetList()
        {
            productCostHistories = await Api.GetListAsync<List<ProductCostHistoryApi>>("ProductCostHistory");
        }
        private async Task Edit(ProductApi product)
        {
            var id = await Api.PutAsync<ProductApi>(product, "Product");
        }
    }
}
