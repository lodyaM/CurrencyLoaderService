using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyService
{
    public class CurrencyContent
    {
        public string Code { get; set; }
        public string FullName { get; set; }
        public decimal Rate { get; set; }
        public DateTime Date { get; set; }
    }
}
