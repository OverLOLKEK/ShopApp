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
    public class AddWriteOffViewModel : BaseViewModel
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
                }
            }
        }
        public ProductOrderInApi ThisProductOrderIn = new ProductOrderInApi();
        public int ProductOrderInRemains = 1;
        public CustomCommand Save { get; set; }
        public CustomCommand Cancel { get; set; }
        public CustomCommand AddOne { get; set; }

        public AddWriteOffViewModel(ProductOrderInApi productOrderIn, ObservableCollection<ProductOrderInApi> productOrderInsToUpdate, ObservableCollection<ProductOrderOutApi> productOrderOutsToUpdate)
        {
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
                    if (Count > ProductOrderInRemains)
                    {
                        MessageBox.Show($"Количество не соотвествует остаткам на складе ({ProductOrderInRemains})!");
                        return;
                    }
                    try
                    {
                        PrepareList(productOrderInsToUpdate, productOrderOutsToUpdate);

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

                if (Count > ProductOrderInRemains)
                {
                    return;
                }
                Count++;

            });
        }

        private void PrepareList(ObservableCollection<ProductOrderInApi> productOrderInsToUpdate, ObservableCollection<ProductOrderOutApi> productOrderOutsToUpdate)
        {
            productOrderOutsToUpdate.Add(new ProductOrderOutApi { Count = Count, Discount = 0, IdProductOrderIn = ThisProductOrderIn.Id, Price = ThisProductOrderIn.Price, Product = ThisProductOrderIn.Product });
            ThisProductOrderIn.Remains -= Count;
            productOrderInsToUpdate.Add(ThisProductOrderIn);
        }

        public void CloseWin(object obj)
        {
            Window win = obj as Window;
            win.Close();
        }

        private async Task GetList(ProductOrderInApi productOrderIn)
        {
            ProductOrderInRemains = (int)productOrderIn.Remains;
            ThisProductOrderIn = productOrderIn;
            ProductTitle = $"{productOrderIn.Product.Fabricator.Title} " + productOrderIn.Product.Title + $"(Номер партии: {productOrderIn.Id})";
            SignalChanged("ProductTitle");
        }

    }
}
