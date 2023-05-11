using ModelsApi;
using ShopClient.Core;
using ShopClient.Views;
using ShopClient.Views.Add;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ShopClient.ViewModels
{
    public class UnitViewModel : BaseViewModel
    {
        private List<UnitApi> unitApis;
        public List<UnitApi> Units
        {
            get => unitApis;
            set
            {
                Set(ref unitApis, value);  
                SignalChanged();
            }
        }

        private UnitApi selectedUnit = new UnitApi { };
        public UnitApi SelectedUnit
        {
            get => selectedUnit;
            set
            {
                selectedUnit = value;
                SignalChanged();
            }
        }

        private List<ProductApi> products;

        public CustomCommand AddUnit { get; set; }
        public CustomCommand EditUnit { get; set; }
        public CustomCommand DeleteUnit { get; set; }

        public UnitViewModel()
        {
            Units = new List<UnitApi>();
            GetList();

            AddUnit = new CustomCommand(() =>
            {
                AddUnit addunit = new AddUnit();
                addunit.ShowDialog();
                Thread.Sleep(200);
                GetList();
            });
            EditUnit = new CustomCommand(() =>
            {
                if (SelectedUnit == null || SelectedUnit.Id == 0) return;
                AddUnit addunit = new AddUnit(SelectedUnit);
                addunit.ShowDialog();
                Thread.Sleep(200);
                GetList();
            });
            DeleteUnit = new CustomCommand(() =>
            {
                MessageBoxResult result = MessageBox.Show("Удалить запись?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                { 
                    if (SelectedUnit == null || SelectedUnit.Id == 0) return;

                    List<ProductApi> y = products.Where(s => s.IdUnit == SelectedUnit.Id).ToList();

                    if (y.Count != 0)
                    {
                        MessageBox.Show("Невозможно удалить, с этим товаром есть записи в БД!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    try
                    {
                        Delete(SelectedUnit);
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

        private async Task GetList()
        {
            products = await Api.GetListAsync<List<ProductApi>>("Product");
            Units = await Api.GetListAsync<List<UnitApi>>("Unit");
            SignalChanged("Units");

        }
        private async Task Delete(UnitApi unit)
        {
            var res = await Api.DeleteAsync<UnitApi>(unit, "Unit");
            GetList();
        }


    }
}
