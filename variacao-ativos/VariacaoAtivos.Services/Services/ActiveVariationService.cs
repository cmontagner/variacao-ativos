using System.Net.Http.Headers;
using VariacaoAtivos.Domain.Models;
using VariacaoAtivos.Domain.Response;
using VariacaoAtivos.Infra.Repositories.Interfaces;
using VariacaoAtivos.Service.Services.Interfaces;

namespace VariacaoAtivos.Service.Services
{
    public class ActiveVariationService : IActiveVariationService
    {
        static HttpClient client = new HttpClient();
        private readonly IActiveVariationRepository _activeVariationRepository;

        public ActiveVariationService
        (
            IActiveVariationRepository activeVariationRepository
        )
        {
            _activeVariationRepository = activeVariationRepository;
        }

        static async Task<YahooActiveVariation> GetYahooFinanceData()
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
                        return content;
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<ActiveVariation> FillActiveVariatiosList(YahooActiveVariation yahooActiveVariation)
        {
            List<ActiveVariation> activeVariations = new List<ActiveVariation>();

            foreach (var result in yahooActiveVariation.chart.result)
            {
                int count = 1;
                foreach (var timestamp in result.timestamp)
                {
                    ActiveVariation variation = new ActiveVariation();
                    variation.Dia = count;
                    variation.Data = TimeStampToDateTime(timestamp);
                    count++;

                    activeVariations.Add(variation);
                }

                foreach (var value in result.indicators.quote)
                {
                    for (int i = 0; i < count-1; i++)
                    {
                        activeVariations[i].Valor = value.open[i];

                        if (i > 0)
                        {
                            activeVariations[i].VariacaoDMenosUM = CalculatePercentageChange(activeVariations[i].Valor, activeVariations[i - 1].Valor);
                            activeVariations[i].VariacaoPrimeiraData = CalculatePercentageChange(activeVariations[i].Valor, activeVariations[0].Valor);
                        }
                    }
                }
            }

            return activeVariations;
        }

        public async Task<ApiResponse<List<ActiveVariation>>> GetActiveVariation()
        {
            ApiResponse<List<ActiveVariation>> ret = new ApiResponse<List<ActiveVariation>>() { Success = true };

            List<ActiveVariation> activeVariation = await _activeVariationRepository.GetAsync();
            ret.Data = activeVariation;

            return ret;
        }

        public async Task<ApiResponse<List<ActiveVariation>>> CreateActiveVariation()
        {
            ApiResponse<List<ActiveVariation>> ret = new ApiResponse<List<ActiveVariation>>() { Success = true };

            var activeVariations = FillActiveVariatiosList(await GetYahooFinanceData());

            foreach (ActiveVariation variation in activeVariations)
            { 
                await _activeVariationRepository.CreateAsync(variation);
            }

            ret.Data = activeVariations;

            return ret;
        }

        public async Task InsertActiveVariation(List<ActiveVariation> variations)
        {
            ApiResponse<List<ActiveVariation>> ret = new ApiResponse<List<ActiveVariation>>() { Success = true };

            foreach (ActiveVariation variation in variations)
            {
                await _activeVariationRepository.CreateAsync(variation);
            }
        }

        private static DateTime TimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        private static decimal CalculatePercentageChange(decimal val1, decimal val2)
        {
            return Math.Round(((val1 - val2) / val2) * 100);
        }
    }
}
