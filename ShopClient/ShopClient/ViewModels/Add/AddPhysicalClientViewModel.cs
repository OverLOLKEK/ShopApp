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
    public class AddPhysicalClientViewModel : BaseViewModel
    {
        public PhysicalClientApi AddPhysicalClient { get; set; }
        public ClientApi AddClient { get; set; } = new ClientApi();

        public CustomCommand Save { get; set; }
        public CustomCommand Cancel { get; set; }

        public AddPhysicalClientViewModel(PhysicalClientApi physicalClient)
        {
            if (physicalClient == null)
            {
                AddPhysicalClient = new PhysicalClientApi { Client = AddClient};
            }
            else
            {
                AddPhysicalClient = new PhysicalClientApi
                {
                    Id = physicalClient.Id,
                    Client = physicalClient.Client,
                    LastName = physicalClient.LastName,
                    FirstName = physicalClient.FirstName,
                    Patronymic = physicalClient.Patronymic
                };
            }


            Save = new CustomCommand(() =>
            {
                MessageBoxResult result = MessageBox.Show("Сохранить изменения?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        if (AddPhysicalClient.FirstName == null || AddPhysicalClient.LastName == null || AddPhysicalClient.Patronymic == null || AddPhysicalClient.Client.Phone == null || AddPhysicalClient.Client.Address == null)
                        {
                            MessageBox.Show("Заполнены не все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        if (AddPhysicalClient.Id == 0)
                        {
                            Add(AddPhysicalClient);
                            AddNewClient(AddClient);
                        }
                        else
                        {
                            Edit(AddPhysicalClient);
                            EditClient(AddPhysicalClient.Client);
                        }
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

        private async Task Add(PhysicalClientApi physicalClient)
        {
            var id = await Api.PostAsync<PhysicalClientApi>(physicalClient, "PhysicalClient");
        }

        private async Task Edit(PhysicalClientApi physicalClient)
        {
            var id = await Api.PutAsync<PhysicalClientApi>(physicalClient, "PhysicalClient");
        }

        private async Task AddNewClient(ClientApi client)
        {
            var id = await Api.PostAsync<ClientApi>(client, "Client");
        }

        private async Task EditClient(ClientApi client)
        {
            var id = await Api.PutAsync<ClientApi>(client, "Client");
        }
    }
}
