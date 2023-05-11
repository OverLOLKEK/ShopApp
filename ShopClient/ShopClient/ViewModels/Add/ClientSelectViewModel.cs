using ModelsApi;
using ShopClient.Core;
using ShopClient.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopClient.ViewModels.Add
{
    public class ClientSelectViewModel : BaseViewModel
    {
        private string searchText = "";
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                Search();
            }
        }

        public List<string> SearchType { get; set; }
        private string selectedSearchType;
        public string SelectedSearchType
        {
            get => selectedSearchType;
            set
            {
                selectedSearchType = value;
                Search();
            }
        }

        private List<string> clientTypes;
        public List<string> ClientTypes
        {
            get => clientTypes;
            set
            {
                clientTypes = value;
                SignalChanged();
            }
        }

        private string selectedClientType;
        public string SelectedClientType
        {
            get => selectedClientType;
            set
            {
                selectedClientType = value;
                SignalChanged();
                Search();
            }
        }

        private List<ClientView> clientViews;
        public List<ClientView> ClientViews
        {
            get => clientViews;
            set
            {
                Set(ref clientViews, value);
                SignalChanged();
            }
        }
        private ClientView selectedClientView;
        public ClientView SelectedClientView
        {
            get => selectedClientView;
            set
            {
                Set(ref selectedClientView, value);
                SignalChanged();
            }
        }

        public List<LegalClientApi> LegalClients = new List<LegalClientApi>();
        public List<PhysicalClientApi> PhysicalClients = new List<PhysicalClientApi>();
        public List<ClientApi> Clients = new List<ClientApi>();
        public List<ClientView> FullClientViews = new List<ClientView>();
        List<ClientView> searchResult;

        public CustomCommand Save { get; set; }
        public CustomCommand Cancel { get; set; }

        public ClientSelectViewModel(OrderOutViewModel orderOutViewModel)
        {
            Task.Run(GetList); 
            SearchType = new List<string>();
            SearchType.AddRange(new string[] { "Наименование", "Телефон", "Адрес" });
            selectedSearchType = SearchType.First();

            ClientTypes = new List<string>();
            ClientTypes.AddRange(new string[] { "Все", "Физический", "Юридический" });
            selectedClientType = ClientTypes.First();

            Save = new CustomCommand(() =>
            {
                MessageBoxResult result = MessageBox.Show("Сохранить изменения?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (SelectedClientView == null || SelectedClientView.Id == 0)
                    {
                        MessageBox.Show("Клиент не выбран", "Ошибка",MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    try
                    {
                        orderOutViewModel.SelectedClient = Clients.First(s => s.Id == SelectedClientView.Id);
                        orderOutViewModel.ClientName = SelectedClientView.Title;
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
                MessageBoxResult result = MessageBox.Show("Отменить?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);
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


        private async Task GetList()
        {
            LegalClients = await Api.GetListAsync<List<LegalClientApi>>("LegalClient");
            PhysicalClients = await Api.GetListAsync<List<PhysicalClientApi>>("PhysicalClient");
            Clients = await Api.GetListAsync<List<ClientApi>>("Client");
            GenerateClientViews();
        }
        private void Search()
        {
            var search = SearchText.ToLower();
            if (SelectedClientType == null)
                SelectedClientType = ClientTypes.First();
            if (SelectedClientType == "Все")
            {
                if (SelectedSearchType == "Наименование")
                    searchResult = FullClientViews
                        .Where(c => c.Title.ToLower().Contains(search)).ToList();
                else if (SelectedSearchType == "Телефон")
                    searchResult = FullClientViews
                        .Where(c => c.Phone.ToString().Contains(search)).ToList();
                else if (SelectedSearchType == "Адрес")
                    searchResult = FullClientViews
                        .Where(c => c.Address.ToString().Contains(search)).ToList();
            }
            else
            {
                if (SelectedSearchType == "Наименование")
                    searchResult = FullClientViews
                        .Where(c => c.Title.ToLower().Contains(search) && c.Type.Contains(SelectedClientType)).ToList();
                else if (SelectedSearchType == "Телефон")
                    searchResult = FullClientViews
                        .Where(c => c.Phone.ToString().Contains(search) && c.Type.Contains(SelectedClientType)).ToList();
                else if (SelectedSearchType == "Адрес")
                    searchResult = FullClientViews
                        .Where(c => c.Address.ToString().Contains(search) && c.Type.Contains(SelectedClientType)).ToList();
            }
            UpdateList();
        }
        private void UpdateList()
        {
            ClientViews = searchResult;
        }
        public void CloseWin(object obj)
        {
            Window win = obj as Window;
            win.Close();
        }
        private void GenerateClientViews()
        {
            ClientViews = new List<ClientView>();
            foreach(ClientApi client in Clients)
            {
                var phycli = PhysicalClients.Find(s=>s.IdClient == client.Id);
                if (phycli != null)
                {
                    ClientViews.Add( new ClientView { Id = client.Id, Address = client.Address, Phone = client.Phone, Title = $"{phycli.LastName} " + $"{phycli.FirstName} " + $"{phycli.Patronymic}", Type = "Физический"  });
                }
                else
                {
                    var legalcli = LegalClients.Find(s => s.IdClient == client.Id);
                    if (legalcli != null)
                    {
                        ClientViews.Add(new ClientView { Id = client.Id, Address = client.Address, Phone = client.Phone, Title = $"{legalcli.Title} (ИНН:{legalcli.Inn})", Type = "Юридический" });
                    }
                   
                }
            }
            FullClientViews = ClientViews;
            Update();
        }
        private void Update()
        {
            //GetList();
            SignalChanged("ClientViews");
            //SelectedProductTypeFilter = ProductTypeFilter.Last();
        }
    }
}
