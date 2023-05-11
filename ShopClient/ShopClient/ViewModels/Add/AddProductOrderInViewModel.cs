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
   public class AddProductOrderInViewModel : BaseViewModel
    {
        

        private string productTitle;
        public string ProductTitle
        {
            get => productTitle;
            set
            {
                productTitle = value;
                SignalChanged();
            }
        }

        private string lastPrices;
        public string LastPrices
        {
            get => lastPrices;
            set
            {
                lastPrices = value;
                SignalChanged();
            }
        }

        private int count = 1;
        public int Count
        {
            get => count;
            set
            {
                if (value != count)
                {
                    count = value;
                    SignalChanged();
                    TotalCalculate();
                }
            }
        }

        private decimal? price;
        public decimal? Price
        {
            get => price;
            set
            {
                if(value != price)
                {
                    price = value;
                    SignalChanged();
                    TotalCalculate();
                }
              
            }
        }

        private decimal? total;
        public decimal? Total
        {
            get => total;
            set
            {
                    total = value;
                    SignalChanged();
            }

        }

        public CustomCommand Save { get; set; }
        public CustomCommand Cancel { get; set; }
        public CustomCommand AddOne { get; set; }

        decimal? buyPrice = 0;
        List<ProductOrderInApi> productOrderIns = new List<ProductOrderInApi>();
        List<ProductApi> Products = new List<ProductApi>();
        List<UnitApi> Units = new List<UnitApi>();
        List<FabricatorApi> Fabricators = new List<FabricatorApi>();
        public AddProductOrderInViewModel(ProductOrderInApi productOrderIn)
        {
            Price = 0;
            Total = 0;

            GetList(productOrderIn);

            Save = new CustomCommand(() =>
            {
                MessageBoxResult result = MessageBox.Show("Внести в документ?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (Count <= 0)
                    {
                        MessageBox.Show("Необходимо выбрать количество!");
                        return;
                    }
                    try
                    {
                        
                        productOrderIn.Price = Price;
                        productOrderIn.Remains = Count;
                        productOrderIn.Count = Count;
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


            AddOne = new CustomCommand(() =>
            {
                Count++;
            });
        }

 public void CloseWin(object obj)
        {
            Window win = obj as Window;
            win.Close();
        }

        private async Task GetList(ProductOrderInApi productOrderIn)
        {
            productOrderIns = await Api.GetListAsync<List<ProductOrderInApi>>("ProductOrderIn");
            Fabricators = await Api.GetListAsync<List<FabricatorApi>>("Fabricator");
            Units = await Api.GetListAsync<List<UnitApi>>("Unit");
            Products = await Api.GetListAsync<List<ProductApi>>("Product");
            
            productOrderIn.Product = Products.First(s => s.Id == productOrderIn.IdProduct);
            productOrderIn.Product.Unit = Units.First(s => s.Id == productOrderIn.Product.IdUnit);
            productOrderIn.Product.Fabricator = Fabricators.First(s => s.Id == productOrderIn.Product.IdFabricator);
            List<ProductOrderInApi> thisProductOrderin = productOrderIns.Where(c => c.IdProduct == productOrderIn.IdProduct).ToList();
           
            if(thisProductOrderin.Count != 0)
            {
                var result = thisProductOrderin.OrderBy(x => x.IdOrder);
                ProductOrderInApi productOrderInRes = result.Last();
                buyPrice = productOrderInRes.Price;
            }

            ProductTitle =  productOrderIn.Product.Title + $"  {productOrderIn.Product.Fabricator.Title} " + $" ({productOrderIn.Product.Unit.Title})";
            LastPrices = $"Последние цены: закупочная: {buyPrice}, розничная: {productOrderIn.Product.RetailPrice}, оптовая: {productOrderIn.Product.WholesalePrice}";
        }
         private void TotalCalculate()
        {
            Total = Count * Price;
            SignalChanged("Title");
        }
       
        private async Task Add(ProductOrderInApi productOrderIn)
        {
            var id = await Api.PostAsync<ProductOrderInApi>(productOrderIn, "ProductOrderIn");
        }
        
    }
}
