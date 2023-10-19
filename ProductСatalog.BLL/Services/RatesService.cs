using ProductCatalog.BLL.Models;
using System.Net.Http.Json;

namespace ProductCatalog.BLL.Services
{
    public class RatesService : IRatesService
    {
        private readonly HttpClient _httpClient;

        private const string ApiPath = "exrates/rates";

        public RatesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RateModel?> GetRateForTodayAsync(int innerCurrencyCode)
        {
            return await _httpClient.GetFromJsonAsync<RateModel>($"{ApiPath}/{innerCurrencyCode}");
        }
    }
}
