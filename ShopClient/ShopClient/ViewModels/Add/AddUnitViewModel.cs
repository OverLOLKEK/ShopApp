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
    public class AddUnitViewModel : BaseViewModel
    {
        public UnitApi AddUnit { get; set; }

        public CustomCommand Save { get; set; }
        public CustomCommand Cancel { get; set; }

        public AddUnitViewModel(UnitApi unit)
        {
            if (unit == null)
            {
                AddUnit = new UnitApi { };
            }
            else
            {
                AddUnit = new UnitApi
                {
                    Id = unit.Id,
                    Title = unit.Title,
                };
            }


            Save = new CustomCommand(() =>
            {
                MessageBoxResult result = MessageBox.Show("Сохранить изменения?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        if (AddUnit.Title == null)
                        {
                            MessageBox.Show("Заполнены не все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        if (AddUnit.Id == 0)
                            Add(AddUnit);
                        else
                            Edit(AddUnit);
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

        private async Task Add(UnitApi unit)
        {
            var id = await Api.PostAsync<UnitApi>(unit, "Unit");
        }
        private async Task Edit(UnitApi unit)
        {
            var id = await Api.PutAsync<UnitApi>(unit, "Unit");
        }
    }
}
