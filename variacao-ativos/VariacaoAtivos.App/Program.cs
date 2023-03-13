using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using VariacaoAtivos.Domain.Models;

namespace HttpClientSample
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            try
                { 
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new System.Uri("https://query2.finance.yahoo.com/v8/finance/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync("chart/PETR4.SA?metrics=high?&interval=1d&range=30d");
                    if (response.IsSuccessStatusCode)
                    {  //GET
                        YahooActiveVariation content = await response.Content.ReadAsAsync<YahooActiveVariation>();
                        Console.WriteLine("Pregão do ativo PETR4 recuperado do Yahoo Finance.");
                        Console.ReadKey();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}