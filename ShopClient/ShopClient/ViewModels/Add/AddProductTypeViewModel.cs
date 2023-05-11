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
    public class AddProductTypeViewModel : BaseViewModel
    {
        public ProductTypeApi AddProductType { get; set; }

        public CustomCommand Save { get; set; }
        public CustomCommand Cancel { get; set; }

        public AddProductTypeViewModel(ProductTypeApi productType)
        {
            if (productType == null)
            {
                AddProductType = new ProductTypeApi { };
            }
            else
            {
                AddProductType = new ProductTypeApi
                {
                    Id = productType.Id,
                    Title = productType.Title,
                };
            }


            Save = new CustomCommand(() =>
            {
                MessageBoxResult result = MessageBox.Show("Сохранить изменения?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        if (AddProductType.Title == null)
                        {
                            MessageBox.Show("Заполнены не все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        if (AddProductType.Id == 0)
                            Add(AddProductType);
                        else
                            Edit(AddProductType);
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

        private async Task Add(ProductTypeApi productType)
        {
            var id = await Api.PostAsync<ProductTypeApi>(productType, "ProductType");
        }
        private async Task Edit(ProductTypeApi productType)
        {
            var id = await Api.PutAsync<ProductTypeApi>(productType, "ProductType");
        }
    }
}
