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
    public class AddLegalClientViewModel : BaseViewModel
    {
        private bool isSupplier;
        public bool IsSupplier 
        {
            get => isSupplier;
            set
            {
                isSupplier = value;
                SignalChanged();
            }
        }
        public LegalClientApi AddLegalClient { get; set; }
        public ClientApi AddClient { get; set; } = new ClientApi();

        public CustomCommand Save { get; set; }
        public CustomCommand Cancel { get; set; }

        public AddLegalClientViewModel(LegalClientApi legalClient)
        {
            if (legalClient == null)
            {
                AddLegalClient = new LegalClientApi { Client = AddClient, IsSupplier = 0 };
            }
            else
            {
                AddLegalClient = new LegalClientApi
                {
                    Id = legalClient.Id,
                    Client = legalClient.Client,
                    Title = legalClient.Title,
                    Inn = legalClient.Inn,
                    Email = legalClient.Email,
                    IsSupplier = legalClient.IsSupplier
                    
                };
            }
            if (AddLegalClient.IsSupplier == 0) 
                IsSupplier = false;
            else IsSupplier = true;

            Save = new CustomCommand(() =>
            {
                MessageBoxResult result = MessageBox.Show("Сохранить изменения?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        if(AddLegalClient.Inn == null || AddLegalClient.Title == null || AddLegalClient.Email == null || AddLegalClient.Client.Address == null || AddLegalClient.Client.Phone == null)
                        {
                            MessageBox.Show("Заполнены не все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        if (IsSupplier == false) AddLegalClient.IsSupplier = 0;
                        else AddLegalClient.IsSupplier = 1;

                        if (AddLegalClient.Id == 0)
                        {
                            Add(AddLegalClient);
                            AddNewClient(AddClient);
                        }
                        else
                        {
                            Edit(AddLegalClient);
                            EditClient(AddLegalClient.Client);
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

        private async Task Add(LegalClientApi legalClient)
        {
            var id = await Api.PostAsync<LegalClientApi>(legalClient, "LegalClient");
        }

        private async Task Edit(LegalClientApi legalClient)
        {
            var id = await Api.PutAsync<LegalClientApi>(legalClient, "LegalClient");
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
