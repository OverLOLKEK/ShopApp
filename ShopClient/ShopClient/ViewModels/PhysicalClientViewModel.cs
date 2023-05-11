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
    public class PhysicalClientViewModel : BaseViewModel
    {
        public int ToggleSort = 0;
        internal void Sort(string p)
        {
            if (p == "Count")
            {
                var forSort = searchResult.Where(s => s.Client.OrdersCount != 0).ToList();
                var notSort = searchResult.Where(s => s.Client.OrdersCount == 0);
                if (ToggleSort == 0) forSort.Sort((x, y) => y.Client.OrdersCount.CompareTo(x.Client.OrdersCount));
                else forSort.Sort((x, y) => x.Client.OrdersCount.CompareTo(y.Client.OrdersCount));
                searchResult = forSort;
                searchResult.AddRange(notSort);
                UpdateList();
            }
            if (ToggleSort == 1) ToggleSort = 0;
            else ToggleSort = 1;
        }

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
        private List<PhysicalClientApi> physicalClients;
        public List<PhysicalClientApi> PhysicalClients
        {
            get => physicalClients;
            set
            {  
                Set(ref physicalClients, value);
                SignalChanged();
            }
        }
        private List<ClientApi> clients;
        public List<ClientApi> Clients
        {
            get => clients;
            set
            { 
                Set(ref clients, value);
                SignalChanged();
              
            }
        }

        private PhysicalClientApi selectedPhysicalClient= new PhysicalClientApi { };
        public PhysicalClientApi SelectedPhysicalClient
        {
            get => selectedPhysicalClient;
            set
            {
                selectedPhysicalClient = value;
                SignalChanged();
            }
        }

        public CustomCommand AddPhysicalClient { get; set; }
        public CustomCommand EditPhysicalClient { get; set; }
        public CustomCommand DeletePhysicalClient { get; set; }

        private List<PhysicalClientApi> FullPhysicalClients = new List<PhysicalClientApi>();
        List<PhysicalClientApi> searchResult;
        List<OrderApi> Orders;

        public PhysicalClientViewModel()
        {    
            Clients = new List<ClientApi>();
            PhysicalClients = new List<PhysicalClientApi>();
            GetList();

            SearchType = new List<string>();
            SearchType.AddRange(new string[] { "Фамилия", "Адрес", "Телефон" });
            selectedSearchType = SearchType.First();

            AddPhysicalClient = new CustomCommand(() =>
            {
                AddPhysicalClient addPhysicalClient = new AddPhysicalClient();
                addPhysicalClient.ShowDialog();
                Thread.Sleep(200);
                Update();
            });
            EditPhysicalClient = new CustomCommand(() =>
            {
                if (SelectedPhysicalClient == null || SelectedPhysicalClient.Id == 0) return;
                AddPhysicalClient addPhysicalClient = new AddPhysicalClient(SelectedPhysicalClient);
                addPhysicalClient.ShowDialog();
                Thread.Sleep(200);
                Update();
            });
            DeletePhysicalClient = new CustomCommand(() =>
            {
                MessageBoxResult result = MessageBox.Show("Удалить запись?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (SelectedPhysicalClient == null || SelectedPhysicalClient.Id == 0) return;
                    try
                    {
                        Delete(SelectedPhysicalClient);
                        Update();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    };
                }
                else return;

            });
        }
        private void Search()
        {
            var search = SearchText.ToLower();

            if (SelectedSearchType == "Фамилия")
                searchResult = FullPhysicalClients
                    .Where(c => c.LastName.ToLower().Contains(search)).ToList();
            else if (SelectedSearchType == "Адрес")
                searchResult = FullPhysicalClients
                    .Where(c => c.Client.Address.ToString().Contains(search)).ToList();
            else if (SelectedSearchType == "Телефон")
                searchResult = FullPhysicalClients
                    .Where(c => c.Client.Phone.ToString().Contains(search)).ToList();
            UpdateList();
        }

        private void UpdateList()
        {

            PhysicalClients = searchResult;
        }
        private async Task Delete(PhysicalClientApi physicalClient)
        {
            var res = await Api.DeleteAsync<PhysicalClientApi>(physicalClient, "PhysicalClient");
            GetList();
        } 
        private async Task GetList()
        {
            PhysicalClients = await Api.GetListAsync<List<PhysicalClientApi>>("PhysicalClient");
            Orders = await Api.GetListAsync<List<OrderApi>>("Order");
            foreach(PhysicalClientApi physicalClientApi in PhysicalClients)
            {
                physicalClientApi.Client.OrdersCount = Orders.FindAll(s => s.IdClient == physicalClientApi.IdClient).ToList().Count;
            }
            FullPhysicalClients = PhysicalClients;
            Search();
        }

        private void Update()
        {
            
            GetList();
            SignalChanged("Clients");
            SignalChanged("PhysicalClients");
        }
    }
}
