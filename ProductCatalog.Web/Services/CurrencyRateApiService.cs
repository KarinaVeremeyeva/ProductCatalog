using ProductCatalog.Web.DTOs;

namespace ProductCatalog.Web.Services
{
    public class CurrencyRateApiService : ICurrencyRateApiService
    {
        private readonly HttpClient _httpClient;

        private const string RatesPath = "api/Rates";

        public CurrencyRateApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<RateDto?> GetUsdRateAsync()
        {
            return await _httpClient.GetFromJsonAsync<RateDto>($"{RatesPath}");
        }
    }
}
