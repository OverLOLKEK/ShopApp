using ModelsApi;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ShopClient.Helper
{
    public class ClientNameConverter : IValueConverter
    {
        public List<LegalClientApi> LegalClients;
        public List<PhysicalClientApi> PhysicalClients;
        public List<ClientApi> Clients;
         string name = "";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           
            string phone = (string)value;
            //var client = Clients.First(s => s.Phone == phone);
            GetList(phone).Wait();

            return name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        async Task GetList(string phone)
        {
            LegalClients = await Api.GetListAsync<List<LegalClientApi>>("LegalClient");
            PhysicalClients = await Api.GetListAsync<List<PhysicalClientApi>>("PhysicalClient");
            var client = PhysicalClients.Find(s => s.Client.Phone == phone);
            if (client != null)
            {
                name = client.LastName + " " + client.FirstName;
            }
            else
            {
                var legClient = LegalClients.Find(s => s.Client.Phone == phone);
                if (legClient != null)
                    name = legClient.Title;
            }
        }
    }
}
