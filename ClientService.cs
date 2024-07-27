using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyService
{
    internal class ClientService
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        public async Task<string> GetRateAsync(DateTime date, string soapXml)
        {
            
            string url = @"https://www.cbr.ru/DailyInfoWebServ/DailyInfo.asmx";
            
            string soap = await File.ReadAllTextAsync(soapXml);
            
            //в xml файле будет замена {0} на дату для работы сервиса
            soap = string.Format(soap, date.ToString("yyyy-MM-dd"));
            var content = new StringContent(soap, Encoding.UTF8, "text/xml");
            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();

        }
        
    }

}
