
using ModelsApi;
using ShopClient.Core;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopClient.ViewModels
{
   public class ReportViewModel : BaseViewModel
    {
        private DateTime dateStart = DateTime.Now;
        public DateTime DateStart
        {
            get => dateStart;
            set
            {
                dateStart = value;
                SignalChanged();
            }
        }
        private DateTime dateFinish = DateTime.Now;
        public DateTime DateFinish
        {
            get => dateFinish;
            set
            {
                dateFinish = value;
                SignalChanged();
            }
        }


        private bool isRetail = true;
        public bool IsRetail
        {
            get => isRetail;
            set
            {
                isRetail = value;
                SignalChanged();
            }
        }
        private bool isWholesale = true;
        public bool IsWholesale
        {
            get => isWholesale;
            set
            {
                isWholesale = value;
                SignalChanged();
            }
        }

        private string productArticle = "";
        public string ProductArticle
        {
            get => productArticle;
            set
            {
                productArticle = value;
                SignalChanged();
            }
        }



        public List<OrderApi> FullOrders = new List<OrderApi>();
        public List<SaleTypeApi> SaleTypes = new List<SaleTypeApi>();
        public List<OrderOutApi> FullOrderOuts = new List<OrderOutApi>();
        public List<ProductOrderInApi> ProductOrderIns = new List<ProductOrderInApi>();
        public List<OrderOutApi> ThisOrderOuts = new List<OrderOutApi>();
        public List<ProductOrderOutApi> ThisProductOrderOuts = new List<ProductOrderOutApi>();
        public List<ProductApi> Products = new List<ProductApi>(); 
        public List<ActionTypeApi> ActionTypes = new List<ActionTypeApi>();

        public CustomCommand MakeReport { get; set; }

        public ReportViewModel()
        {
            GetList();

            MakeReport = new CustomCommand(() =>
            {
                int res;
                bool isConvert;
                isConvert = Int32.TryParse(ProductArticle, out res);
                if (IsRetail == false && IsWholesale == false)
                {
                    MessageBox.Show("Необходимо выбрать хотя бы один тип продажи", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (ProductArticle.Length != 0 && !isConvert)
                {
                    MessageBox.Show("Неверный Артикул", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (ProductArticle.Length != 0)
                {
                    var valid = 0;
                    foreach (var item in Products)
                    {
                        if (item.Article == int.Parse(ProductArticle))
                        {
                            valid = 1;
                        }
                    }
                    if (valid == 0)
                    {
                        MessageBox.Show("Товара с таким артикулом не существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
               
                GenerateReportList();
            });
        }

        private void GenerateReportList()
        {
            ThisProductOrderOuts.Clear();
            ThisOrderOuts.Clear();
            var RetaileSale = SaleTypes.First(s => s.Title == "Розничная");
            var WholesaleSale = SaleTypes.First(s => s.Title == "Оптовая");
            var saleActionType = ActionTypes.First(s=>s.Name == "Продажа");
            var Orders = FullOrders.Where(s => s.IdActionType == saleActionType.Id).ToList();

            var validOrders = FullOrderOuts.Where(s => s.Status != "Отменен").ToList();
            foreach (OrderOutApi orderOut in validOrders)
            {
                var order = FullOrders.First(s=>s.Id == orderOut.IdOrder);
                if (Orders.Contains(order))
                {
                     if (order.Date >= DateStart && order.Date <= DateFinish)
                {
                        if (IsRetail == true && orderOut.IdSaleType == RetaileSale.Id)
                        {
                            ThisOrderOuts.Add(orderOut);
                        }
                        if (IsWholesale == true && orderOut.IdSaleType == WholesaleSale.Id)
                        {
                            ThisOrderOuts.Add(orderOut);
                        }
                    }
                }
            }

            foreach (OrderOutApi orderOut in ThisOrderOuts)
            {
                foreach (ProductOrderOutApi productOrderOut in orderOut.ProductOrderOuts)
                {
                    if (ProductArticle.ToString().Length != 0)
                    {
                        var productOrderIn = ProductOrderIns.First(s=>s.Id == productOrderOut.IdProductOrderIn);
                        if (productOrderIn.Product.Article == int.Parse(ProductArticle))
                        {
                            ThisProductOrderOuts.Add(productOrderOut);
                        }
                    }
                    else
                    {
                        ThisProductOrderOuts.Add(productOrderOut);
                    }
                }
            }
            GenerateReport(ThisProductOrderOuts);
            //GenerateReportPdf(ThisProductOrderOuts);
        }
        private void GenerateReportPdf(List<ProductOrderOutApi> productOrderOuts)
        {
           
        }
        private void GenerateReport(List<ProductOrderOutApi> productOrderOuts)
        {
            var TotalCount = 0;
            decimal TotalPurchase = 0;
            decimal TotalPriceWithDiscount = 0;
            decimal TotalProfit = 0;
            var workbook = new Workbook();
            var sheet = workbook.Worksheets[0];
            sheet.Range["A1"].Value = "Отчет по продажам";
            sheet.Range["A1:B1"].Merge();
            sheet.Range["D1"].Value = "С";
            sheet.Range["E1"].Value = DateStart.ToShortDateString();
            sheet.Range["F1"].Value = "ПО";
            sheet.Range["G1"].Value = DateFinish.ToShortDateString();  
            sheet.Range["D1:G1"].Style.HorizontalAlignment = HorizontalAlignType.Center;

            sheet.Range["A3"].Value = "Артикул";
            sheet.Range["B3"].Value = "Дата";
            sheet.Range["C3"].Value = "Наименование";
            sheet.Range["D3"].Value = "Кол-во";
            sheet.Range["E3"].Value = "Тип продажи";
            sheet.Range["F3"].Value = "Закупочная цена";
            sheet.Range["G3"].Value = "Цена продажи";
            sheet.Range["H3"].Value = "Cкидка";
            sheet.Range["I3"].Value = "Сумма со скидкой";
            sheet.Range["J3"].Value = "Прибыль";

            var index = 4;
            foreach (ProductOrderOutApi productOrderOut in productOrderOuts)
            {
                var productOrderIn = ProductOrderIns.First(s => s.Id == productOrderOut.IdProductOrderIn);
                var orderOut = FullOrderOuts.First(s=>s.Id == productOrderOut.IdOrderOut);
                var order = FullOrders.First(s=>s.Id == orderOut.IdOrder);
                var saleType = SaleTypes.First(s => s.Id == orderOut.IdSaleType);
                sheet.Range[$"A{index}"].Value = productOrderIn.Product.Article.ToString();
                sheet.Range[$"B{index}"].Value = order.Date.ToString().Substring(0, order.Date.ToString().Length - 8);
                sheet.Range[$"C{index}"].Value = productOrderIn.Product.Title.ToString();
                sheet.Range[$"D{index}"].Value = productOrderOut.Count.ToString();
                TotalCount += (int)productOrderOut.Count;
                sheet.Range[$"E{index}"].Value = saleType.Title;
                sheet.Range[$"F{index}"].Value = productOrderIn.Price.ToString();
                sheet.Range[$"F{index}"].NumberFormat = "0.00 ₽";
                TotalPurchase += (decimal)(productOrderIn.Price * productOrderOut.Count);
                sheet.Range[$"G{index}"].Value = productOrderOut.Price.ToString();
                sheet.Range[$"G{index}"].NumberFormat = "0.00 ₽";
                sheet.Range[$"H{index}"].Value = productOrderOut.Discount.ToString();
                sheet.Range[$"H{index}"].NumberFormat = "0.00 ₽";
                sheet.Range[$"I{index}"].Value = ((productOrderOut.Price - productOrderOut.Discount) * productOrderOut.Count).ToString();
                sheet.Range[$"I{index}"].NumberFormat = "0.00 ₽";
                TotalPriceWithDiscount += (decimal)((productOrderOut.Price - productOrderOut.Discount) * productOrderOut.Count);
                var a = (decimal)((productOrderOut.Price - productOrderOut.Discount - productOrderIn.Price) * productOrderOut.Count);
                sheet.Range[$"J{index}"].Value = a.ToString();
                sheet.Range[$"J{index}"].NumberFormat = "0.00 ₽";
                TotalProfit += a;
                index++;
            }

            sheet.Range[$"A{index}"].Value = "Всего";
            sheet.Range[$"A{index}:C{index}"].Merge();
            sheet.Range[$"D{index}"].Value = TotalCount.ToString();
            sheet.Range[$"E{index}"].Style.Color = Color.Gray;
            sheet.Range[$"F{index}"].Value = TotalPurchase.ToString();
            sheet.Range[$"F{index}"].NumberFormat = "0.00 ₽";
            sheet.Range[$"G{index}"].Style.Color = Color.Gray;
            sheet.Range[$"H{index}"].Style.Color = Color.Gray;
            sheet.Range[$"I{index}"].Value = TotalPriceWithDiscount.ToString();
            sheet.Range[$"I{index}"].NumberFormat = "0.00 ₽";
            sheet.Range[$"J{index}"].Value = TotalProfit.ToString();
            sheet.Range[$"J{index}"].NumberFormat = "0.00 ₽";

            sheet.Range[$"A3:J{index}"].BorderInside(LineStyleType.Thin);
            sheet.Range[$"A3:J{index}"].BorderAround(LineStyleType.Medium);

            sheet.AllocatedRange.AutoFitColumns();

            workbook.SaveToFile("text.xls");
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(Environment.CurrentDirectory + "/text.xls")
            {
                UseShellExecute = true
            };
            p.Start();
        }
        private async Task GetList()
        {
            FullOrders = await Api.GetListAsync<List<OrderApi>>("Order");
            FullOrderOuts = await Api.GetListAsync<List<OrderOutApi>>("OrderOut");
            ProductOrderIns = await Api.GetListAsync<List<ProductOrderInApi>>("ProductOrderIn");
            SaleTypes = await Api.GetListAsync<List<SaleTypeApi>>("SaleType");
            Products = await Api.GetListAsync<List<ProductApi>>("Product");
            ActionTypes = await Api.GetListAsync<List<ActionTypeApi>>("ActionType");
        }
   }
}
