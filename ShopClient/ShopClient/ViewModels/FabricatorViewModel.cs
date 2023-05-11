using ModelsApi;
using ShopClient.Core;
using ShopClient.Views.Add;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ShopClient.ViewModels
{
    public class FabricatorViewModel : BaseViewModel
    {
        private List<FabricatorApi> fabricators;
        public List<FabricatorApi> Fabricators
        {
            get => fabricators;
            set
            {
                if (value != fabricators)
                {
                  Set(ref fabricators, value); 
                  SignalChanged();
                  SignalChanged("Fabricators");
                }
            }
        }

        private FabricatorApi selectedFabricator = new FabricatorApi { };
        public FabricatorApi SelectedFabricator
        {
            get => selectedFabricator;
            set
            {
                selectedFabricator = value;
                SignalChanged();
            }
        }
        private List<ProductApi> products;

        public CustomCommand AddFabricator { get; set; }
        public CustomCommand EditFabricator { get; set; }
        public CustomCommand DeleteFabricator { get; set; }

        public FabricatorViewModel()
        {
            Fabricators = new List<FabricatorApi>();
            Task.Run(GetList);

            AddFabricator = new CustomCommand(() =>
            {
                AddFabricator addFabricator = new AddFabricator();
                addFabricator.ShowDialog();
                Thread.Sleep(200);
                Task.Run(GetList);
            });
            EditFabricator = new CustomCommand(() =>
            {
                if (SelectedFabricator == null || SelectedFabricator.Id == 0) return;
                AddFabricator addunit = new AddFabricator(SelectedFabricator);
                addunit.ShowDialog();
                Thread.Sleep(200);
                Task.Run(GetList);
            });
            DeleteFabricator = new CustomCommand(() =>
            {
                MessageBoxResult result = MessageBox.Show("Удалить запись?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (SelectedFabricator == null || SelectedFabricator.Id == 0) return;

                    List<ProductApi> y = products.Where(s => s.IdFabricator == SelectedFabricator.Id).ToList();

                    if (y.Count != 0)
                    {
                        MessageBox.Show("Невозможно удалить, с этим товаром есть записи в БД!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    try
                    {
                        Delete(SelectedFabricator);
                        Task.Run(GetList);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    };
                }
                else return;

            });
        }

        private async Task GetList()
        {
            products = await Api.GetListAsync<List<ProductApi>>("Product");
            Fabricators = await Api.GetListAsync<List<FabricatorApi>>("Fabricator");
            SignalChanged("Fabricators");
        }
        private async Task Delete(FabricatorApi fabricator)
        {
            var res = await Api.DeleteAsync<FabricatorApi>(fabricator, "Fabricator");
            GetList();
        }

    }
}
