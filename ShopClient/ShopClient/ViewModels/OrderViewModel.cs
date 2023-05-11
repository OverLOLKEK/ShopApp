using ModelsApi;
using ShopClient.Core;
using ShopClient.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopClient.ViewModels
{
    public class OrderViewModel : BaseViewModel
    {
        public int ToggleSort = 0;
        internal void Sort(string p)
        {
            if (p == "Date")
            {
                var forSort = searchResult.Where(s => s.Date != null).ToList();
                var notSort = searchResult.Where(s => s.Date == null);
                if (ToggleSort == 0)  forSort.Sort((x, y) => y.Date.Value.CompareTo(x.Date));
                else forSort.Sort((x, y) => x.Date.Value.CompareTo(y.Date));
                searchResult = forSort;
                searchResult.AddRange(notSort);
                
            }
            if (ToggleSort == 1) ToggleSort = 0;
            else ToggleSort = 1;
            paginationPageIndex = 0;
            Pagination();
        }

        public List<string> ViewCountRows { get; set; }

        public string SelectedViewCountRows
        {
            get => selectedViewCountRows;
            set
            {
                selectedViewCountRows = value;
                paginationPageIndex = 0;
                Pagination();
            }
        }

        private List<ActionTypeApi> actionTypeFilter;
        public List<ActionTypeApi> ActionTypeFilter
        {
            get => actionTypeFilter;
            set
            {
                actionTypeFilter = value;
                SignalChanged();
            }
        }

        private ActionTypeApi selectedActionTypeFilter;
        public ActionTypeApi SelectedActionTypeFilter
        {
            get => selectedActionTypeFilter;
            set
            {
                selectedActionTypeFilter = value;
                SignalChanged();
                Search();
            }
        }

        public string SearchCountRows
        {
            get => searchCountRows;
            set
            {
                searchCountRows = value;
                SignalChanged();
            }
        }
        private string pages;
        public string Pages
        {
            get => pages;
            set
            {
                pages = value;
                SignalChanged();
            }
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

        private List<OrderApi> orders;
        public List<OrderApi> Orders
        {
            get => orders;
            set
            {
                Set(ref orders, value);
                SignalChanged();
            }
        }
        private List<ActionTypeApi> actionTypes;
        public List<ActionTypeApi> ActionTypes
        {
            get => actionTypes;
            set
            {
                Set(ref actionTypes, value);
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
        private OrderApi selectedOrder = new OrderApi { };
        public OrderApi SelectedOrder
        {
            get => selectedOrder;
            set
            {
                selectedOrder = value;
                SignalChanged();
            }
        }

        public CustomCommand BackPage { get; set; }
        public CustomCommand ForwardPage { get; set; }
        public CustomCommand OpenOrderDetails { get; set; }

        private List<OrderApi> FullOrders = new List<OrderApi>();
        public int rows = 0;
        public int CountPages = 0;
        List<OrderApi> searchResult;
        int paginationPageIndex = 0;
        private string searchCountRows;
        private string selectedViewCountRows;
        public OrderViewModel()
        {
            Clients = new List<ClientApi>();
            ActionTypes = new List<ActionTypeApi>();
            Orders = new List<OrderApi>();
            GetList();

            ViewCountRows = new List<string>();
            ViewCountRows.AddRange(new string[] { "10", "50", "все" });
            selectedViewCountRows = ViewCountRows.First();

            SearchType = new List<string>();
            SearchType.AddRange(new string[] { "Клиент", "Дата", "№ Заказа" });
            selectedSearchType = SearchType.First();

            BackPage = new CustomCommand(() =>
            {
                if (searchResult == null)
                    return;
                if (paginationPageIndex > 0)
                    paginationPageIndex--;
                Pagination();
            });

            ForwardPage = new CustomCommand(() =>
            {
                if (searchResult == null)
                    return;
                int.TryParse(SelectedViewCountRows, out int rowsOnPage);
                if (rowsOnPage == 0)
                    return;
                int countPage = searchResult.Count() / rowsOnPage;
                CountPages = countPage;
                if (searchResult.Count() % rowsOnPage != 0)
                    countPage++;
                if (countPage > paginationPageIndex + 1)
                    paginationPageIndex++;
                Pagination();

            });
            OpenOrderDetails = new CustomCommand(() =>
            {
                if (SelectedOrder == null || SelectedOrder.Id == 0) return;
                if(SelectedOrder.ActionType.Name == "Поступление")
                {
                    OrderInDetails orderInDetails = new OrderInDetails(SelectedOrder);
                    orderInDetails.ShowDialog();
                }
            if (SelectedOrder.ActionType.Name == "Продажа")
            {
                    OrderOutDetails orderOutDetails = new OrderOutDetails(SelectedOrder);
                    orderOutDetails.ShowDialog();
                }
                if (SelectedOrder.ActionType.Name == "Списание")
            {
                WriteOffDetails writeOffDetails = new WriteOffDetails(SelectedOrder);
                writeOffDetails.ShowDialog();
                }
                Thread.Sleep(200);
                Update();
            });
           
        }

        private void InitPagination()
        {
            SearchCountRows = $"Найдено записей: {searchResult.Count} из {FullOrders.Count}";
            paginationPageIndex = 0;
        }

        private void Pagination()
        {
            int rowsOnPage = 0;
            if (!int.TryParse(SelectedViewCountRows, out rowsOnPage))
            {
                Orders = searchResult;
            }
            else
            {
                Orders = searchResult.Skip(rowsOnPage * paginationPageIndex)
                    .Take(rowsOnPage).ToList();

            }

            int.TryParse(SelectedViewCountRows, out rows);
            if (rows == 0)
                rows = FullOrders.Count;
            CountPages = (searchResult.Count() - 1) / rows;
            Pages = $"{paginationPageIndex + 1}/{CountPages + 1}";
        }

        private void Search()
        {
            var search = SearchText.ToLower();
            if (SelectedActionTypeFilter == null)
                SelectedActionTypeFilter = ActionTypeFilter.Last();
            if (SelectedActionTypeFilter.Name == "Все типы")
            {
                if (SelectedSearchType == "Клиент")
                    searchResult = FullOrders
                        .Where(c => c.Client.Phone.ToLower().Contains(search)).ToList();
                else if(SelectedSearchType == "Дата")
                    searchResult = FullOrders
                     .Where(c => c.Date.ToString().ToLower().Contains(search)).ToList();
                else if (SelectedSearchType == "№ Заказа")
                    searchResult = FullOrders
                     .Where(c => c.Id.ToString().ToLower().Contains(search)).ToList();
            }
            else
            {
                if (SelectedSearchType == "Клиент")
                    searchResult = FullOrders
                        .Where(c => c.Client.Phone.ToLower().Contains(search) && c.ActionType.Name.Contains(SelectedActionTypeFilter.Name)).ToList();
                else if (SelectedSearchType == "Дата")
                    searchResult = FullOrders
                     .Where(c => c.Date.ToString().ToLower().Contains(search) && c.ActionType.Name.Contains(SelectedActionTypeFilter.Name)).ToList();
                else if (SelectedSearchType == "№ Заказа")
                    searchResult = FullOrders
                     .Where(c => c.Id.ToString().ToLower().Contains(search) && c.ActionType.Name.Contains(SelectedActionTypeFilter.Name)).ToList();
            }
            InitPagination();
            Pagination();
        }

        private async Task GetList()
        {
            Clients = await Api.GetListAsync<List<ClientApi>>("Client");
            ActionTypes = await Api.GetListAsync<List<ActionTypeApi>>("ActionType");
            Orders = await Api.GetListAsync<List<OrderApi>>("Order");    
            var x = Orders;
            Orders = x.OrderByDescending(s => s.Date).ToList();
            FullOrders = Orders;
            foreach (OrderApi order in Orders)
            {
                order.Client = Clients.First(s => s.Id == order.IdClient);
                order.ActionType = ActionTypes.First(s => s.Id == order.IdActionType);
            }

            ActionTypeFilter = await Api.GetListAsync<List<ActionTypeApi>>("ActionType");
            ActionTypeFilter.Add(new ActionTypeApi { Name = "Все типы" });
            SelectedActionTypeFilter = ActionTypeFilter.Last();

            InitPagination();
            Pagination();
        }
        private void Update()
        {
            GetList();
            SignalChanged("Clients");
            SignalChanged("ActionTypes");
            SignalChanged("Orders");
            SelectedActionTypeFilter = ActionTypeFilter.Last();
        }
    }
}
