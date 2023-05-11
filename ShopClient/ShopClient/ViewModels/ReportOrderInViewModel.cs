using ModelsApi;
using ShopClient.Core;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopClient.ViewModels
{
    public class ReportOrderInViewModel : BaseViewModel
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
        public List<ProductOrderInApi> FullOrderIns = new List<ProductOrderInApi>();
        public List<ProductOrderInApi> ThisOrderIns = new List<ProductOrderInApi>();
        public List<ProductOrderOutApi> ThisProductOrderOuts = new List<ProductOrderOutApi>();
        public List<ProductApi> Products = new List<ProductApi>();
        public List<ActionTypeApi> ActionTypes = new List<ActionTypeApi>();

        public CustomCommand MakeReport { get; set; }

        public ReportOrderInViewModel()
        {
            GetList();

            MakeReport = new CustomCommand(() =>
            {
                int res;
                bool isConvert;
                isConvert = Int32.TryParse(ProductArticle, out res);
               
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
            ThisOrderIns.Clear();
            var saleActionType = ActionTypes.First(s => s.Name == "Поступление");
            var Orders = FullOrders.Where(s => s.IdActionType == saleActionType.Id).ToList();

            foreach (ProductOrderInApi oredrIn in FullOrderIns)
            {
                if (ProductArticle.ToString().Length != 0)
                {
                     if (oredrIn.Product.Article == int.Parse(ProductArticle))
                     {
                         ThisOrderIns.Add(oredrIn);
                     }
                }
                else
                    {
                        ThisOrderIns.Add(oredrIn);
                    }
            }
            foreach (var item in ThisOrderIns)
            {
                item.Order = FullOrders.Find(s => s.Id == item.IdOrder);
            }
            GenerateReport(ThisOrderIns);
            //GenerateReportPdf(ThisProductOrderOuts);
        }
        private void GenerateReportPdf(List<ProductOrderOutApi> productOrderOuts)
        {

        }
        private void GenerateReport(List<ProductOrderInApi> productOrderIns)
        {
            productOrderIns = productOrderIns.OrderBy(s => s.Order.Date).ToList();
            var TotalCount = 0;
            decimal TotalPurchase = 0;
            var TotalRemains = 0;

            var workbook = new Workbook();
            var sheet = workbook.Worksheets[0];
            sheet.Range["A1"].Value = "Отчет по поставкам";
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
            sheet.Range["E3"].Value = "Остаток";
            sheet.Range["F3"].Value = "Закупочная цена";
            sheet.Range["G3"].Value = "Сумма";

            var index = 4;
            foreach (ProductOrderInApi productOrderIn in productOrderIns)
            {
                var order = FullOrders.First(s => s.Id == productOrderIn.IdOrder);
                sheet.Range[$"A{index}"].Value = productOrderIn.Product.Article.ToString();
                sheet.Range[$"B{index}"].Value = order.Date.ToString().Substring(0, order.Date.ToString().Length - 8);
                sheet.Range[$"C{index}"].Value = productOrderIn.Product.Title.ToString();
                sheet.Range[$"D{index}"].Value = productOrderIn.Count.ToString();
                TotalCount += (int)productOrderIn.Count;
                sheet.Range[$"E{index}"].Value = productOrderIn.Remains.ToString();
                TotalRemains += (int)productOrderIn.Remains;
                sheet.Range[$"F{index}"].Value = productOrderIn.Price.ToString();
                sheet.Range[$"F{index}"].NumberFormat = "0.00 ₽";
                TotalPurchase += (decimal)(productOrderIn.Price * productOrderIn.Count);
                sheet.Range[$"G{index}"].Value = (productOrderIn.Price * productOrderIn.Count).ToString();
                sheet.Range[$"G{index}"].NumberFormat = "0.00 ₽";
                index++;
            }

            sheet.Range[$"A{index}"].Value = "Всего";
            sheet.Range[$"A{index}:C{index}"].Merge();
            sheet.Range[$"D{index}"].Value = TotalCount.ToString();
            sheet.Range[$"E{index}"].Value = TotalRemains.ToString();    
            sheet.Range[$"F{index}"].Style.Color = Color.Gray;
            sheet.Range[$"G{index}"].Value = TotalPurchase.ToString();
            sheet.Range[$"G{index}"].NumberFormat = "0.00 ₽";


            sheet.Range[$"A3:G{index}"].BorderInside(LineStyleType.Thin);
            sheet.Range[$"A3:G{index}"].BorderAround(LineStyleType.Medium);

            sheet.AllocatedRange.AutoFitColumns();

            workbook.SaveToFile("text2.xls");
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(Environment.CurrentDirectory + "/text2.xls")
            {
                UseShellExecute = true
            };
            p.Start();
        }
        private async Task GetList()
        {
            FullOrders = await Api.GetListAsync<List<OrderApi>>("Order");
            FullOrderIns = await Api.GetListAsync<List<ProductOrderInApi>>("ProductOrderIn");
            SaleTypes = await Api.GetListAsync<List<SaleTypeApi>>("SaleType");
            Products = await Api.GetListAsync<List<ProductApi>>("Product");
            ActionTypes = await Api.GetListAsync<List<ActionTypeApi>>("ActionType");
        }
    }
}
