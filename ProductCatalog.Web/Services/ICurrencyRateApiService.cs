using ProductCatalog.Web.DTOs;

namespace ProductCatalog.Web.Services
{
    public interface ICurrencyRateApiService
    {
        Task<RateDto?> GetUsdRateAsync();
    }
}
