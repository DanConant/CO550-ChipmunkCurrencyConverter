using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace CO550_ChipmunkCurrencyConverter.Pages
{
    public class IndexModel : PageModel
    {
        public string inputAmount;
        public decimal outputAmount;
        public decimal EUR;
        public void OnGet()
        {
            GetCurrencyRates();
        }
        public class CurrencyResponse
        {
            [JsonProperty("query")]
            public Query Query { get; set; }

            [JsonProperty("data")]
            public Dictionary<string, decimal> Data { get; set; }
        }

        public class Query
        {
            [JsonProperty("apikey")]
            public Guid Apikey { get; set; }

            [JsonProperty("base_currency")]
            public string BaseCurrency { get; set; }

            [JsonProperty("timestamp")]
            public long Timestamp { get; set; }
        }

        public void GetCurrencyRates()
        {
            //apikey e26400d0-3e3f-11ec-b40d-7bd419cca186   
            WebRequest request = WebRequest.Create("https://freecurrencyapi.net/api/v2/latest?apikey=e26400d0-3e3f-11ec-b40d-7bd419cca186&base_currency=GBP");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Console.WriteLine(response.StatusDescription);
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            CurrencyResponse currencyResponse = JsonConvert.DeserializeObject<CurrencyResponse>(responseFromServer);
            Console.WriteLine("this is the currency response: ");
            Console.WriteLine(currencyResponse.Data["EUR"]);
            EUR = currencyResponse.Data["EUR"];
            reader.Close();
            dataStream.Close();
            response.Close();
        }
        public void OnPost()
        {
                GetCurrencyRates();
                this.inputAmount = Request.Form["inputAmount"];
                Console.WriteLine(EUR);
                this.outputAmount = Convert.ToDecimal(inputAmount) * EUR;
         

            }

        }
    }
