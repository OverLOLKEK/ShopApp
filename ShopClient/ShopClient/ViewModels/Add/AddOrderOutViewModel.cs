using ModelsApi;
using ShopClient.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopClient.ViewModels.Add
{
   public class AddOrderOutViewModel : BaseViewModel
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

        private int? count = 1;
        public int? Count
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
                if (value != price)
                {
                    price = value;
                    SignalChanged();
                    TotalCalculate();
                }

            }
        }
        private decimal? discount;
        public decimal? Discount
        {
            get => discount;
            set
            {
                if (value != discount)
                {
                    discount = value;
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

        private string pickedSaleType;
        public string PickedSaleType
        {
            get => pickedSaleType;
            set
            {
                pickedSaleType = value;
                SignalChanged();
            }
        }

        public CustomCommand Save { get; set; }
        public CustomCommand Cancel { get; set; }
        public CustomCommand AddOne { get; set; }
        int ListCount = 0;
        int? ProductCount = 0;
        decimal? buyPrice = 0;
        decimal? thisPrice = 0;
        ProductApi ThisProduct = new ProductApi();

        List<ProductOrderInApi> productOrderIns = new List<ProductOrderInApi>();
        List<ProductOrderInApi> ThisProductOrderIns = new List<ProductOrderInApi>();
        List<OrderApi> ThisProductOrders = new List<OrderApi>();
        List<OrderApi> FullOrders = new List<OrderApi>();
        List<ProductApi> Products = new List<ProductApi>();
        List<UnitApi> Units = new List<UnitApi>();
        List<FabricatorApi> Fabricators = new List<FabricatorApi>();

        public AddOrderOutViewModel(List<ProductOrderOutApi> productOrderOuts, SaleTypeApi saleType, ObservableCollection<ProductOrderInApi> productOrderInsToUpdate , ProductApi SelectedProduct)
        {
            Discount = 0;
            Price = 0;
            Total = 0;

            GetList(productOrderOuts, saleType, SelectedProduct);

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
                    if (Count > SelectedProduct.Count)
                    {
                        MessageBox.Show($"Количество не соотвествует остаткам на складе ({SelectedProduct.Count})!");
                        return;
                    }
                    try
                    {
                        PrepareList(productOrderOuts, productOrderInsToUpdate);
                        //productOrderIn.Price = Price;
                        //productOrderIn.Remains = Count;
                        //productOrderIn.Count = Count;
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
                
                if (Count > ProductCount)
                {
                    return;
                }
                Count++;
              
            });
        }

        private void PrepareList(List<ProductOrderOutApi> productOrderOuts, ObservableCollection<ProductOrderInApi> productOrderInsToUpdate)
        {
            var OrderIns = ThisProductOrderIns.OrderBy(s => s.Order.Date).ToList();
            ThisProductOrderIns = OrderIns;
            ThisProductOrderIns.ToArray();
            int? countRemains = Count;
            for (int i = 0; countRemains > 0; i++)
            {
                var countRemainsBefore = countRemains;
                countRemains -= ThisProductOrderIns[i].Remains;
                if (countRemains <= 0)
                {
                    productOrderOuts.Add(new ProductOrderOutApi { IdProductOrderIn = ThisProductOrderIns[i].Id, Product = ThisProduct, Price = thisPrice, Count = countRemainsBefore, Discount = Discount });
                    ThisProductOrderIns[i].Remains = Math.Abs((int)countRemains);
                }
                else
                {
                    productOrderOuts.Add(new ProductOrderOutApi { IdProductOrderIn = ThisProductOrderIns[i].Id, Product = ThisProduct, Price = thisPrice, Count = Count - countRemains, Discount = Discount });
                    ThisProductOrderIns[i].Remains = 0;
                }
                productOrderInsToUpdate.Add(ThisProductOrderIns[i]);
            }

            //ThisProductOrderIns.ToArray();
            //int? countRemains = Count;
            //for (int i = 0; countRemains > 0; i++)
            //{
            //    var countRemainsBefore = countRemains;
            //    countRemains -= ThisProductOrderIns[i].Remains;
            //    if (countRemains < 0)
            //    {
            //        var toggle = 0;
            //        foreach (var item in productOrderOuts)
            //        {
            //            if (ThisProductOrderIns[i].Id == item.IdProductOrderIn && item.Discount == Discount)
            //            {
            //                item.Count += countRemainsBefore;
            //                toggle = 1;
            //            }
            //        }
            //        if (toggle == 0)
            //        {
            //        productOrderOuts.Add(new ProductOrderOutApi { IdProductOrderIn = ThisProductOrderIns[i].Id, Product = ThisProduct, Price = thisPrice, Count = countRemainsBefore, Discount = Discount });
            //        ThisProductOrderIns[i].Remains = Math.Abs((int)countRemains);
            //        }

            //    }
            //    else
            //    {
            //        var toggle = 0;
            //        foreach (var item in productOrderOuts)
            //        {
            //            if (ThisProductOrderIns[i].Id == item.IdProductOrderIn && item.Discount == Discount)
            //            {
            //                item.Count += Count - countRemains;
            //                toggle = 1;
            //            }
            //        }
            //        if (toggle == 0)
            //        {
            //            productOrderOuts.Add(new ProductOrderOutApi { IdProductOrderIn = ThisProductOrderIns[i].Id, Product = ThisProduct, Price = thisPrice, Count = Count - countRemains, Discount = Discount });
            //            ThisProductOrderIns[i].Remains = 0;
            //        }
            //    }
            //    var toggle1 = 0;
            //    foreach (var item in productOrderInsToUpdate)
            //    {
            //        if (ThisProductOrderIns[i].Id == item.Id)
            //        {
            //            var orderOut = productOrderOuts.First(s => s.IdProductOrderIn == item.Id);
            //            item.Remains = item.Count - orderOut.Count;
            //            toggle1 = 1;
            //        }
            //    }
            //    if (toggle1 == 0)
            //    {
            //        productOrderInsToUpdate.Add(ThisProductOrderIns[i]);
            //    }
            //}
        }

        public void CloseWin(object obj)
        {
            Window win = obj as Window;
            win.Close();
        }

        private async Task GetList(List<ProductOrderOutApi> productOrderOuts, SaleTypeApi saleType, ProductApi SelectedProduct)
        {
            FullOrders = await Api.GetListAsync<List<OrderApi>>("Order");
            productOrderIns = await Api.GetListAsync<List<ProductOrderInApi>>("ProductOrderIn");
            Fabricators = await Api.GetListAsync<List<FabricatorApi>>("Fabricator");
            Units = await Api.GetListAsync<List<UnitApi>>("Unit");
            Products = await Api.GetListAsync<List<ProductApi>>("Product");

          
            ThisProductOrderIns = productOrderIns.Where(s => s.IdProduct == SelectedProduct.Id && s.Remains > 0).ToList();
            SelectedProduct.Count = ThisProductOrderIns.Select(s => s.Remains).Sum();
            ThisProduct = SelectedProduct;
            ProductCount = SelectedProduct.Count;
            //List<ProductOrderInApi> thisProductOrderin = productOrderIns.Where(c => c.IdProduct == productOrderIn.IdProduct).ToList();
            foreach (var item in ThisProductOrderIns)
            {
                item.Order = FullOrders.Find(s => s.Id == item.IdOrder);
            }
            ThisProductOrderIns.OrderBy(s => s.Order.Date);
           
            //var result = ThisProductOrders.OrderBy(x => x.Date);

            
            //if (thisProductOrderin.Count != 0)
            //{
            //    var result = thisProductOrderin.OrderBy(x => x.IdOrder);
            //    ProductOrderInApi productOrderInRes = result.Last();
            //    buyPrice = productOrderInRes.Price;
            //}
            if (saleType.Title == "Оптовая")
            {
                Price = SelectedProduct.WholesalePrice; 
                thisPrice = SelectedProduct.WholesalePrice;
            }
            else if (saleType.Title == "Розничная")
            {
                Price = SelectedProduct.RetailPrice;
                thisPrice = SelectedProduct.RetailPrice;
            }
            PickedSaleType = $"Цена: ({saleType.Title})";
            ProductTitle =  SelectedProduct.Title + $"  {SelectedProduct.Fabricator.Title} " + $" ({SelectedProduct.Unit.Title})";
        }
        private void TotalCalculate()
        {
            Total = Count * (Price - Discount);
            SignalChanged("Title");
        }

        private async Task Add(ProductOrderInApi productOrderIn)
        {
            var id = await Api.PostAsync<ProductOrderInApi>(productOrderIn, "ProductOrderIn");
        }


    }
}
