using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;

namespace CO550_ChipmunkCurrencyConverter.Pages
{
    public class IndexModel : PageModel
    {
        public string inputAmount;
        public decimal outputAmount;
        public decimal EURO = 1.35M;
        public decimal GBP = 1M;
        public void OnGet()
        {
            GetCurrencyRates();
        }
        public class Currency
        {
            public string Query { get; set; }
            public string Data { get; set; }
        }

        public void GetCurrencyRates()
        {
            //apikey e26400d0-3e3f-11ec-b40d-7bd419cca186   
            WebRequest request = WebRequest.Create("https://freecurrencyapi.net/api/v2/latest?apikey=e26400d0-3e3f-11ec-b40d-7bd419cca186&base_currency=EUR");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Console.WriteLine(response.StatusDescription);
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            Currency currency1 = JsonSerializer.Deserialize<Currency>(responseFromServer);
            Console.WriteLine(currency1.Data);
            //Console.WriteLine(responseFromServer);
            //Console.WriteLine($"Data: {currency1}");
            reader.Close();
            dataStream.Close();
            response.Close();
            Console.WriteLine(response);
        }
        public void OnPost()
        {
            {
                this.inputAmount = Request.Form["inputAmount"];
                this.outputAmount = Convert.ToDecimal(inputAmount) * EURO;
            }

        }
    }
}