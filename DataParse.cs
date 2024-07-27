using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CurrencyService
{
    public class DataParse
    {
        public List<CurrencyContent> Parse(string xmlResponse)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlResponse);
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xml.NameTable);

            //https://stackoverflow.com/questions/76456748/is-it-allowed-to-system-xml-xmlnamespacemanager-addnamespace-an-empty-namespace
            namespaceManager.AddNamespace("soap", "http://schemas.xmlsoap.org");
            namespaceManager.AddNamespace("m", "http://web.cbr.ru/");
            XmlNodeList nodes = xml.SelectNodes("//ValuteData/ValuteCursOnDate", namespaceManager);
            List<CurrencyContent> currencies = new List<CurrencyContent>();
            foreach (XmlNode node in nodes)
            {
                currencies.Add(new CurrencyContent
                {
                    //поля указаны и их обозначения указаны на http://www.cbr.ru/DailyInfoWebServ/DailyInfo.asmx
                    Code = node["VchCode"].InnerText,
                    FullName = node["Vname"].InnerText,
                    Rate = decimal.Parse((node["Vcurs"].InnerText).Replace('.',',')),
                    Date = DateTime.Now

                });
            }
            return currencies;
            

        }

    }
}
