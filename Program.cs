namespace CurrencyService
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            
            ClientService service = new ClientService();
            DataParse dataParse = new DataParse();
            DbManager dbManager = new DbManager();

            string dailySoap = @"..\..\..\GetCursOnDate.xml";
            string dailyResponse = await service.GetRateAsync(DateTime.Today, dailySoap);

            /*
            на сайте Цб api указан как устаревший. поэтому не работает

            string monthlyResponse = await service.GetRateAsync(firstDay, monthlySoap);
            List<CurrencyContent> monthlyRates = dataParse.Parse(monthlyResponse);
            await dbManager.InsertAsync(monthlyRates);
            */


            List<CurrencyContent> monthlyCurrencies = new List<CurrencyContent>();
            
            //Month - 1 т.к. иначе не получится подсчитать среднее значение за месяц
            DateTime firstday = new DateTime(DateTime.Today.Year,DateTime.Today.Month-1, 1);
            for (int i = 1; i<=30; i++)
            {
                DateTime date = new DateTime(DateTime.Today.Year,DateTime.Today.Month-1, i);
                string monthlyResponse = await service.GetRateAsync(DateTime.Today, dailySoap);
                List<CurrencyContent> dailyCurrencies = dataParse.Parse(monthlyResponse);
                monthlyCurrencies = dailyCurrencies;
                for (int j = 0; j < dailyCurrencies.Count; j++)
                {

                    monthlyCurrencies[j].Date = firstday;
                    monthlyCurrencies[j].Rate+= dailyCurrencies[j].Rate/dailyCurrencies.Count;
                }
                

            }

            

            List<CurrencyContent> dailyRates = dataParse.Parse(dailyResponse);
            await dbManager.InsertAsync(dailyRates);
            await dbManager.InsertAsync(monthlyCurrencies);
            

        }

        
    }
}