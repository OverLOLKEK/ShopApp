using ModelsApi;
using ShopClient.Core;
using ShopClient.Views.Add;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ShopClient.ViewModels
{
   public class ProductTypeViewModel : BaseViewModel
    {
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

        private ProductTypeApi selectedProductType = new ProductTypeApi { };
        public ProductTypeApi SelectedProductType
        {
            get => selectedProductType;
            set
            {
                selectedProductType = value;
                SignalChanged();
            }
        }

        private List<ProductApi> products;

        public CustomCommand AddProductType { get; set; }
        public CustomCommand EditProductType { get; set; }
        public CustomCommand DeleteProductType { get; set; }

        public ProductTypeViewModel()
        {
            ProductTypes = new List<ProductTypeApi>();
            GetList();


          
            AddProductType = new CustomCommand(() =>
            {
                AddProductType addProductType = new AddProductType();
                addProductType.ShowDialog();
                Thread.Sleep(200);
                GetList();
            });
            EditProductType = new CustomCommand(() =>
            {
            if (SelectedProductType == null || SelectedProductType.Id == 0) return;
                AddProductType addProductType = new AddProductType(SelectedProductType);
                addProductType.ShowDialog();
                Thread.Sleep(200);
                GetList();
            }); 
            DeleteProductType = new CustomCommand(() =>
            {
                MessageBoxResult result = MessageBox.Show("Удалить запись?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (SelectedProductType == null || SelectedProductType.Id == 0) return;

                    List<ProductApi> y = products.Where(s => s.IdProductType == SelectedProductType.Id).ToList();

                    if (y.Count != 0)
                    {
                        MessageBox.Show("Невозможно удалить, с этим товаром есть записи в БД!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    try
                    {
                        Delete(SelectedProductType);
                        GetList();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    };
                }
                else return;

            });
        }

        private async Task Delete(ProductTypeApi productType)
        {
            var res = await Api.DeleteAsync<ProductTypeApi>(productType, "ProductType");  
            GetList();
        }
        private async Task GetList()
        {
            products = await Api.GetListAsync<List<ProductApi>>("Product");
            ProductTypes = await Api.GetListAsync<List<ProductTypeApi>>("ProductType");
            SignalChanged("ProductTypes");
        }
       
    }
}
