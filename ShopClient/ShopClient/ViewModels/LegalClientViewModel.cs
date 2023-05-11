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
   public class LegalClientViewModel : BaseViewModel
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
        private List<LegalClientApi> legalClients;
        public List<LegalClientApi> LegalClients
        {
            get => legalClients;
            set
            {
                Set(ref legalClients, value);
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

        private LegalClientApi selectedLegalClient = new LegalClientApi { };
        public LegalClientApi SelectedLegalClient
        {
            get => selectedLegalClient;
            set
            {
                selectedLegalClient = value;
                SignalChanged();
            }
        }
        private bool isOnlySuppliers = false;
        public bool IsOnlySuppliers
        {
            get => isOnlySuppliers;
            set
            {
                if (value != isOnlySuppliers)
                {
                    isOnlySuppliers = value;
                    SignalChanged();
                    Search();

                }
            }
        }
        

        public CustomCommand AddLegalClient { get; set; }
        public CustomCommand EditLegalClient { get; set; }
        public CustomCommand DeleteLegalClient { get; set; }

        private List<LegalClientApi> FullLegalClients = new List<LegalClientApi>();
        List<LegalClientApi> searchResult;
        public   List<OrderApi> Orders;

        public LegalClientViewModel()
        {
            Clients = new List<ClientApi>();
            LegalClients = new List<LegalClientApi>();
            GetList();

            SearchType = new List<string>();
            SearchType.AddRange(new string[] {"Наименование","ИНН","Телефон" });
            selectedSearchType = SearchType.First();


            AddLegalClient = new CustomCommand(() =>
            {
                AddLegalClient addLegalClient = new();
                addLegalClient.ShowDialog();
                Thread.Sleep(200);
                Update();
            });
            EditLegalClient = new CustomCommand(() =>
            {
            if (SelectedLegalClient == null || SelectedLegalClient.Id == 0) return;
                AddLegalClient addLegalClient = new AddLegalClient(SelectedLegalClient);
                addLegalClient.ShowDialog();
                Thread.Sleep(200);
                Update();

            });
            DeleteLegalClient = new CustomCommand(() =>
            {
                MessageBoxResult result = MessageBox.Show("Удалить запись?", "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (SelectedLegalClient == null || SelectedLegalClient.Id == 0) return;
                    try
                    {
                        Delete(SelectedLegalClient);
                        Update();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    };
                }
                else return;

            });
            UpdateList();
        }
        private void Search()
        {
            var search = SearchText.ToLower();

                if (SelectedSearchType == "Наименование")
                    searchResult = FullLegalClients
                        .Where(c => c.Title.ToLower().Contains(search)).ToList();
                else if (SelectedSearchType == "ИНН")
                    searchResult = FullLegalClients
                        .Where(c => c.Inn.ToString().Contains(search)).ToList();
            else if (SelectedSearchType == "Телефон")
                searchResult = FullLegalClients
                    .Where(c => c.Client.Phone.ToString().Contains(search)).ToList();
            var a = searchResult;
            if (IsOnlySuppliers == true)
            {
                searchResult = a.Where(c=>c.IsSupplier == 1).ToList();
            }
            UpdateList();
        }

        private void UpdateList()
        {
            LegalClients = searchResult;

        }

        private async Task Delete(LegalClientApi legalClient)
        {
            var res = await Api.DeleteAsync<LegalClientApi>(legalClient, "LegalClient");
            Update();
        }
        private async Task GetList()
        {
            LegalClients = await Api.GetListAsync<List<LegalClientApi>>("LegalClient");
            Orders = await Api.GetListAsync<List<OrderApi>>("Order");
            foreach (LegalClientApi legal in LegalClients)
            {
                legal.Client.OrdersCount = Orders.FindAll(s => s.IdClient == legal.IdClient).ToList().Count;
            }
            FullLegalClients = LegalClients;
            Search();
        }
        

        private void Update()
        {
           
            SignalChanged("Clients");
            GetList();
            SignalChanged("LegalClients");
        }
    }
}
