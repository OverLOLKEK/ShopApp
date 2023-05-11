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
    public class AddFabricatorViewModel : BaseViewModel
    {
        public FabricatorApi AddFabricator { get; set; }

        public CustomCommand Save { get; set; }
        public CustomCommand Cancel { get; set; }

        public AddFabricatorViewModel(FabricatorApi fabricator)
        {
            if (fabricator == null)
            {
                AddFabricator = new FabricatorApi { };
            }
            else
            {
                AddFabricator = new FabricatorApi
                {
                    Id = fabricator.Id,
                    Title = fabricator.Title,
                };
            }

            Save = new CustomCommand(() =>
            {
                MessageBoxResult result = MessageBox.Show("Сохранить изменения?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        if (AddFabricator.Title == null)
                        {
                            MessageBox.Show("Заполнены не все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        if (AddFabricator.Id == 0)
                            Add(AddFabricator);
                        else
                            Edit(AddFabricator);
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

        private async Task Add(FabricatorApi fabricator)
        {
            var id = await Api.PostAsync<FabricatorApi>(fabricator, "Fabricator");
        }
        private async Task Edit(FabricatorApi fabricator)
        {
            var id = await Api.PutAsync<FabricatorApi>(fabricator, "Fabricator");
        }
    }
}
