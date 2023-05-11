using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using ModelsApi;
using ShopClient.Core;
using ShopClient.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopClient.ViewModels
{
   public class DashViewModel : BaseViewModel
    {
        private DateTime dateNow = DateTime.Now;
        public DateTime DateNow
        {
            get => dateNow;
            set
            {
                    dateNow = value;
                    Calculate();
                    SignalChanged();
            }
        }
        private DateTime dateCompare = DateTime.Now.AddMonths(-1);
        public DateTime DateCompare
        {
            get => dateCompare;
            set
            {
                dateCompare = value;
                Calculate();
                SignalChanged();
            }
        }
        private decimal profitNow;
        public decimal ProfitNow
        {
            get => profitNow;
            set
            {
                profitNow = value;
                SignalChanged();
            }
        }
        private decimal profitCompare;
        public decimal ProfitCompare
        {
            get => profitCompare;
            set
            {
                profitCompare = value;
                SignalChanged();
            }
        }
        private string profitDifference = "0";
        public string ProfitDifference
        {
            get => profitDifference;
            set
            {
                profitDifference = value;
                SignalChanged();
            }
        }
        private decimal averageCheckNow;
        public decimal AverageCheckNow
        {
            get => averageCheckNow;
            set
            {
                averageCheckNow = value;
                SignalChanged();
            }
        }
        private decimal averageCheckCompare;
        public decimal AverageCheckCompare
        {
            get => averageCheckCompare;
            set
            {
                averageCheckCompare = value;
                SignalChanged();
            }
        }
        private string averageCheckDifference = "0";
        public string AverageCheckDifference
        {
            get => averageCheckDifference;
            set
            {
                averageCheckDifference = value;
                SignalChanged();
            }
        }
        public string ProfitColor { get; set; } = "#000000";
        public string AverageCheckColor { get; set; } = "#000000";
        public string OrdersCountColor { get; set; } = "#000000";
        public string OrdersCountDifference { get; set; }
        public int OrdersCountNow { get; set; }
        public int OrdersCountCompare { get; set; }

        ChartValues<double> Counts = new ChartValues<double>();

        public SeriesCollection SeriesCollection { get; set; }
        private string[] labels;
        public string[] Labels
        {
            get => labels;
            set
            {
                labels = value;
                SignalChanged();
            }
        }
        public Func<double, string> Formatter { get; set; }

        public List<ActionTypeApi> ActionTypes = new List<ActionTypeApi>();
        public List<SaleTypeApi> SaleTypes = new List<SaleTypeApi>();
        public List<OrderApi> FullOrders = new List<OrderApi>();
        public List<ProductApi> FullProducts = new List<ProductApi>();
        public List<OrderOutApi> FullOrderOuts = new List<OrderOutApi>();
        public List<ProductOrderInApi> FullProductOrderIns = new List<ProductOrderInApi>();
        public Func<ChartPoint, string> PointLabel { get; set; }
        public int RetailValue { get; set; } = 0;
        public int WholesaleValue { get; set; } = 0;
        public SeriesCollection PieSeries { get; set; }

        public DashViewModel()
        {
            GetList();

            
        }

        private async Task GetList()
        {
            ActionTypes = await Api.GetListAsync<List<ActionTypeApi>>("ActionType");
            SaleTypes = await Api.GetListAsync<List<SaleTypeApi>>("SaleType");
            FullProducts = await Api.GetListAsync<List<ProductApi>>("Product");
            FullOrders = await Api.GetListAsync<List<OrderApi>>("Order");
            FullOrderOuts = await Api.GetListAsync<List<OrderOutApi>>("OrderOut");
            FullProductOrderIns = await Api.GetListAsync<List<ProductOrderInApi>>("ProductOrderIn");
            Calculate();
        }

        private void Calculate()
        {
            List<OrderOutApi> OrdersOutNow = new List<OrderOutApi>();
            List<OrderOutApi> OrdersOutCompare = new List<OrderOutApi>();

            foreach (var item in FullOrders)
            {
                var actionType = ActionTypes.First(s => s.Name == "Списание");
                if (item.IdActionType == actionType.Id)
                {
                 
                }
                else if (true)
                {
                    var date = (DateTime)item.Date;
                    if (date.Year == DateNow.Year && date.Month == DateNow.Month)
                    {
                        foreach (var orderOutApi in FullOrderOuts)
                        {
                            if (orderOutApi.IdOrder == item.Id && orderOutApi.Status != "Отменен")
                            {
                                OrdersOutNow.Add(orderOutApi);
                            }
                        }
                    }
                    if (date.Year == DateCompare.Year && date.Month == DateCompare.Month)
                    {
                        foreach (var orderOutApi in FullOrderOuts)
                        {
                            if (orderOutApi.IdOrder == item.Id && orderOutApi.Status != "Отменен")
                            {
                                OrdersOutCompare.Add(orderOutApi);
                            }
                        }
                    }
                }
            }
            DataSet(OrdersOutNow, OrdersOutCompare);
        }
        private void DataSet(List<OrderOutApi> orderOutsNow, List<OrderOutApi> orderOutsCompare)
        {
            ProfitNow = ProfitCalc(orderOutsNow);
            ProfitCompare = ProfitCalc(orderOutsCompare);
            if (ProfitCompare != 0)
            {
                var a = ProfitNow / (ProfitCompare / 100M);
                a -= 100;
                a = Math.Round(a, 2);
                if (a > 0)
                    ProfitColor = "Green";
                else
                    ProfitColor = "Red";
                ProfitDifference = a.ToString() + "%";
            }
            else
            {
                ProfitDifference = "0%";
                ProfitColor = "#000000";
            }

            OrdersCountNow = orderOutsNow.Count;
            OrdersCountCompare = orderOutsCompare.Count;
            if (OrdersCountCompare != 0)
            {
                var a = OrdersCountNow / (OrdersCountCompare / 100M);
                a -= 100;
                a = Math.Round(a, 2);
                if (a > 0)
                    OrdersCountColor = "Green";
                else
                    OrdersCountColor = "Red";
                OrdersCountDifference = a.ToString() + "%";
            }
            else
            {
                OrdersCountDifference = "0%";
                OrdersCountColor = "#000000";
            }

            if (orderOutsNow.Count != 0)
            {
                var res = ProfitNow / orderOutsNow.Count;
                AverageCheckNow = Math.Round(res, 4);
            }
            else
                AverageCheckNow = 0;

            if (orderOutsCompare.Count != 0)
            {
                var res = ProfitCompare / orderOutsCompare.Count;
                AverageCheckCompare = Math.Round(res, 4);
            }
            else
                AverageCheckCompare = 0;
            if (AverageCheckCompare != 0)
            {
                var b = AverageCheckNow / (AverageCheckCompare / 100M);
                b -= 100;
                b = Math.Round(b, 2);
                if (b > 0)
                    AverageCheckColor = "Green";
                else
                    AverageCheckColor = "Red";
                AverageCheckDifference = b.ToString() + "%";
            }
                else
            {
                AverageCheckDifference = "0%";
                AverageCheckColor = "#000000";
            }
            GeneratePieChart(orderOutsNow);
            GenerateChart(orderOutsNow);
            Update();
        }
        private decimal ProfitCalc(List<OrderOutApi> list)
        {
            decimal profit = 0;
            foreach (var orderOut in list)
            {
                foreach (var productOrderOut in orderOut.ProductOrderOuts)
                {
                    var productOrderIn = FullProductOrderIns.First(s => s.Id == productOrderOut.IdProductOrderIn);
                    profit += (decimal)((decimal)(productOrderOut.Price - productOrderOut.Discount - productOrderIn.Price) * productOrderOut.Count);
                }
            }
            return profit;
        }
        private void GenerateChart(List<OrderOutApi> orderOuts)
        {
            Counts = new ChartValues<double>();
            List<ChartProduct> chartProducts = new List<ChartProduct>();
            foreach (OrderOutApi orderOut in orderOuts)
            {
                foreach (ProductOrderOutApi productOrderOut in orderOut.ProductOrderOuts)
                {
                    var orderIn = FullProductOrderIns.First(s => s.Id == productOrderOut.IdProductOrderIn);
                    var product = FullProducts.First(s => s.Id == orderIn.IdProduct);
                    if (chartProducts.Count == 0)
                        chartProducts.Add(new ChartProduct { Product = product, Count = (int)productOrderOut.Count });
                    else
                    {
                        var c = chartProducts.Find(s => s.Product == product);
                        if (c != null)
                        {
                            c.Count += (int)productOrderOut.Count;
                        }
                        else
                            chartProducts.Add(new ChartProduct { Product = product, Count = (int)productOrderOut.Count });
                    }
                }
            }
            List<ChartProduct> thisChartProducts = new List<ChartProduct>();
            for (int i = 0; i < 5; i++)
            {
                ChartProduct prod = chartProducts.Find(p => p.Count == chartProducts.Max(count=>count.Count));
                if (prod == null)
                {
                    chartProducts.Remove(prod);
                    break;
                }
                thisChartProducts.Add(prod);
                chartProducts.Remove(prod);
            }
            foreach (var item in thisChartProducts)
            {
                Counts.Add((double)item.Count);
            }
            string[] products = new string[thisChartProducts.Count];
            for (int i = 0; i < thisChartProducts.Count; i++)
            {
                products[i] = thisChartProducts[i].Product.Title;
            }
            SeriesCollection = new SeriesCollection
            {
                new RowSeries
                {
                    DataLabels = true,
                    Title = "Товары",
                    Values = Counts
                }
            };
            Labels = products;
            Formatter = value => value.ToString("N");
            SignalChanged("SeriesCollection");
            SignalChanged("Labels");
            SignalChanged("Formatter");
        }
        private void GeneratePieChart(List<OrderOutApi> orderOuts)
        {
            WholesaleValue = 0;
            RetailValue = 0;

            foreach (var item in orderOuts)
            {
                SaleTypeApi sale = SaleTypes.First(s => s.Id == item.IdSaleType);
                if (sale.Title == "Оптовая")
                    WholesaleValue++;
                else
                    RetailValue++;
            }
            PointLabel = chartPoint =>
              string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            PieSeries = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Розничные",
                    Values = new ChartValues<ObservableValue> {new ObservableValue(RetailValue) },
                    DataLabels = true,
                    LabelPoint = PointLabel,
                    FontSize = 16
                },
                  new PieSeries
                {
                    Title = "Оптовые",
                    Values = new ChartValues<ObservableValue> {new ObservableValue(WholesaleValue) },
                    DataLabels = true,
                    LabelPoint = PointLabel,
                      FontSize = 16
                },
            };
            SignalChanged("PieSeries");
            SignalChanged("WholesaleValue");
            SignalChanged("RetailValue");
            SignalChanged("PointLabel");
        }
        private void Update()
        {
            SignalChanged("AverageCheckColor");
            SignalChanged("ProfitColor");
            SignalChanged("ProfitNow");
            SignalChanged("ProfitCompare");
            SignalChanged("ProfitDifference");
            SignalChanged("AverageCheckNow");
            SignalChanged("AverageCheckCompare");
            SignalChanged("AverageCheckDifference");
            SignalChanged("OrdersCountDifference");
            SignalChanged("OrdersCountNow");
            SignalChanged("OrdersCountCompare");
            SignalChanged("OrdersCountColor");
        }
    }
}
